/*********************
 * Mononity
 * Ver : 1.0 
 * Date : 2017.10.07
*********************/

using System;
using System.Collections;
using System.Collections.Generic;
using Mononity.Messages;
using UnityEngine;

namespace Mononity
{
	public class MononityClient
	{
		private readonly string _token;

		public MononityClient (string token)
		{
			//Console.WriteLine ("Mononity Client Create");
			//Console.WriteLine (token);
			//print ("Mononity Client Create");
			//print (token);
			_token = token;
		}

		public void SendMessage(string message){
			MononityInfoMessage mononityInfoMessage = new MononityInfoMessage ();
			Byte[] data = StringToAscii(mononityInfoMessage.returnJson ());
			Dictionary<string, string> headers = new Dictionary<string, string>();
			headers["X-ApiKey"] = _token;
			new WWW ("http://210.94.194.82:9981/thingplug/sub", data, headers);
			//Console.WriteLine (mononityInfoMessage);
			//print (mononityInfoMessage);
		}

		// for create test
		public string isPossible(){
			MononityInfoMessage mononityInfoMessage = new MononityInfoMessage ();
			string jsonValue = mononityInfoMessage.returnJson ();
			MononityInfoMessage dataFromJson = JsonUtility.FromJson<MononityInfoMessage> (jsonValue);
			return dataFromJson.DeviceModel;
			//return mononityInfoMessage.DeviceModel;
			//return _token;
		}

		private static byte[] StringToAscii(string s)
		{
			byte[] retval = new byte[s.Length];
			for (int i = 0; i < s.Length; i++)
			{
				char ch = s[i];
				retval[i] = ch <= 0x7f ? (byte)ch : (byte)'?';
			}
			return retval;
		}
	}
}

