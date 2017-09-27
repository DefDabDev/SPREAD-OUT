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
	private bool _isMove = true;
	public bool isMove {set {_isMove = value;} get {return _isMove;}}

	private void Awake()
	{
		instance = this;
	}


    private void Update()
    {
        if (_isMove && !GameManager.gameEnd)
            transform.localPosition += (Vector3.left * _speed);
        // _rigid2D.velocity = Vector2.left * _speed;
        // else
        // _rigid2D.velocity = Vector2.zero;

        if (Input.GetKeyDown(KeyCode.A))
            GameManager.gameEnd = true;
        if (GameManager.gameEnd)
        {
            Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.localPosition, new Vector3(0, 0, -10800), 1 * Time.deltaTime);
            transform.localPosition = Vector2.Lerp(transform.localPosition, new Vector2(-380, -105), 1 * Time.deltaTime);
        }
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
