using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaintMover : MonoBehaviour {

	[SerializeField]
	private float _speed = 1f;
	
	private Rigidbody2D _rigidBody2D = null;
	private Camera _mainCamera = null;
	private void Awake()
	{
		_mainCamera = Camera.main;
		_rigidBody2D = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		if (IsOutOfScreen())
		{
			gameObject.SetActive(false);
		}
	}

	private void FixedUpdate() 
	{
		_rigidBody2D.velocity = transform.up * _speed;
	}

	public bool IsOutOfScreen()
	{
		Rect rect = new Rect(0f, 0f, 1920f, 1080f);
		if (rect.Contains(_mainCamera.WorldToScreenPoint(transform.position)))
		{
			return true;
		}
		return false;
	}

	public void SetPaint(float angle)
	{
		transform.localRotation = Quaternion.Euler(0f, 0f, angle);
	}
}
