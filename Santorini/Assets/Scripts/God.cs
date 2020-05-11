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
    protected GameObject _workerPrefab1 = default;
    protected GameObject _workerPrefab2 = default;

    protected abstract void PlaceWorker(Ground ground);
    protected abstract void TurnSequence(Ground ground);
    protected abstract void Select();
    protected abstract void Move();
    protected abstract void Build();

    public void OnStart(GameObject workerPrefab1, GameObject workerPrefab2)
    {
        _workerPrefab1 = workerPrefab1;
        _workerPrefab2 = workerPrefab2;
    }

    public void PlayTurn(Ground ground)
    {
        // if not done placing, PlaceWorkers
        if (!_donePlacing)
        {
            PlaceWorker(ground);
        }
        else
        {
            TurnSequence(ground);
        }
    }

    public GodStatus GetStatus() { return _status; }
}