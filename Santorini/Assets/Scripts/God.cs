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
        DoneTurn,
        Won
    }

    protected GodStatus _status = GodStatus.Waiting;
    protected bool _donePlacing = false;
    
    protected abstract void OnStartNewTurn();
    protected abstract void PlaceWorker(List<Worker> workers);
    protected abstract void TurnSequence(List<Worker> workers);
    protected abstract void Select(List<Worker> workers);
    protected abstract void Move(List<Worker> workers);
    protected abstract void Build(List<Worker> workers);
    
    public void PlayTurn(List<Worker> workers)
    {
        if(_status == GodStatus.DoneTurn)
        {
            OnStartNewTurn();
        }

        // if not done placing, PlaceWorkers
        if (!_donePlacing)
        {
            _status = GodStatus.Placing;
            PlaceWorker(workers);
        }
        else
        {
            TurnSequence(workers);
        }
    }

    public GodStatus GetStatus() { return _status; }
}