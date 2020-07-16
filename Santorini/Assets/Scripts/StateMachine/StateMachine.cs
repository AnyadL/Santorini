using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine 
{
    List<State> _states = default;
    State _currentState = default;

    InputSystem _input = default;
    Ground _ground = default;

    public void Initialize(InputSystem input, Ground ground)
    {
        _states = new List<State>();

        _input = input;
        _ground = ground;
    }

    public void RegisterState(State state)
    {
        _states.Add(state);
    }

    public void UpdateCurrentState()
    {
        int newStateIndex = _currentState.UpdateState(_input, _ground);

        int stateTransitionCounter = 0;

        while (newStateIndex != -1)
        {
            ++stateTransitionCounter;
            if(stateTransitionCounter > 15)
            {
                Debug.LogError("Detected possible infinite loop in state machine");
#if UNITY_EDITOR
                UnityEditor.EditorApplication.ExitPlaymode();
#endif
                break;
            }

            _currentState.ExitState();
            _currentState = _states[newStateIndex];
            _currentState.EnterState(_input, _ground);
            newStateIndex = _currentState.UpdateState(_input, _ground);
        }
    }

    public int GetCurrentStateId()
    {
        return _currentState.GetStateId();
    }

    public void SetState(int stateId)
    {
        _currentState = _states[stateId];
    }
}
