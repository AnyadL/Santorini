using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

/// <summary>
/// There are 25 tiles on a Santorini board. Each tile has 8 neighbours, 3-8 of which are directly neighbouring. Only certain God powers allow a Player to move their Worker using indirect neighbours.
/// </summary>
public class Tile : MonoBehaviour
{
    [System.Serializable]
    public class TileNeighbour
    {
        public enum Direction
        {
            North,
            NorthEast,
            East,
            SouthEast,
            South,
            SouthWest,
            West,
            NorthWest
        }

        [SerializeField]
        Tile _tile;

        [SerializeField]
        Direction _direction;

        [SerializeField]
        bool _directlyNeighbouring = false;

        public Tile GetTile()
        {
            return _tile;
        }
        
        public Direction GetDirection()
        {
            return _direction;
        }

        public bool IsDirectlyNeighbouring()
        {
            return _directlyNeighbouring;
        }
    }

    [SerializeField]
    TileNeighbour[] _neighbours;

    float _groundWorkerY = 0.0f;
    float _level1WorkerY = 0.0f;
    float _level2WorkerY = 0.0f;
    float _level3WorkerY = 0.0f;
    float _level1TowerPieceY = 0.0f;
    float _level2TowerPieceY = 0.0f;
    float _level3TowerPieceY = 0.0f;
    float _domeY = 0.0f;

    Worker _workerOnTile = null;
    List<ITowerPiece> _towerPiecesOnTile;

    bool _domed = false;

    public void OnStart(float groundWorkerY, float level1WorkerY, float level2WorkerY, float level3WorkerY, float level1TowerPieceY, float level2TowerPieceY, float level3TowerPieceY, float domeY)
    {
        _groundWorkerY = groundWorkerY;
        _level1WorkerY = level1WorkerY;
        _level2WorkerY = level2WorkerY;
        _level3WorkerY = level3WorkerY;
        _level1TowerPieceY = level1TowerPieceY;
        _level2TowerPieceY = level2TowerPieceY;
        _level3TowerPieceY = level3TowerPieceY;
        _domeY = domeY;

        _towerPiecesOnTile = new List<ITowerPiece>();
    }

    public bool TryPlaceWorker(GameObject workerPrefab)
    {
        if (_workerOnTile != null || _domed)
        {
            Debug.LogError("Tried to place worker on an occupied tile");
            return false;
        }

        GameObject newWorker = Instantiate(workerPrefab, new Vector3(transform.position.x, GetWorkerY(), transform.position.z), Quaternion.identity);
        _workerOnTile = newWorker.GetComponent<Worker>();
        return true;
    }

    public void OnUpdate()
    {
        foreach (TileNeighbour tileNeighbour in _neighbours)
        {
            tileNeighbour.GetTile().transform.Find("TileMesh").gameObject.SetActive(false);
        }

        transform.Find("TileMesh").gameObject.SetActive(false);
    }

    public void OnMoveWorker(GameObject worker, int previousLevel)
    {
        if(_workerOnTile != null)
        {
            Debug.LogError("Tried to move worker onto already occupied tile");
        }

        if(_towerPiecesOnTile.Select(t => t.GetLevel()).Max() > previousLevel + 1)
        {
            Debug.LogError("Worker tried to move up more than one level");
        }

        worker.transform.position = new Vector3(transform.position.x, GetWorkerY(), transform.position.z);

        _workerOnTile = worker.GetComponent<Worker>();
    }

    public void OnBuild(ITowerPiece towerPiece)
    {
        if(_workerOnTile != null)
        {
            Debug.LogError("Tried to build on top of worker");
        }

        if(_towerPiecesOnTile.Count == 0)
        {
            if(towerPiece.GetLevel() == 1)
            {
                _towerPiecesOnTile.Add(towerPiece);
            }
        }
        else
        {
            int maxLevel = _towerPiecesOnTile.Select(t => t.GetLevel()).Max();
            if(maxLevel + 1 == towerPiece.GetLevel())
            {
                _towerPiecesOnTile.Add(towerPiece);
            }
            else
            {
                Debug.LogError("Tried to build an invalid piece");
            }
        }

        if(towerPiece.GetType() == typeof(TowerPiece_Dome))
        {
            _domed = true;
        }
    }

    float GetWorkerY()
    {
        switch (_towerPiecesOnTile.Count)
        {
            case 0:
                return _groundWorkerY;
            case 1:
                return _level1WorkerY;
            case 2:
                return _level2WorkerY;
            case 3:
                return _level3WorkerY;
            default:
                return -1f;
        }
    }
}
