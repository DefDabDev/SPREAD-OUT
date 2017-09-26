using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum STATE
{
    RUN,
    SLEEP,
}

public static class StateNames
{
    public const string runState = "RunState";
    public const string jumpState = "JumpState";
    public const string clibState = "ClibState";
    public const string deadState = "DeadState";
}

public abstract class State : MonoBehaviour {

    /// <summary>
    /// 현재 State의 상태
    /// </summary>
    [SerializeField]
    protected STATE _state = STATE.SLEEP;
    public STATE state { get { return _state; } }

    [SerializeField]
    protected Character _character = null;
    public Character charcter {get {return _character;}}

    /// <summary>
    /// State 이름
    /// </summary>
    [SerializeField]
    protected string _stateName;
    public string stateName { get { return _stateName; } }

    private SpriteAnimator _spriteAnimator = null;
    public SpriteAnimator spriteAnimator { get { return _spriteAnimator ?? (_spriteAnimator = GetComponent<SpriteAnimator>()); } }

    private Rigidbody2D _rigidBody2D = null;
    public Rigidbody2D rigid2D {get {return _rigidBody2D ?? (_rigidBody2D = GetComponent<Rigidbody2D>());}}

    private BoxCollider2D _collider = null;
    public new BoxCollider2D collider {get {return _collider ?? (_collider = GetComponent<BoxCollider2D>());}}

    /// <summary>
    /// 이전 State에서 현재 State로 넘어갈 때 할일
    /// </summary>
    public abstract void OnChange();

    /// <summary>
    /// 현재 State 다음 State로 넘어갈 때 할일
    /// </summary>
    public abstract void ToChange();

    /// <summary>
    /// 업데이트
    /// </summary>
    public abstract void Doing();

    /// <summary>
    /// State 초기화
    /// </summary>
    public abstract void Init();

    /// <summary>
    /// 페인트가 없는 블록에서의 액션
    /// </summary>
    public virtual void NormalActionUp() {}

    /// <summary>
    /// 페인트가 있는 블록에서의 액션
    /// </summary>
    public virtual void PaintActionUp() {}

    /// <summary>
    /// 페인트가 없는 블록에서의 액션
    /// </summary>
    public virtual void NormalActionPress() {}

    /// <summary>
    /// 페인트가 있는 블록에서의 액션
    /// </summary>
    public virtual void PaintActionPress() {}

    /// <summary>
    /// 페인트가 없는 블록에서의 액션
    /// </summary>
    public virtual void NormalActionDown() {}

    /// <summary>
    /// 페인트가 있는 블록에서의 액션
    /// </summary>
    public virtual void PaintActionDown() {}

    public virtual void TriggerEnter(Collider2D other) {}
    public virtual void TriggerStay(Collider2D other) {}
    public virtual void TriggerExit(Collider2D other) {}

    public virtual void CollisionEnter(Collision2D other) {}
    public virtual void CollisionStay(Collision2D other) {}
    public virtual void CollisionExit(Collision2D other) {}
 }
