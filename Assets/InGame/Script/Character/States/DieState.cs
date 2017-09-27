using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieState : State
{
	[SerializeField]
	private float _jumpPower;

    public override void Init()
    {

    }

    public override void OnChange()
    {
		collider.isTrigger = true;
		rigid2D.gravityScale = 1.2f;
		rigid2D.AddForce(Vector2.up * _jumpPower);
		spriteAnimator.ChangeState(StateNames.dieState);

		for (int index = 0; index <= 9; ++index)
			Physics2D.IgnoreLayerCollision(10, index, true);
    }

    public override void ToChange()
    {
    }

	public override void Doing()
    {
    }
}
