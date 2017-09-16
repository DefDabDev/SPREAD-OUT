using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AL.ALLog
{
	public static class ALLog 
	{
		public static void Log(object message)
		{
		#if UNITY_EDITOR
			if (Application.isEditor)
				Debug.Log(message);
		#endif
		}

		public static void WarningLog(object message)
		{
		#if UNITY_EDITOR
			if (Application.isEditor)
				Debug.LogWarning(message);
		#endif
		}

		public static void ErrorLog(object message)
		{
		#if UNITY_EDITOR
			if (Application.isEditor)
				Debug.LogError(message);
		#endif
		}
	}
}
