using System.Collections.Generic;
using UnityEngine;

public class StateMachine 
{
    List<State> _states = default;
    State _currentState = default;

    InputSystem _input = default;
    Board _board = default;
    Worker.Colour _playerColour = default;

    public void Initialize(InputSystem input, Board board, Worker.Colour colour)
    {
        _states = new List<State>();

        _input = input;
        _board = board;
        _playerColour = colour;
    }

    public void RegisterState(State state)
    {
        _states.Add(state);
    }

    public void UpdateCurrentState()
    {
        int newStateIndex = _currentState.UpdateState(_input, _board);

        int stateTransitionCounter = 0;

        while (newStateIndex != -1)
        {
            Debug.Log($"{Time.frameCount}: Machine {_playerColour} -- Leaving State: {(Player.StateId)_currentState.GetStateId()}, Entering State: {(Player.StateId)_states[newStateIndex].GetStateId()}");
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
            _currentState.EnterState(_input, _board);
            newStateIndex = _currentState.UpdateState(_input, _board);
        }
    }

    public int GetCurrentStateId()
    {
        return _currentState.GetStateId();
    }

    public void SetState(int stateId)
    {
        if(_currentState != null)
        {
            _currentState.ExitState();
        }

        _currentState = _states[stateId];
        _currentState.EnterState(_input, _board);
    }
}
