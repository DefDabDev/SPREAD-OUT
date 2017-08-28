using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UVScroll : MonoBehaviour {

	[SerializeField]
	private float _speed = 1f;

	private float _x = 0f;

	private RawImage _rawImage = null;

	private void Awake()
	{
		_rawImage = GetComponent<RawImage>();
	}

	private void Update()
	{
		_x += Time.smoothDeltaTime * _speed;
		_rawImage.uvRect = new Rect(_x, 0f, 1f, 1f);
	}
}
