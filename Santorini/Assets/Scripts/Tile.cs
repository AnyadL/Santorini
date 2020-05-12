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

    public enum Level
    {
        Ground = 0,
        Level1 = 1,
        Level2 = 2,
        Level3 = 3,
        Dome = 4
    }

    [SerializeField]
    TileNeighbour[] _neighbours;

    GameObject _level1TowerPiece = default;
    GameObject _level2TowerPiece = default;
    GameObject _level3TowerPiece = default;
    GameObject _dome = default;

    float _groundWorkerY = 0.0f;
    float _level1WorkerY = 0.0f;
    float _level2WorkerY = 0.0f;
    float _level3WorkerY = 0.0f;
    float _level1TowerPieceY = 0.0f;
    float _level2TowerPieceY = 0.0f;
    float _level3TowerPieceY = 0.0f;
    float _domeY = 0.0f;

    Worker _workerOnTile = null;
    List<TowerPiece> _towerPiecesOnTile;

    bool _domed = false;

    public void OnStart()
    {
        _towerPiecesOnTile = new List<TowerPiece>();
    }

    public void SetYPositions(float groundWorkerY, float level1WorkerY, float level2WorkerY, float level3WorkerY, float level1TowerPieceY, float level2TowerPieceY, float level3TowerPieceY, float domeY)
    {
        _groundWorkerY = groundWorkerY;
        _level1WorkerY = level1WorkerY;
        _level2WorkerY = level2WorkerY;
        _level3WorkerY = level3WorkerY;
        _level1TowerPieceY = level1TowerPieceY;
        _level2TowerPieceY = level2TowerPieceY;
        _level3TowerPieceY = level3TowerPieceY;
        _domeY = domeY;
    }

    public void SetPrefabs(GameObject level1TowerPiece, GameObject level2TowerPiece, GameObject level3TowerPiece, GameObject dome)
    {
        _level1TowerPiece = level1TowerPiece;
        _level2TowerPiece = level2TowerPiece;
        _level3TowerPiece = level3TowerPiece;
        _dome = dome;
    }

    public Worker GetWorkerOnTile()
    {
        return _workerOnTile;
    }

    public Level GetLevel()
    {
        return (Level) _towerPiecesOnTile.Count;
    }

    public bool IsTileNeighbouring(Tile possibleNeighbour)
    {
        foreach(TileNeighbour tile in _neighbours)
        {
            if (tile.GetTile() == possibleNeighbour)
            {
                return true;
            }
        }

        return false;
    }

    public bool IsTileDirectlyNeighbouring(Tile possibleNeighbour)
    {
        foreach (TileNeighbour tile in _neighbours)
        {
            if (tile.GetTile() == possibleNeighbour && tile.IsDirectlyNeighbouring())
            {
                return true;
            }
        }

        return false;
    }

    public void OnUpdate()
    {
        foreach (TileNeighbour tileNeighbour in _neighbours)
        {
            tileNeighbour.GetTile().transform.Find("TileMesh").gameObject.SetActive(false);
        }

        transform.Find("TileMesh").gameObject.SetActive(false);
    }

    public bool TryPlaceWorker(GameObject workerPrefab)
    {
        if (_workerOnTile != null || _domed)
        {
            Debug.LogError("Tried to place worker on an occupied tile");
            return false;
        }

        if(_towerPiecesOnTile.Count > 0)
        {
            Debug.LogError("Tried to place worker on a level other than the ground");
            return false;
        }

        GameObject newWorker = Instantiate(workerPrefab, new Vector3(transform.position.x, GetWorkerY(), transform.position.z), Quaternion.identity);
        _workerOnTile = newWorker.GetComponent<Worker>();
        _workerOnTile.SetTile(this);
        return true;
    }

    public bool TryMoveWorker(Worker worker, Level previousLevel)
    {
        if(_workerOnTile != null || _domed)
        {
            Debug.LogError("Tried to move worker onto already occupied tile");
            return false;
        }
        
        if(_towerPiecesOnTile.Count > (int) previousLevel + 1)
        {
            Debug.LogError("Worker tried to move up more than one level");
            return false;
        }

        worker.transform.position = new Vector3(transform.position.x, GetWorkerY(), transform.position.z);

        _workerOnTile = worker;
        worker.GetTile().OnWorkerExitTile();
        _workerOnTile.SetTile(this);
        return true;
    }

    public bool TryBuild()
    {
        if(_workerOnTile != null || _domed)
        {
            Debug.LogError("Tried to build on an occupied tile");
            return false;
        }

        GameObject towerPieceGO = Instantiate(GetTowerPiecePrefab(), new Vector3(transform.position.x, GetTowerPieceY(), transform.position.z), Quaternion.identity);
        TowerPiece towerPiece = towerPieceGO.AddComponent<TowerPiece>();
        _towerPiecesOnTile.Add(towerPiece);

        if(_towerPiecesOnTile.Count == 4)
        {
            _domed = true;
        }

        return true;
    }

    public void OnWorkerExitTile()
    {
        _workerOnTile = null;
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

    float GetTowerPieceY()
    {
        switch (_towerPiecesOnTile.Count)
        {
            case 0:
                return _level1TowerPieceY;
            case 1:
                return _level2TowerPieceY;
            case 2:
                return _level3TowerPieceY;
            case 3:
                return _domeY;
            default:
                return -1f;
        }
    }

    GameObject GetTowerPiecePrefab()
    {
        switch (_towerPiecesOnTile.Count)
        {
            case 0:
                return _level1TowerPiece;
            case 1:
                return _level2TowerPiece;
            case 2:
                return _level3TowerPiece;
            case 3:
                return _dome;
            default:
                return null;
        }
    }
}
