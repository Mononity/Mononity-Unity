
/*********************
 * Mononity
 * Ver : 1.2 
 * Date : 2017.11.13
*********************/

using System;
using UnityEngine;
using System.Diagnostics;

namespace Mononity.Messages
{
	[Serializable]
	public class MononityProfiling{
		public string framerate;
		public string cpu;
		public string ram;
	}
	public class MononityProfilingMessage
	{
		MononityProfiling monoProfiling;
		//PerformanceCounter cpuCounter;
		//PerformanceCounter ramCounter;

		public MononityProfilingMessage ()
		{
			//cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total", true);
			//ramCounter = new PerformanceCounter("Memory", "Available MBytes", true);

			monoProfiling = new MononityProfiling ();
			monoProfiling.framerate = Application.targetFrameRate+"bps";
			//monoProfiling.cpu = cpuCounter.NextValue () + "%";
			//monoProfiling.ram = ramCounter.NextValue () + "MB";
			monoProfiling.cpu = "0%";
			monoProfiling.ram = "0MB";
		}

		public string returnJson() {
			//Debug.Log ( JsonUtility.ToJson (monoProfiling, prettyPrint: true) );
			return JsonUtility.ToJson (monoProfiling, prettyPrint: false);
		}
	}
}

