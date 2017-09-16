using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : State
{
    public override void Init()
    {
        _state = STATE.SLEEP;
        _stateName = StateNames.runState;
    }

    public override void OnChange()
    {
        _state = STATE.RUN;
        spriteAnimator.ChangeState(StateNames.runState);
    }

    public override void ToChange()
    {

    }

    public override void Doing()
    {
        if (_state.Equals(STATE.RUN))
        {

        }
    }
}
