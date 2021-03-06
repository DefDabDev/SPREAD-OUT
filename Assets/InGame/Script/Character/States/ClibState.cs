﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClibState : State {

	[SerializeField]
	private float _speed = 1f;

	[SerializeField]
	private float _radius = 75f;

	[SerializeField]
	private bool _isClimb = false;

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
		// Vector3 target = new Vector3(transform.localPosition.x + _radius, transform.localPosition.y + _radius, 0f);
		TileRunner.instance.Lerp(_radius);
		while(timer <= 1f)
		{
			timer += Time.deltaTime;
			// transform.localPosition = Vector3.Slerp(transform.localPosition, target, timer);
			transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0, 0, 90), timer);
			yield return null;
		}
		_isClimb = true;
	}

	private IEnumerator OffFromWall()
	{
		_isClimb = false;
		collider.isTrigger = true;
		rigid2D.gravityScale = 0f;
		rigid2D.velocity = Vector2.zero;
		float timer = 0f;
		TileRunner.instance.Lerp(310f);
		Vector3 target = new Vector3(transform.localPosition.x, transform.localPosition.y + (335f), 0f);
		while(timer <= 1f)
		{
			timer += Time.deltaTime;
			transform.localPosition = Vector3.Slerp(transform.localPosition, target, timer);
			transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, 0, 0), timer);
			yield return null;
		}
		rigid2D.gravityScale = 1f;
		collider.isTrigger = false;
		_character.ChangeState(StateNames.runState);
	}

	public override void Doing()
    {
		if (_isClimb)
		{
			rigid2D.velocity = Vector3.up * _speed;

			if (!_character.IsPaintTile())
			{
				_character.Die();
			}
		}
    }

	public override void TriggerEnter(Collider2D other)
	{
		if (_isClimb && other.tag.Equals("CurvTile"))
		{
			StartCoroutine("OffFromWall");
		}
	}
}
