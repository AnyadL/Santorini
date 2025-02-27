﻿using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// There are 25 tiles on a Santorini board. Each tile has 8 neighbours, 3-8 of which are directly neighbouring. 
/// Only some God powers allow a Player to move their Worker using indirect neighbours.
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

#pragma warning disable 0649
        [SerializeField]
        Tile _tile;

        [SerializeField]
        Direction _direction;
#pragma warning restore 0649

        [SerializeField]
        bool _directlyNeighbouring = false; //A1 and A2 are directly neighbouring. A1 and A5 are indirectly neighbouring
        
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
        Board = 0,
        Level1 = 1,
        Level2 = 2,
        Level3 = 3,
        Dome = 4
    }

#pragma warning disable 0649
    [SerializeField]
    TileNeighbour[] _neighbours;
#pragma warning restore 0649

    GameObject _level1TowerPiece = default;
    GameObject _level2TowerPiece = default;
    GameObject _level3TowerPiece = default;
    GameObject _dome = default;

    GameObject _level1TowerPieceGhost = default;
    GameObject _level2TowerPieceGhost = default;
    GameObject _level3TowerPieceGhost = default;
    GameObject _domeGhost = default;

    GameObject _towerPieceGhost = null;
    GameObject _workerGhost = null;

    float _boardWorkerY = 0.0f;
    float _level1WorkerY = 0.0f;
    float _level2WorkerY = 0.0f;
    float _level3WorkerY = 0.0f;
    float _level1TowerPieceY = 0.0f;
    float _level2TowerPieceY = 0.0f;
    float _level3TowerPieceY = 0.0f;
    float _domeY = 0.0f;
        
    float _domeOnGroundOffset = -3.3f;
    float _domeOnLevel1Offset = -3.3f;
    float _domeOnLevel2Offset = -1.8f;

    Worker _workerOnTile = null;
    List<TowerPiece> _towerPiecesOnTile;

    bool _domed = false;

    public void OnStart()
    {
        _towerPiecesOnTile = new List<TowerPiece>();
    }

    public void SetYPositions(float boardWorkerY, float level1WorkerY, float level2WorkerY, float level3WorkerY, float level1TowerPieceY, float level2TowerPieceY, float level3TowerPieceY, float domeY)
    {
        _boardWorkerY = boardWorkerY;
        _level1WorkerY = level1WorkerY;
        _level2WorkerY = level2WorkerY;
        _level3WorkerY = level3WorkerY;
        _level1TowerPieceY = level1TowerPieceY;
        _level2TowerPieceY = level2TowerPieceY;
        _level3TowerPieceY = level3TowerPieceY;
        _domeY = domeY;
    }

    public void SetTowerPiecePrefabs(GameObject level1TowerPiece, GameObject level2TowerPiece, GameObject level3TowerPiece, GameObject dome)
    {
        _level1TowerPiece = level1TowerPiece;
        _level2TowerPiece = level2TowerPiece;
        _level3TowerPiece = level3TowerPiece;
        _dome = dome;
    }

    public void SetTowerPieceGhostPrefabs(GameObject level1TowerPiece, GameObject level2TowerPiece, GameObject level3TowerPiece, GameObject dome)
    {
        _level1TowerPieceGhost = level1TowerPiece;
        _level2TowerPieceGhost = level2TowerPiece;
        _level3TowerPieceGhost = level3TowerPiece;
        _domeGhost = dome;
    }

    public Worker GetWorkerOnTile()
    {
        return _workerOnTile;
    }

    public bool HasWorkerOnTile()
    {
        return _workerOnTile != null;
    }

    public Level GetLevel()
    {
        return (Level) _towerPiecesOnTile.Count;
    }

    public TileNeighbour[] GetTileNeighbours()
    {
        return _neighbours;
    }

    public int GetTowerPieceCount()
    {
        return _towerPiecesOnTile.Count;
    }

    public bool IsDomed()
    {
        return _domed;
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
        DestroyGhosts();
    }

    public Worker PlaceWorker(GameObject workerPrefab, Worker.Gender gender, Worker.Colour colour)
    {
        GameObject newWorker = Instantiate(workerPrefab, new Vector3(transform.position.x, GetWorkerY(), transform.position.z), Quaternion.identity);
        _workerOnTile = newWorker.GetComponent<Worker>();
        _workerOnTile.Initialize(gender, colour);
        _workerOnTile.SetTile(this);
        return _workerOnTile;
    }

    public void AddWorker(Worker worker)
    {
        _workerOnTile = worker;
        _workerOnTile.transform.position = new Vector3(transform.position.x, GetWorkerY(), transform.position.z);
    }

    public void AddGhostWorker(GameObject workerGhost)
    {
        // hide workers
        if(_workerOnTile)
        {
            _workerOnTile.gameObject.SetActive(false);
        }

        _workerGhost = Instantiate(workerGhost, new Vector3(transform.position.x, GetWorkerY(), transform.position.z), Quaternion.identity);
    }

    public void RemoveWorker()
    {
        _workerOnTile = null;
    }

    public void AddTowerPiece()
    {
        GameObject towerPieceGO = Instantiate(GetTowerPiecePrefab(), new Vector3(transform.position.x, GetTowerPieceY(), transform.position.z), Quaternion.identity);
        TowerPiece towerPiece = towerPieceGO.AddComponent<TowerPiece>();
        _towerPiecesOnTile.Add(towerPiece);

        if (_towerPiecesOnTile.Count == 4)
        {
            _domed = true;
        }
    }

    public void AddTowerGhostPiece()
    {
        _towerPieceGhost = Instantiate(GetTowerPieceGhostPrefab(), new Vector3(transform.position.x, GetTowerPieceY(), transform.position.z), Quaternion.identity);      
    }

    public void DestroyGhosts()
    {
        if(_towerPieceGhost != null)
        {
            Destroy(_towerPieceGhost);
        }

        if(_workerGhost != null)
        {
            Destroy(_workerGhost);
        }

        // unhide workers
        if(_workerOnTile)
        {
            _workerOnTile.gameObject.SetActive(true);
        }
    }

    public void AddSpecificPiece(int pieceLevel)
    {
        // Domes are a different height than other tile pieces and need to be offset
        float offset = 0.0f;
        if(pieceLevel == 3)
        {
            if(_towerPiecesOnTile.Count == 0) { offset = _domeOnGroundOffset; }
            else if (_towerPiecesOnTile.Count == 1) { offset = _domeOnLevel1Offset; }
            else if (_towerPiecesOnTile.Count == 2) { offset = _domeOnLevel2Offset; }
        }
        
        GameObject towerPieceGO = Instantiate(GetSpecificTowerPiecePrefab(pieceLevel), new Vector3(transform.position.x, GetTowerPieceY() + offset, transform.position.z), Quaternion.identity);
        TowerPiece towerPiece = towerPieceGO.AddComponent<TowerPiece>();
        _towerPiecesOnTile.Add(towerPiece);

        if (_towerPiecesOnTile.Count == 4 || pieceLevel == 3)
        {
            _domed = true;
        }
    }

    public void AddSpecificGhostPiece(int pieceLevel)
    {
        // Domes are a different height than other tile pieces and need to be offset
        float offset = 0.0f;
        if(pieceLevel == 3)
        {
            if(_towerPiecesOnTile.Count == 0) { offset = _domeOnGroundOffset; }
            else if (_towerPiecesOnTile.Count == 1) { offset = _domeOnLevel1Offset; }
            else if (_towerPiecesOnTile.Count == 2) { offset = _domeOnLevel2Offset; }
        }
        
        _towerPieceGhost = Instantiate(GetSpecificTowerPieceGhostPrefab(pieceLevel), new Vector3(transform.position.x, GetTowerPieceY() + offset, transform.position.z), Quaternion.identity);
    }

    public void RemoveTowerPiece()
    {
        GameObject removedTowerPiece = _towerPiecesOnTile[_towerPiecesOnTile.Count - 1].gameObject;
        _towerPiecesOnTile.RemoveAt(_towerPiecesOnTile.Count - 1);
        Destroy(removedTowerPiece);

        // No matter how high the tower was, if you remove the top item you can guarantee it is no longer domed
        _domed = false;
    }
    public bool NextTowerPieceIsLevel1()
    {
        return _towerPiecesOnTile.Count == 0;
    }
    public bool NextTowerPieceIsLevel2()
    {
        return _towerPiecesOnTile.Count == 1;
    }

    public bool NextTowerPieceIsLevel3()
    {
        return _towerPiecesOnTile.Count == 2;
    }

    public bool NextTowerPieceIsDome()
    {
        return _towerPiecesOnTile.Count == 3;
    }

    float GetWorkerY()
    {
        switch (_towerPiecesOnTile.Count)
        {
            case 0:
                return _boardWorkerY;
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

    GameObject GetTowerPieceGhostPrefab()
    {
        switch (_towerPiecesOnTile.Count)
        {
            case 0:
                return _level1TowerPieceGhost;
            case 1:
                return _level2TowerPieceGhost;
            case 2:
                return _level3TowerPieceGhost;
            case 3:
                return _domeGhost;
            default:
                return null;
        }
    }

    GameObject GetSpecificTowerPiecePrefab(int pieceLevel)
    {
        switch (pieceLevel)
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
        
    GameObject GetSpecificTowerPieceGhostPrefab(int pieceLevel)
    {
        switch (pieceLevel)
        {
            case 0:
                return _level1TowerPieceGhost;
            case 1:
                return _level2TowerPieceGhost;
            case 2:
                return _level3TowerPieceGhost;
            case 3:
                return _domeGhost;
            default:
                return null;
        }
    }
}