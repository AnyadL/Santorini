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

    protected abstract void PlaceWorkers();
    protected abstract void TurnSequence();
    protected abstract void Select();
    protected abstract void Move();
    protected abstract void Build();

    public void PlayTurn()
    {
        TurnSequence();
    }

    public GodStatus GetStatus() { return _status; }
}