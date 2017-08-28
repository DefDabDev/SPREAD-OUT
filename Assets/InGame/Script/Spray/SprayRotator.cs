using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayRotator : MonoBehaviour {

	private float _currentAngle = 0f;
	public float currentAngle { get {return _currentAngle;} }

	private const float _correctionvalue = Mathf.Deg2Rad * 90f;

	private void Start () {
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
		transform.localRotation = Quaternion.Euler(0f, 0f, z);
	}
}
