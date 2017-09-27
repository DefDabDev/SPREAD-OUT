using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PunchState : State
{
	[SerializeField]
	private Image _star = null;

	[SerializeField]
	private float _blinkTime = 0.3f;

    public override void Init()
    {
		_state = STATE.SLEEP;
        _stateName = StateNames.runState;
		_star.gameObject.SetActive(false);
    }

    public override void OnChange()
    {
		_state = STATE.SLEEP;
        _stateName = StateNames.runState;
    }

    public override void ToChange()
    {
		_state = STATE.SLEEP;
		_star.gameObject.SetActive(false);
    }

	public override void Doing()
    {

    }

	private IEnumerator Blink()
	{
		_star.gameObject.SetActive(true);
		yield return new WaitForSeconds(_blinkTime);
		_character.ChangeState(StateNames.runState);
	}
}
