using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class God : MonoBehaviour
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
    protected Worker _selectedWorker = null;
    
    protected InputSystem _input = default;
    protected Board _board = default;

    protected Worker.Colour _workerColour = default;

    protected abstract void OnStartNewTurn();
    protected abstract IEnumerator PlaceWorker();
    protected abstract void TurnSequence();
    protected abstract void Select();
    protected abstract void Move();
    protected abstract void Build();

    public void OnStart(InputSystem input, Board board, Worker.Colour workerColour)
    {
        _workerColour = workerColour;

        _input = input;
        _board = board;
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
            StartCoroutine(PlaceWorker());
        }
        else
        {
            TurnSequence();
        }
    }

    public GodStatus GetStatus() { return _status; }
}