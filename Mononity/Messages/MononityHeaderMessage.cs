
/*********************
 * Mononity
 * Ver : 1.5 
 * Date : 2017.11.30
*********************/

using System;
using UnityEngine;

namespace Mononity.Messages
{
	[Serializable]
	public class MononityHeader{
		public string apiKey;
		public string time;
		public string language;
		public string manufacturer;
		public string device;
		public string os;
	}

	public class MononityHeaderMessage
	{
		MononityHeader monoHead;

		public MononityHeaderMessage ()
		{
			monoHead = new MononityHeader();

			monoHead.apiKey = "MononityTest";
			monoHead.time = System.DateTime.Now.ToString ("yyyy/MM/dd HH:mm:ss");
			monoHead.language = Application.systemLanguage.ToString();
			monoHead.manufacturer = SystemInfo.deviceUniqueIdentifier;
			monoHead.device = SystemInfo.deviceModel;
			monoHead.os = SystemInfo.operatingSystem;
		}

		public string returnJson() {
			//Debug.Log ( JsonUtility.ToJson (monoHead, prettyPrint: true) );
			return JsonUtility.ToJson (monoHead, prettyPrint: false);
		}
	}
}

