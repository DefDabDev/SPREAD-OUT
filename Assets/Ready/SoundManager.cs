using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	private static bool _isInit = false;

	private void Awake()
	{
		if (!_isInit)
		{
			DontDestroyOnLoad(gameObject);
			_isInit = true;
		}
		else
		{
			Destroy(gameObject);
		}
	}
}
