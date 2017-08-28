using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintMover : MonoBehaviour {

	[SerializeField]
	private float _speed = 1f;
	
	private Rigidbody2D _rigidBody2D = null;

	private void Awake()
	{
		_rigidBody2D = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate() 
	{
		_rigidBody2D.velocity = transform.up * _speed;
	}

	public void SetPaint(float angle)
	{
		transform.localRotation = Quaternion.Euler(0f, 0f, angle);
	}
}
