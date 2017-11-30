
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
	public class MononityMessageFormat{
		public string head;
		public string data;
	}

	public class MononityMessage
	{
		MononityMessageFormat monoFormat;
		MononityHeaderMessage monoHeader;
		MononityProfilingMessage monoProfiling;

		public MononityMessage ()
		{
			monoFormat = new MononityMessageFormat ();
			monoHeader = new MononityHeaderMessage ();
			monoProfiling = new MononityProfilingMessage ();
		}

		public string returnJson() {
			monoFormat.head = monoHeader.returnJson ();
			monoFormat.data = monoProfiling.returnJson ();
			//Debug.Log ( JsonUtility.ToJson (monoProfiling, prettyPrint: true) );
			return JsonUtility.ToJson (monoFormat, prettyPrint: false);
		}
	}
}

