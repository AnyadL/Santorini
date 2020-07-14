using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    List<State> _states = default;
    State _currentState = default;

    public void Initialize(State initialState)
    {
        _states = new List<State>() { initialState };
        _currentState = initialState;
    }

    public void RegisterState(State state)
    {
        _states.Add(state);
    }

    public void UpdateCurrentState()
    {
        State newState = _currentState.UpdateState();

        while (newState != null)
        {
            _currentState.ExitState();
            _currentState = newState;
            _currentState.EnterState();
            newState = _currentState.UpdateState();
        }
    }
}
