using System.Collections;
using System.Collections.Generic;
using AL.ALLog;
using UnityEngine;

public class Character : MonoBehaviour {

    [SerializeField]
    private SprayShooter _spray = null;
    public SprayShooter spray {get {return _spray;}}

    [SerializeField]
    private List<State> _states;

    [SerializeField]
    private string _currentStateName = string.Empty;

    private State _currentState = null;

    public bool isAttacking {set;get;}

	private void Awake ()
    {
        isAttacking = false;
        InitCharacter();
        ChangeState(StateNames.runState);
	}

    private void InitCharacter()
    {
        for (int i = 0; i < _states.Count; ++i)
            _states[i].Init();

        for (int index = 0; index <= 9; ++index)
			Physics2D.IgnoreLayerCollision(10, index, false);
    }

    private void Start()
    {
        TileRunner.instance.isMove = true;
    }
	
	private void Update ()
    {
#if UNITY_EDITOR
        DebugAction();
#endif

        if (transform.localPosition.y < -600f || transform.localPosition.x < -1100f)
        {
            GameManager.instance.GameEnd();
        }

        if (_currentState != null)
            _currentState.Doing();
	}

    private void DebugAction()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            ActionDown();

        if (Input.GetKey(KeyCode.Space))
            ActionPress();

        if (Input.GetKeyUp(KeyCode.Space))
            ActionUp();
    }

    public State GetState(string name)
    {
        for (int i = 0; i < _states.Count; ++i)
            if (_states[i].stateName.Equals(name))
                return _states[i];

        ALLog.ErrorLog(string.Format("Can't Find State! Name ({0})", name));
        return null;
    }

    public void ChangeState(string name)
    {
        ChangeState(GetState(name));
    }
    
    public void ChangeState(State state)
    {
        if (_currentState != null && _currentState.Equals(state))
            return;

        if (_currentState != null)
            _currentState.ToChange();
            
        _currentState = state;
        _currentState.OnChange();
    }

    public void ActionUp()
    {
        if (IsPaintTile()) _currentState.PaintActionUp();
        else _currentState.NormalActionUp();
    }

    public void ActionPress()
    {
        if (IsPaintTile()) _currentState.PaintActionPress();
        else _currentState.NormalActionPress();
    }

    public void ActionDown()
    {
        if (IsPaintTile()) _currentState.PaintActionDown();
        else _currentState.NormalActionDown();
    }

    public bool IsPaintTile()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + (transform.up * -1.2f), -transform.up, 0.2f);
        
        if (!hit) return false;
        return hit.transform.localPosition.z.Equals(0f) ? false : true;
    }

    public bool IsPaintTile(Vector3 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + (direction * -1.2f), direction, 0.2f);
        
        if (!hit) return false;
        return hit.transform.localPosition.z.Equals(0f) ? false : true;
    }

    public void Die()
    {
        ChangeState(StateNames.dieState);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch(other.tag)
        {
            case "CurvTile":
                
                if (other.transform.localPosition.z < 1f)
                    Die();

                ChangeState(StateNames.clibState);
                break;

            case "Monster":
                if (!isAttacking)
                    Die();
                return;

            default:
                break;
        }

        _currentState.TriggerEnter(other);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        _currentState.TriggerStay(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _currentState.TriggerExit(other);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        switch(other.gameObject.tag)
        {
            case "Monster":
                if (!isAttacking)
                    Die();
                return;
        }

        _currentState.CollisionEnter(other);
    }

    private void OnCollisionStayr2D(Collision2D other)
    {
        _currentState.CollisionStay(other);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        _currentState.CollisionExit(other);
    }
}
