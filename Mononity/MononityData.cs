
/*********************
 * Mononity
 * Ver : 1.5 
 * Date : 2017.11.30
*********************/

using System;
using UnityEngine;

namespace Mononity
{
	[Serializable]
	public class MononityData{
		public string apiKey;
		public string time;
		public string unityStackDump;
		public string fps;
		public string currentScene;
		public string androidProfileData;
	}

	public class MononityDataController
	{
		MononityData mononityData;

		public MononityDataController (string apiKey, string stackDump, string fpsData, string sceneData, string androidData)
		{
			mononityData = new MononityData();

			mononityData.apiKey = apiKey;
			mononityData.time = System.DateTime.Now.ToString ("yyyy/MM/dd HH:mm");
			mononityData.unityStackDump = stackDump;
			mononityData.fps = fpsData;
			mononityData.currentScene = sceneData;
			mononityData.androidProfileData = androidData;
		}

		public string returnJson() {
			//Debug.Log ( JsonUtility.ToJson (monoHead, prettyPrint: true) );
			return JsonUtility.ToJson (mononityData, prettyPrint: true);
		}
	}
}

