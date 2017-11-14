
/*********************
 * Mononity
 * Ver : 1.2 
 * Date : 2017.11.13
*********************/


using System;
using System.Threading;
using System.Diagnostics;
using UnityEngine;

using StreamWriter = System.IO.StreamWriter;

namespace Mononity
{
	public class MononityAgent:MonoBehaviour
	{
		public Timer mononityTimer;

		// SINGLETON
		public static AndroidJavaClass mononityPlugin;

		public static MononityAgent instance;

		public void OnApplicationQuit() {		// Ensure that the instance is destroyed when the game is stopped in the editor.			
			instance = null;
			mononityTimer.Dispose ();
		}

		public void Awake() 
		{
			//Debug.Log ("MonoAgent Awake");
			if (instance != null){
				Destroy (gameObject);
			}else{
				instance = this;
				DontDestroyOnLoad (gameObject);
			} 
		}

		public void OnEnable()
		{
			//Debug.Log ("MonoAgent OnEnable");
		}

		public void Start()
		{
			//Debug.Log ("MonoAgent Start");
			mononityPlugin = new AndroidJavaClass("com.test.unityplugin.PluginClass");

			//monoTimer = new Timer(Printing, "Hello", 1000, 5000);
			//mononityTimer = new Timer(GetData, "", 1000, 5000);
			//mononityTimer = new Timer(GetFullStack, "", 1000, 5000);
			mononityTimer = new Timer(SceneDump, "", 1000, 5000); // Dump Full Scenes
		}

		public static void Printing (object data){
			//Debug.Log (data);
		}

		public static void GetData (object data){
			/*
			string getmemoryallocated = mononityPlugin.CallStatic<string>("GetMemoryAllocated");
			string Androidcallstack = mononityPlugin.CallStatic<string>("GetCallStack");
			string getcpuusage = mononityPlugin.CallStatic<string>("GetCpuUsagePercentage");
			string getcurrentthreadname = mononityPlugin.CallStatic<string>("getCurrentThreadName");

			string Unitycallstack = System.Environment.StackTrace;

			string log = "GetMemoryAllocated : " + getmemoryallocated + "\n\n" +
				"CallUnityStack : " + Unitycallstack + "\n\n"+
				"CallAndroidStack : " + Androidcallstack + "\n\n" +
				"GetcpuUsage : " + getcpuusage + "\n\n" +
				"GetcurrentThreadname : " + getcurrentthreadname + "\n\n";
		
			string filepath = Application.persistentDataPath + "/temp" + System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".txt";
			System.IO.File.WriteAllText(filepath, log);
			*/
			string filepath = Application.persistentDataPath + "/temp" + System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".txt";
			System.IO.File.WriteAllText(filepath, System.Environment.StackTrace);
		}

		/*
		public static void GetFullStack (object data){
			string filepath = Application.persistentDataPath + "/temp" + System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".txt";

			using (StreamWriter writer = new StreamWriter (filepath, false)) {
				ProcessThreadCollection currentThreads = Process.GetCurrentProcess ().Threads;

				foreach (ProcessThread thread in currentThreads) {
					StackTrace st = new StackTrace(thread, true);
					string stackIndent = "";
					for(int i =0; i< st.FrameCount; i++ )
					{
						StackFrame sf = st.GetFrame(i);
						Console.WriteLine();
						Console.WriteLine("{0} Method: {1}", stackIndent, sf.GetMethod() );
						Console.WriteLine("{0} File: {1}", stackIndent, sf.GetFileName());
						Console.WriteLine("{0} Line Number: {1}", stackIndent, sf.GetFileLineNumber());
						stackIndent += "  ";
					}
				}
			}
		}
		*/

		public static void SceneDump (object data){
			string filepath = Application.persistentDataPath + "/temp" + System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".txt";

			using (StreamWriter writer = new StreamWriter(filepath, false))
			{
				GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>() ;
				foreach (GameObject gameObject in allObjects)
				{
					DumpGameObject(gameObject, writer, "");
				}
			}
		}


		private static void DumpGameObject(GameObject gameObject, StreamWriter writer, string indent)
		{
			writer.WriteLine("{0}+{1}", indent, gameObject.name);

			foreach (Component component in gameObject.GetComponents<Component>())
			{
				DumpComponent(component, writer, indent + "  ");
			}

			foreach (Transform child in gameObject.transform)
			{
				DumpGameObject(child.gameObject, writer, indent + "  ");
			}
		}

		private static void DumpComponent(Component component, StreamWriter writer, string indent)
		{
			writer.WriteLine("{0}{1}", indent, (component == null ? "(null)" : component.GetType().Name));
		}
	}
}

