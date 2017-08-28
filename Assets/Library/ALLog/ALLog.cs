using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AL.ALLog
{
	public static class ALLog 
	{
		public static void Log(object message)
		{
			if (Application.isEditor)
				Debug.Log(message);
		}

		public static void WarningLog(object message)
		{
			if (Application.isEditor)
				Debug.LogWarning(message);
		}

		public static void ErrorLog(object message)
		{
			if (Application.isEditor)
				Debug.LogError(message);
		}
	}
}
