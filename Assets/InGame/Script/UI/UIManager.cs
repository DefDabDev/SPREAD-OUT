using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AL.ALUtil;

public class UIManager : ALComponentSingleton<UIManager> {

	[SerializeField]
	private Character _character;

	private void Awake () 
	{
		instance = this;
	}

	public void Attack()
	{
		_character.ChangeState(StateNames.punchState);
	}

	public void ActionUp()
	{
		_character.ActionUp();
	}

	public void ActionPress()
	{
		_character.ActionPress();
	}

	public void ActionDown()
	{
		_character.ActionDown();
	}
}
