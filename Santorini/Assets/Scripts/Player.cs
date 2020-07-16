using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public enum StateId
    {
        Waiting = 0,
        Placing = 1,
        Selecting = 2,
        Moving = 3,
        Building = 4,
        DoneTurn = 5
    }

    StateMachine _stateMachine = default;

    protected InputSystem _input = default;
    protected Ground _ground = default;

    God _god = default;
    List<Worker> _workers = default;
    Worker.Colour _colour = default;

    Worker _selectedWorker = default;

    public void Initialize(InputSystem input, Ground ground, Worker.Colour colour)
    {
        _input = input;
        _ground = ground;
        _workers = new List<Worker>();
        _colour = colour;

        _god = new BaseGod();

        _stateMachine = new StateMachine();

        WaitingState waitingState = new WaitingState();
        _stateMachine.Initialize(waitingState, input, ground);

        PlacingState placingState = new PlacingState();
        _stateMachine.RegisterState(placingState);

        SelectingState selectingState = new SelectingState();
        _stateMachine.RegisterState(selectingState);
        
        MovingState movingState = new MovingState();
        _stateMachine.RegisterState(movingState);

        BuildingState buildingState = new BuildingState();
        _stateMachine.RegisterState(buildingState);

        DoneTurnState doneTurnState = new DoneTurnState();
        _stateMachine.RegisterState(doneTurnState);
    }

    public void UpdatePlayer(bool activePlayer)
    {
        if (!activePlayer)
        {
            return;
        }

        _stateMachine.UpdateCurrentState();
    }

    public bool PreventsWin(Player opponent)
    {
        return _god.PreventsWin(opponent);
    }

    public bool HasWon()
    {
        return _god.HasWon(_ground, _workers);
    }

    public bool IsDoneTurn()
    {
        return _stateMachine.GetStateId() == (int)StateId.DoneTurn;
    }

    public void FinalizeTurn()
    {
        if(_god.DonePlacing())
        {
            _god.EnableRealTurns();
        }

        _stateMachine.SetState((int)StateId.Waiting);
    }

    public bool TrySelectWorker(Worker worker)
    {
        if(_workers.Contains(worker))
        {
            _selectedWorker = worker;
            return true;
        }

        return false;
    }

    public Worker GetSelectedWorker()
    {
        return _selectedWorker;
    }

    public God GetGod()
    {
        return _god;
    }

    public List<Worker> GetWorkers()
    {
        return _workers;
    }

    public void AddWorker(Worker worker)
    {
        _workers.Add(worker);
    }

    public Worker.Colour GetColour()
    {
        return _colour;
    }
}
