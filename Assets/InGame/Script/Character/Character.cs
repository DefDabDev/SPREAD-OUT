using System.Collections;
using System.Collections.Generic;
using AL.ALLog;
using UnityEngine;

public class Character : MonoBehaviour {

    [SerializeField]
    private List<State> _states;

    [SerializeField]
    private string _currentStateName;

    private State _currentState = null;

	void Awake ()
    {
        for (int i = 0; i < _states.Count; ++i)
            _states[i].Init();

        ChangeState(StateNames.runState);
	}
	
	void Update ()
    {
        if (_currentState != null)
            _currentState.Doing();
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch(other.tag)
        {
            case "CurvTile":
                ChangeState(StateNames.clibState);
                break;

            case "Tile":
                
                break;

            default:
                break;
        }
    }
}
