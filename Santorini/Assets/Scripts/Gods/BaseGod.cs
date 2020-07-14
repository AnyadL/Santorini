using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGod : God
{
    GodStatus _previousStatus = GodStatus.Waiting;
    int _numWorkers = 0;
    
    // Base God Turn Sequence:
    //      Select a worker
    //      Move that worker
    //      Build with that worker
    protected override void TurnSequence(List<Worker> workers)
    {
        if (_status == GodStatus.Selecting || (_status == GodStatus.Waiting && _previousStatus == GodStatus.Waiting))
        {
            _status = GodStatus.Selecting;
            _previousStatus = GodStatus.Selecting;
            Select(workers);
        }
        else if (_status == GodStatus.Moving || (_status == GodStatus.Waiting && _previousStatus == GodStatus.Selecting))
        {
            _status = GodStatus.Moving;
            _previousStatus = GodStatus.Moving;
            Move(workers);
        }
        else if (_status == GodStatus.Building || (_status == GodStatus.Waiting && _previousStatus == GodStatus.Moving))
        {
            _status = GodStatus.Building;
            _previousStatus = GodStatus.Building;
            Build(workers);
        }
        else
        {
            Debug.LogError("God is in an unknown state");
        }
    }

    protected override void OnStartNewTurn()
    {
        _status = GodStatus.Waiting;
        _previousStatus = GodStatus.Waiting;
    }

    protected override void PlaceWorker(List<Worker> workers)
    {
        //if (_input.Mouse0ClickedOnBoard())
        //{
        //    Tile tile = _ground.GetNearestTiltToLastClick();
        //    if (tile.TryPlaceWorker(_workerPrefab1))
        //    {
        //        tile.GetWorkerOnTile().SetGod(this);
        //        _status = GodStatus.DoneTurn;
        //        _numWorkers++;
        //        if (_numWorkers == 2)
        //        {
        //            _donePlacing = true;
        //            _previousStatus = GodStatus.Waiting;
        //        }
        //    }
        //}
    }

    protected override void Select(List<Worker> workers)
    {
        //if (_input.Mouse0ClickedOnBoard())
        //{
        //    Worker worker = _ground.GetNearestTiltToLastClick().GetWorkerOnTile();
        //    if (GodSelectedOwnWorker(worker))
        //    {
        //        SelectWorker(worker);
        //        _status = GodStatus.Waiting;
        //    }
        //}
    }

    bool GodSelectedOwnWorker(Worker worker)
    {
        // If one of our workers is on the tile, Player has changed selected worker
        return worker != null && worker.GetGod() == this;
    }
    
    void SelectWorker(Worker worker)
    {
        //DeselectWorker();
        //_selectedWorker = worker;
        //_selectedWorker.EnableHighlight();
    }

    void DeselectWorker()
    {
        //if (_selectedWorker != null)
        //{
        //    _selectedWorker.DisableHighlight();
        //}
    }

    protected override void Move(List<Worker> workers)
    {
        //if(_input.Mouse0ClickedOnBoard())
        //{
        //    Tile tile = _ground.GetNearestTiltToLastClick();

        //    if(GodSelectedOwnWorker(tile.GetWorkerOnTile()))
        //    {
        //        SelectWorker(tile.GetWorkerOnTile());
        //        return;
        //    }

        //    if (tile.IsTileDirectlyNeighbouring(_selectedWorker.GetTile()))
        //    {
        //        if(tile.TryMoveWorker(_selectedWorker, _selectedWorker.GetTile().GetLevel()))
        //        {
        //            if (_selectedWorker.GetTile().GetLevel() == Tile.Level.Level3)
        //            {
        //                _status = GodStatus.Won;
        //            }
        //            else
        //            {
        //                _status = GodStatus.Waiting;
        //            }
        //        }
        //    }
        //}
    }

    protected override void Build(List<Worker> workers)
    {
        //if(_input.Mouse0ClickedOnBoard())
        //{
        //    Tile tile = _ground.GetNearestTiltToLastClick();
        //    if (tile.IsTileDirectlyNeighbouring(_selectedWorker.GetTile()))
        //    {
        //        if (tile.TryBuild())
        //        {
        //            _status = GodStatus.DoneTurn;
        //            _selectedWorker.DisableHighlight();
        //        }
        //    }
        //}
    }
}