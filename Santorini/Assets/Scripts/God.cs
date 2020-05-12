using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class God
{
    public enum GodStatus
    {
        Waiting,
        Placing,
        Selecting,
        Moving,
        Building, 
        DoneTurn
    }

    protected GodStatus _status = GodStatus.Waiting;
    protected bool _donePlacing = false;
    protected Worker _selectedWorker = null;

    protected GameObject _workerPrefab1 = default;
    protected GameObject _workerPrefab2 = default;

    protected InputSystem _input = default;
    protected Ground _ground = default;

    protected abstract void OnStartNewTurn();
    protected abstract void PlaceWorker();
    protected abstract void TurnSequence();
    protected abstract void Select();
    protected abstract void Move();
    protected abstract void Build();

    public void OnStart(GameObject workerPrefab1, GameObject workerPrefab2, InputSystem input, Ground ground)
    {
        _workerPrefab1 = workerPrefab1;
        _workerPrefab2 = workerPrefab2;

        _input = input;
        _ground = ground;
    }

    public void PlayTurn()
    {
        if(_status == GodStatus.DoneTurn)
        {
            OnStartNewTurn();
        }

        // if not done placing, PlaceWorkers
        if (!_donePlacing)
        {
            _status = GodStatus.Placing;
            PlaceWorker();
        }
        else
        {
            TurnSequence();
        }
    }

    public GodStatus GetStatus() { return _status; }
}