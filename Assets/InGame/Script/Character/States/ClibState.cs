using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClibState : State {

	[SerializeField]
	private float _speed = 1f;

	[SerializeField]
	private float _radius = 75f;

	[SerializeField]
	private bool _isClimb = false;

	private bool _flag = false;

  	public override void Init()
    {
		_state = STATE.SLEEP;
		_stateName = StateNames.clibState;
		_isClimb = false;
    }

    public override void OnChange()
    {
		_state = STATE.RUN;
		rigid2D.gravityScale = 0f;
		StartCoroutine("OnToWall");
		_isClimb = false;
		_flag = false;
    }

    public override void ToChange()
    {
		_state = STATE.SLEEP;
		rigid2D.gravityScale = 1f;
		_isClimb = false;
    }

	private IEnumerator OnToWall()
	{
		float timer = 0f;
		Vector3 target = new Vector3(transform.localPosition.x + _radius, transform.localPosition.y + _radius, 0f);
		while(timer <= 1f)
		{
			timer += Time.deltaTime;
			transform.localPosition = Vector3.Lerp(transform.localPosition, target, timer);
			transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, 0, 90), timer);
			yield return null;
		}
		_isClimb = true;
	}

	private IEnumerator OffFromWall()
	{
		float timer = 0f;
		Vector3 target = new Vector3(transform.localPosition.x - _radius, transform.localPosition.y - _radius, 0f);
		while(timer <= 1f)
		{
			timer += Time.deltaTime;
			transform.localPosition = Vector3.Lerp(transform.localPosition, target, timer);
			transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, 0, 0), timer);
			yield return null;
		}
		_character.ChangeState(StateNames.runState);
	}

	public override void Doing()
    {
		if (_isClimb)
		{
			rigid2D.velocity = Vector3.up * _speed;

			if (!_character.IsPaintTile())
			{
				Debug.Log("Game Over");
			}
		}
    }

	public override void TriggerEnter(Collider2D other)
	{
		if (_isClimb && other.tag.Equals("Player"))
		{
			StartCoroutine("OffFromWall");
		}
	}
}
