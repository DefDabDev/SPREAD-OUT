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
    public const string deadState = "DeadState";
}

public abstract class State : MonoBehaviour {

    /// <summary>
    /// 현재 State의 상태
    /// </summary>
    [SerializeField]
    protected STATE _state = STATE.SLEEP;
    public STATE state { get { return _state; } }

    /// <summary>
    /// State 이름
    /// </summary>
    [SerializeField]
    protected string _stateName;
    public string stateName { get { return _stateName; } }

    private SpriteAnimator _spriteAnimator = null;
    public SpriteAnimator spriteAnimator { get { return _spriteAnimator ?? (_spriteAnimator = GetComponent<SpriteAnimator>()); } }

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
}
