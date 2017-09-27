using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AL.ALUtil;

public class TileRunner : ALComponentSingleton<TileRunner> {

	// [SerializeField]
	// private Rigidbody2D _rigid2D = null;

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
			transform.localPosition += (Vector3.left * _speed);
			// _rigid2D.velocity = Vector2.left * _speed;
		// else
			// _rigid2D.velocity = Vector2.zero;
	}

	public void Lerp(float x)
	{
		StartCoroutine("Lerping", transform.localPosition.x - x);
	}

	private IEnumerator Lerping(float x)
	{
		float timer = 0f;
		while(timer <= 1f)
		{
			timer += Time.deltaTime;
			transform.localPosition = ALLerp.Lerp(transform.localPosition, 
										new Vector3(x, transform.localPosition.y, transform.localPosition.z), 
										timer);
			yield return null;
		}
	}

	public void Lerp(Vector3 vector)
	{
		StartCoroutine("Lerping", transform.localPosition - vector);
	}

	private IEnumerator Lerping(Vector3 vector)
	{
		float timer = 0f;
		while(timer <= 1f)
		{
			timer += Time.deltaTime;
			transform.localPosition = ALLerp.Lerp(transform.localPosition, vector, timer);
			yield return null;
		}
	}
}
