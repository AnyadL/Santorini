using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGod : God
{
    GodStatus _previousStatus = GodStatus.Waiting;
    int _numWorkers = 0;

    protected override void PlaceWorker(Ground ground)
    {
        if(_input.Mouse0ClickedOnBoard())
        {
            if(ground.TryPlaceWorkerOnTile(_workerPrefab1))
            {
                _status = GodStatus.DoneTurn;
                _numWorkers++;
                if(_numWorkers == 2)
                {
                    _donePlacing = true;
                }
            }
        }
    }

    // Base God Turn Sequence:
    //      Select a worker
    //      Move that worker
    //      Build with that worker
    protected override void TurnSequence(Ground ground)
    {
        if (_status == GodStatus.Selecting || (_status == GodStatus.Waiting && _previousStatus == GodStatus.Waiting))
        {
            _previousStatus = _status;
            Select();
        }
        else if (_status == GodStatus.Moving || (_status == GodStatus.Waiting && _previousStatus == GodStatus.Selecting))
        {
            _previousStatus = _status;
            Move();
        }
        else if (_status == GodStatus.Building || (_status == GodStatus.Waiting && _previousStatus == GodStatus.Moving))
        {
            _previousStatus = _status;
            Build();
        }
        else
        {
            _status = GodStatus.DoneTurn;
        }
    }

    protected override void Select()
    {
        _status = GodStatus.Selecting;
        throw new System.NotImplementedException();
    }

    protected override void Move()
    {
        _status = GodStatus.Moving;
        throw new System.NotImplementedException();
    }

    protected override void Build()
    {
        _status = GodStatus.Building;
        throw new System.NotImplementedException();
    }
}