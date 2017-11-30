
/*********************
 * Mononity
 * Ver : 1.5 
 * Date : 2017.11.30
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
			mononityInstance = new AndroidJavaClass("com.mononity.haribo.MainActivity");
		}

		public string returnData(){
			if (AndroidJNI.AttachCurrentThread () == 0) {
				string androiddata = mononityInstance.CallStatic<string>("GetProfileData");
				string Unitycallstack = System.Environment.StackTrace;

				AndroidJNI.DetachCurrentThread ();

				return androiddata;
			} else {
				return "NULL";
			}
		}
	}
}

