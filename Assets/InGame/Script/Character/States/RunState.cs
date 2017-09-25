using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : State
{
    [SerializeField]
    private float _jumpScale = 1f;

    [SerializeField]
    private float _groundDisatance = 1f;
    private float _originGroundDistance = 0f;

    [SerializeField]
    private bool _isJump = false;

    public override void Init()
    {
        _state = STATE.SLEEP;
        _stateName = StateNames.runState;
        _originGroundDistance = _groundDisatance;
    }

    public override void OnChange()
    {
        _state = STATE.RUN;
        spriteAnimator.ChangeState(StateNames.runState);
        TileRunner.instance.isMove = true;
    }

    public override void ToChange()
    {
        TileRunner.instance.isMove = false;
        _state = STATE.SLEEP;
    }

    public override void Doing()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            NormalAction();

#if UNITY_EDITOR
        Debug.DrawLine(transform.position + (Vector3.down * 1.2f), transform.position + (Vector3.down * 1.2f) + (Vector3.down * _originGroundDistance), Color.red);
        Debug.DrawLine(transform.position + (Vector3.down * 1.2f), transform.position + (Vector3.down * 1.2f) + (Vector3.down * _groundDisatance), Color.green);
#endif

        RaycastHit2D hit = Physics2D.Raycast(transform.position + (Vector3.down * 1.2f), Vector3.down, _groundDisatance);
        if (hit)
        {
            Debug.Log(hit.collider.name);
            _isJump = false;
        }
        else
        {
            _isJump = true;
        }

        if (_isJump)
            _groundDisatance = rigid2D.velocity.y < 0 ? _originGroundDistance : 0.001f;
    }

    public override void NormalAction()
    {
        if (_isJump)
            return;

        rigid2D.velocity = new Vector3(rigid2D.velocity.x, _jumpScale);
        _groundDisatance = _originGroundDistance;
        _isJump = true;
    }

    public override void PaintAction()
    {

    }
}
