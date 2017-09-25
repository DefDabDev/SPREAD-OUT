using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AL.ALUtil;

public class TileRunner : ALComponentSingleton<TileRunner> {

	[SerializeField]
	private Rigidbody2D _rigid2D = null;

	[SerializeField]
	private float _speed = 1f;

	[SerializeField]
	private bool _isMove = false;
	public bool isMove {set {_isMove = value;} get {return _isMove;}}

	private void Awake()
	{
		instance = this;
	}

	private void Update()
	{
		if (_isMove)
			_rigid2D.velocity = Vector2.left * _speed;
		else
			_rigid2D.velocity = Vector2.zero;
	}
}
