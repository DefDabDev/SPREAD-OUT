using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayRotator : MonoBehaviour {

	private float _currentAngle = 0f;
	public float currentAngle { get {return _currentAngle;} }

	private const float _correctionvalue = Mathf.Deg2Rad * 90f;
	private Vector3 _scale;

	private void Start () {
		_scale = transform.localScale;
		_currentAngle = 0;
	}

	private void Update()
	{
		if (Input.GetMouseButton(0))
		{
			Vector2 touchPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 distance = (Vector2)transform.position - touchPoint;
			distance = distance.normalized;
			_currentAngle = (Mathf.Atan2(distance.y, distance.x) + _correctionvalue) * Mathf.Rad2Deg;
			SetRotationZ(_currentAngle);
		}
	}

	public void SetRotationZ(float z)
	{
		float targetZ = z + 90f;
		float value = (targetZ > 90f && targetZ < 270f) ? -1f : 1f;

		transform.localScale = new Vector3(_scale.x, value * _scale.y, _scale.z);
		transform.localRotation = Quaternion.Euler(0f, 0f, targetZ);
	}
}
