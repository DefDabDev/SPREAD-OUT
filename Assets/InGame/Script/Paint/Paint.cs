using System.Collections;
using System.Collections.Generic;
using AL.ALUtil;
using UnityEngine;
using UnityEngine.UI;



public class Paint : MonoBehaviour {

	[SerializeField]
	private Sprite[] _paints;

	[SerializeField]
	private float _speed = 1f;

	private Image _image = null;
	private Rigidbody2D _rigidBody2D = null;
	private Camera _mainCamera = null;
	private Transform _targetTile = null;
	private BoxCollider2D _collider = null;
	private readonly Vector2 _bulletSize = new Vector2(25f, 82f);
	private readonly Vector2 _floorSize = new Vector2(150f, 150f);
	private readonly Rect _screenRect = new Rect(0f, 0f, 1920f, 1080f);

	private void Awake()
	{
		_mainCamera = Camera.main;
		_collider = GetComponent<BoxCollider2D>();
		_rigidBody2D = GetComponent<Rigidbody2D>();
		_image = GetComponent<Image>();
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
		if (_targetTile == null)
		{
			_rigidBody2D.velocity = transform.up * _speed;
		}
		else
		{
			transform.position = _targetTile.position;
		}
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
		_image.sprite = _paints[(int)PaintIndex.BULLET];
		_image.rectTransform.sizeDelta = _bulletSize;
		_collider.size = _bulletSize;
		_targetTile = null;
		_rigidBody2D.bodyType = RigidbodyType2D.Dynamic;
		_image.fillAmount = 1f;
		gameObject.SetActive(true);
	}
}
