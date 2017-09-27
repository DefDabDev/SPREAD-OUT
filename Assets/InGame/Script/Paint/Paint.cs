using System.Collections;
using System.Collections.Generic;
using AL.ALUtil;
using UnityEngine;
using UnityEngine.UI;



public class Paint : MonoBehaviour {

	[SerializeField]
	private float _speed = 1f;

	private Rigidbody2D _rigidBody2D = null;
	private Camera _mainCamera = null;
	private readonly Rect _screenRect = new Rect(0f, 0f, 1920f, 1080f);

	private void Awake()
	{
		_mainCamera = Camera.main;
		_rigidBody2D = GetComponent<Rigidbody2D>();
	}

	private void OnDisable()
	{
		transform.position = new Vector3(10f, 10f, 0f);
	}

	private void Update()
	{
		if (!IsOutOfScreen())
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
		if (_screenRect.Contains(_mainCamera.WorldToScreenPoint(transform.position)))
		{
			return true;
		}
		return false;
	}

	public void SetPaint(float angle, Vector3 position)
	{
		StopAllCoroutines();
		transform.position = position;
		transform.localRotation = Quaternion.Euler(0f, 0f, angle);
		gameObject.SetActive(true);
	}
}
