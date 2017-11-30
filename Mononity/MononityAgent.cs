
/*********************
 * Mononity
 * Ver : 1.5 
 * Date : 2017.11.30
*********************/

using System;
using System.Threading;
using System.Diagnostics;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

using StreamWriter = System.IO.StreamWriter;

namespace Mononity
{
	public class MononityAgent:MonoBehaviour
	{
		// SINGLETON
		public static float deltaTime = 0.0f;
		public static bool threadActivate = false;

		public static Timer mononityTimer;
		public static MononityAndroid mononityAndroid = null;
		public static MononityAgent instance;

		public static Queue profilingData;

		public string apiKey;
		public static string staticKey;

		public void Update()
		{
			deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
		}

		public void OnApplicationQuit() {		// Ensure that the instance is destroyed when the game is stopped in the editor.
			SaveData();
			instance = null;
			mononityTimer.Dispose ();
		}

		public void OnApplicationPause()
		{
			SaveData ();
			GetStackData ();
			mononityTimer.Dispose ();
			threadActivate = false;
		}

		public void Awake() 
		{
			UnityEngine.Debug.Log ("MonoAgent Awake");
			if (instance != null){
				Destroy (gameObject);
			}else{
				instance = this;
				DontDestroyOnLoad (gameObject);
			} 
		}

		public void OnEnable()
		{
			UnityEngine.Debug.Log ("MonoAgent OnEnable");
			//Application.logMessageReceived += MononityHander;
		}

		public void OnDisable()
		{
			UnityEngine.Debug.Log ("MonoAgent OnDisable");
		}

		public void FixedUpdate()
		{
			if (!threadActivate) {
				mononityTimer = new Timer (GetData, "", 1000, 5000);
				threadActivate = true;
			}
		}
		/*
		public void OnDisable()
		{
			//Application.logMessageReceived -= MononityHander;
		}

		void MononityHander(string logString, string stackTrace, LogType type)
		{
			//string filepath = Application.persistentDataPath + "/CrashReport" + System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".txt";
			//System.IO.File.WriteAllText(filepath, logString + "\r\n" + stackTrace);
			if (type == LogType.Exception) {
				string filepath = Application.persistentDataPath + "/CrashReport" + System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".txt";
				System.IO.File.WriteAllText(filepath, stackTrace);
			}
		}
		*/

		public void Start()
		{
			int startdelay = UnityEngine.Random.Range (10, 100) * 100;
			profilingData = new Queue ();
			mononityAndroid = new MononityAndroid ();
			staticKey = apiKey;
			UnityEngine.Debug.Log ("MonoAgent Start");

			//System.IO.File.WriteAllText(Application.persistentDataPath + "/Start.txt", "Delay : " + startdelay);

			if (!threadActivate) {
				mononityTimer = new Timer (GetData, "", startdelay, 5000);
				threadActivate = true;
			}
			//mononityTimer = new Timer(GetFullStack, "", 1000, 5000);
		}


		public static void GetData (object data){
			float fps = 1.0f / deltaTime;
			string stackDump = System.Environment.StackTrace;
			string currentScene = SceneManager.GetActiveScene ().name;
			string androidData = mononityAndroid.returnData ();
			MononityDataController dataController = new MononityDataController (staticKey, stackDump, fps.ToString (), currentScene, androidData);
			string currentProfilingData = dataController.returnJson ();
			profilingData.Enqueue (currentProfilingData);

			//System.IO.File.WriteAllText(filepath, currentProfilingData);

			/*
			string filepath = Application.persistentDataPath + "/ProfileData" + System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".txt";
			StackTrace stackData = new StackTrace(true);

			System.IO.File.WriteAllText(filepath, stackData.ToString());
			*/
	
			if (profilingData.Count > 100) {
				profilingData.Dequeue ();
			}
		}

		public static void SaveData(){
			int idx = 1;
			string fulldata = "[";

			while (profilingData.Count > 0) {
				fulldata += profilingData.Peek ();
				fulldata += ", ";
				profilingData.Dequeue ();
				idx++;
			}
			fulldata += "]";
				
			System.IO.File.WriteAllText(Application.persistentDataPath + "/ProfileDump.txt", fulldata);
		}

		public static void GetStackData(){
			string data = "";
			data += "StackTraceUtility : " + StackTraceUtility.ExtractStackTrace ();
			/*
			data += "Current Thread : " + Thread.CurrentThread.Name + "\r\n";
			ProcessThreadCollection currentThreads = Process.GetCurrentProcess ().Threads;
			data += "Thread Count : " + currentThreads.Count + "\r\n";
			foreach (ProcessThread thread in currentThreads) {
				data += thread.Id + "\r\n";
			}
			*/
			System.IO.File.WriteAllText(Application.persistentDataPath + "/StackDump.txt", data);
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
	}
}

