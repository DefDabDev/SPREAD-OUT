using System.Collections;
using System.Collections.Generic;
using AL.ALUtil;
using UnityEngine;

public class CameraFollower : MonoBehaviour {

	[SerializeField]
	private float _smoothScale = 5f;

	[SerializeField]
	private Transform _target = null;

	private float _offsetY = 0f;

	private void Start()
	{
		_offsetY = transform.localPosition.y - _target.localPosition.y;
		Debug.Log(_offsetY);
	}

	private void Update()
	{
		if (_target.localPosition.y + _offsetY < _offsetY)
			return;

		Vector3 cameraPosition = new Vector3(transform.localPosition.x, _target.localPosition.y + _offsetY, transform.localPosition.z);
		transform.localPosition = ALLerp.Lerp(transform.localPosition, cameraPosition, _smoothScale * Time.deltaTime);
	}
}
