using System;
using System.Collections;
using System.Collections.Generic;
using AL.ALUtil;
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

    private bool _isIn = false;

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
        StopAllCoroutines();

        if (_isIn)
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + 130f, transform.localPosition.z);

        _isIn = false;
        _isJump = false;
        _character.spray.isLock = false;
    }

    public override void Doing()
    {
#if UNITY_EDITOR
        Debug.DrawLine(transform.position + (Vector3.down * 1.2f), transform.position + (Vector3.down * 1.2f) + (Vector3.down * _originGroundDistance), Color.red);
        Debug.DrawLine(transform.position + (Vector3.down * 1.2f), transform.position + (Vector3.down * 1.2f) + (Vector3.down * _groundDisatance), Color.green);
#endif

        if (Physics2D.Raycast(transform.position + (Vector3.down * 1.2f), Vector3.down, _groundDisatance))
        {
            _isJump = false;
            spriteAnimator.ChangeStateNotInit(StateNames.runState);
        }
        else
        {
            _isJump = true;
        }

        if (_isJump)
            _groundDisatance = rigid2D.velocity.y < 0 ? _originGroundDistance : 0.001f;
    }

    public override void NormalActionDown()
    {
        if (_isJump)
            return;

        rigid2D.velocity = new Vector3(rigid2D.velocity.x, _jumpScale);
        _groundDisatance = _originGroundDistance;
        _isJump = true;
        spriteAnimator.ChangeState(StateNames.jumpState);
    }

    public override void PaintActionDown()
    {
        if (_isJump || !charcter.IsPaintTile())
            return;
        
        StartCoroutine("In");
    }

    public override void PaintActionUp()
    {
        StopAllCoroutines();
        StartCoroutine("Out");
    }


    private IEnumerator In()
    {
        if (_isIn)
            yield break;

        rigid2D.gravityScale = 0f;
        collider.isTrigger = true;
        charcter.spray.isLock = true;
        _isIn = true;
        float timer = 0f;
        Vector3 target = new Vector3(transform.localPosition.x, transform.localPosition.y - 130f, transform.localPosition.z);
        while(timer <= 1f)
        {
            timer += Time.deltaTime * 2.5f;
            transform.localPosition = ALLerp.Lerp(transform.localPosition, target, timer);
            yield return null;
        }
    }

    private IEnumerator Out()
    {
        if (!_isIn)
            yield break;

        float timer = 0f;
        Vector3 target = new Vector3(transform.localPosition.x, transform.localPosition.y + 130f, transform.localPosition.z);
        while(timer <= 1f)
        {
            timer += Time.deltaTime * 2.5f;
            transform.localPosition = ALLerp.Lerp(transform.localPosition, target, timer);
            yield return null;
        }
        collider.isTrigger = false;
        rigid2D.gravityScale = 1f;
        _isIn = false;
        charcter.spray.isLock = false;
    }

    public override void CollisionEnter(Collision2D other)
    {
        if (other.gameObject.tag.Equals("Tile"))
        {
        }
    }

    public override void TriggerEnter(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Tile") && other.transform.localPosition.z < 1f)
        {
            if (_isIn)
            {
                StartCoroutine("Out");
            }
        }
    }
}
