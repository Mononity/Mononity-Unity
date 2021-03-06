﻿
/*********************
 * Mononity
 * Ver : 1.5 
 * Date : 2017.11.30
*********************/

using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using Mononity.Messages;
using UnityEngine;
using UnityEngine.Networking;

namespace Mononity
{
	public class MononityClient
	{
		// Mononity Client
		private static DateTime stackTime = DateTime.Now;

		public MononityClient ()
		{
			stackTime = DateTime.Now;
			Console.WriteLine ("Mononity Client Create");
		}

		// SendMessage to Server
		// Usage : StartCoroutine(mononityClient.SendMessage());
		public IEnumerator SendMessage(){
			//string url = "http://127.0.0.1:9147/thingplug/sub";
			string url = "http://210.94.194.82:9147/thingplug/sub";

			MononityMessage monoMessage = new MononityMessage ();
			byte[] data = Encoding.UTF8.GetBytes (monoMessage.returnJson ().ToCharArray());
	
			Hashtable header = new Hashtable();
			header.Add("Content-Type", "text/json");
			//header.Add("Content-Length", data.Length.ToString());

			WWW www = new WWW(url, data, header);

			yield return www;
			if (www.isDone && www.error == null)
			{
				Debug.Log("www Result: " + www.text);
			}
			else
			{
				Debug.Log("error : " + www.error);
			}
			/*
			WWWForm form = new WWWForm ();
			form.AddField ("header", header.returnJson());
			//form.AddField ("header", "TEST");
			form.AddField ("data", "NULL");

			UnityWebRequest www = UnityWebRequest.Post (url, form);
			yield return www.Send ();

			if(www.isNetworkError || www.isHttpError) {
				//Debug.Log(www.error);
				Debug.Log("Error");
			}
			else {
				Debug.Log("Form upload complete!");
			}
			*/
		}

		// for create test
		public static void StackTrace(){
			DateTime currentTime = DateTime.Now;
			if(currentTime.Subtract(stackTime).Seconds >= 5){
				stackTime = currentTime;
				string filename = stackTime.ToString("Stack-yyyy-MM-dd-HH:mm:ss");
				string filepath = Application.persistentDataPath + "/" + filename +".txt";
				string stackdata = System.DateTime.Now.ToString ("yyyy/MM/dd HH:mm:ss\n") + System.Environment.StackTrace;
				Debug.Log(System.Environment.StackTrace);
				System.IO.File.WriteAllText (filepath, stackdata);
			}
		}
	}
}

