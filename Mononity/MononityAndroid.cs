
/*********************
 * Mononity
 * Ver : 1.3 
 * Date : 2017.11.20
*********************/

using System;
using System.Threading;
using System.Diagnostics;
using UnityEngine;

namespace Mononity
{
	public class MononityAndroid
	{
		private AndroidJavaObject mononityInstance = null;
		private AndroidJavaObject activityContext = null;
		public MononityAndroid ()
		{
			mononityInstance = new AndroidJavaClass("com.test.unityplugin.PluginClass");
			/*
			using (AndroidJavaClass activityClass = new AndroidJavaClass ("com.unity3d.player.UnityPlayer")){
				activityContext = activityClass.GetStatic<AndroidJavaObject> ("currentActivity");
				if (activityContext == null) {
					//
				}

				mononityInstance = new AndroidJavaClass("com.test.unityplugin.PluginClass");
			}
			*/
		}

		public string returnData(){
			//string tempdata = mononityInstance.CallStatic<string> ("tempmsg");
			//string tempdata = (new AndroidJavaClass("com.test.unityplugin.PluginClass")).CallStatic<string>("GetCallStack");
			//return mononityInstance.CallStatic<string> ("tempmsg");
			if (AndroidJNI.AttachCurrentThread () == 0) {
				
				string getmemoryallocated = mononityInstance.CallStatic<string> ("GetMemoryAllocated");
				string Androidcallstack = mononityInstance.CallStatic<string> ("GetCallStack");
				string getcpuusage = mononityInstance.CallStatic<string> ("GetCpuUsagePercentage");
				string getcurrentthreadname = mononityInstance.CallStatic<string> ("getCurrentThreadName");
				string Unitycallstack = System.Environment.StackTrace;

				string log = "GetMemoryAllocated : " + getmemoryallocated + "\n\n" +
				             "CallUnityStack : " + Unitycallstack + "\n\n" +
				             "CallAndroidStack : " + Androidcallstack + "\n\n" +
				             "GetcpuUsage : " + getcpuusage + "\n\n" +
				             "GetcurrentThreadname : " + getcurrentthreadname + "\n\n";
				AndroidJNI.DetachCurrentThread ();
				return log;
			} else {
				return "NULL";
			}
		}
	}
}

