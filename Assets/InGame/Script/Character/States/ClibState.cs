using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClibState : State {

	[SerializeField]
	private float _radius = 75f;

    public override void Init()
    {
		_state = STATE.SLEEP;
		_stateName = StateNames.clibState;
    }

    public override void OnChange()
    {
		_state = STATE.RUN;
		rigid2D.gravityScale = 0f;
		StartCoroutine("OnToWall");
		Debug.Log("Clib State!");
    }

    public override void ToChange()
    {
		_state = STATE.SLEEP;
		rigid2D.gravityScale = 1f;	
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
	}

	public override void Doing()
    {
    }

    public override void NormalAction()
    {
    }

    public override void PaintAction()
    {
    }
}
