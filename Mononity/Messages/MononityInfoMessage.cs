/*********************
 * Mononity
 * Ver : 1.0 
 * Date : 2017.10.07
*********************/

using System;
using UnityEngine;

namespace Mononity.Messages
{
	public class MononityInfoMessage
	{
		public MononityInfoMessage ()
		{
			DeviceModel = SystemInfo.deviceModel;
			DeviceType = SystemInfo.deviceType.ToString();
			DateTime = System.DateTime.Now.ToString ("yyyy/MM/dd HH:mm:ss");
		}

		public string DeviceModel { get; set; }

		public string DeviceType { get; set; }

		public string DateTime { get; set; }

		public string returnJson() {
			return JsonUtility.ToJson (this);
		}
	}
}

