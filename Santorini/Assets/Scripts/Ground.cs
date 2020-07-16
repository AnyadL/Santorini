using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class Ground : MonoBehaviour
{
    [SerializeField]
    Tile[] _tiles = null;

    [Header("Tower Prefabs")]
    [SerializeField]
    GameObject _tower1Piece = default;
    [SerializeField]
    GameObject _tower2Piece = default;
    [SerializeField]
    GameObject _tower3Piece = default;
    [SerializeField]
    GameObject _dome = default;

    [System.SerializableAttribute]
    struct WorkerPrefab
    {
#pragma warning disable 0649
        public Worker.Gender gender;
        public Worker.Colour colour;
        public GameObject prefab;
#pragma warning restore 0649
    }

    [Header("Worker Prefabs")]
    [SerializeField]
    List<WorkerPrefab> _workerPrefabs = default;

    [Header("Positions")]
    [SerializeField]
    Transform _groundWorkerPosition = default;
    [SerializeField]
    Transform _level1WorkerPosition = default;
    [SerializeField]
    Transform _level2WorkerPosition = default;
    [SerializeField]
    Transform _level3WorkerPosition = default;
    [SerializeField]
    Transform _level1TowerPiecePosition = default;
    [SerializeField]
    Transform _level2TowerPiecePosition = default;
    [SerializeField]
    Transform _level3TowerPiecePosition = default;
    [SerializeField]
    Transform _domePosition = default;

    List<Player> _players = default;
    Player _activePlayer = default;

    public void OnStart(List<Player> players)
    {
        for(int i = 0; i < _workerPrefabs.Count; ++i)
        {
            for(int j = 0; j < _workerPrefabs.Count; ++j)
            {
                if (i == j) { continue; }
                if (_workerPrefabs[i].gender == _workerPrefabs[j].gender && _workerPrefabs[i].colour == _workerPrefabs[j].colour)
                {
                    Debug.LogError("Worker prefab list defines the same worker twice.");
                }
            }
        }

        _players = players;
        
        foreach (Tile tile in _tiles)
        {
            tile.OnStart();
            tile.SetYPositions(_groundWorkerPosition.position.y, _level1WorkerPosition.position.y, _level2WorkerPosition.position.y, _level3WorkerPosition.position.y, _level1TowerPiecePosition.position.y, _level2TowerPiecePosition.position.y, _level3TowerPiecePosition.position.y, _domePosition.position.y);
            tile.SetPrefabs(_tower1Piece, _tower2Piece, _tower3Piece, _dome);
        }
    }

    public void OnUpdate(Player activePlayer)
    {
        _activePlayer = activePlayer;

        foreach (Tile tile in _tiles)
        {
            tile.OnUpdate();
        }
    }

    public bool AllowsMove(Worker worker, Tile tile)
    {
        // Can't move onto domed tile
        if (tile.IsDomed())
        {
            return false;
        }

        Tile oldTile = worker.GetTile();

        // Can't move up more than one level
        if (tile.GetLevel() > oldTile.GetLevel() + 1)
        {
            return false;
        }

  
        return true;
    }

    public bool OpponentsAllowMove(Tile tile)
    {
        bool allowed = true;
        foreach(Player opponent in _players)
        {
            if(opponent == _activePlayer) { continue; }
            allowed |= opponent.GetGod().AllowsOpponentMove(tile);
        }

        return allowed;
    }

    public bool OpponentsAllowMove(Worker worker, Tile tile)
    {
        bool allowed = true;
        foreach (Player opponent in _players)
        {
            if (opponent == _activePlayer) { continue; }
            allowed |= opponent.GetGod().AllowsOpponentMove(worker, tile);
        }

        return allowed;
    }

    public bool AllowsBuild(Worker worker, Tile tile)
    {
        return !tile.IsDomed();
    }

    public bool OpponentsAllowBuild(Worker worker, Tile tile)
    {
        bool allowed = true;
        foreach (Player opponent in _players)
        {
            if (opponent == _activePlayer) { continue; }
            allowed |= opponent.GetGod().AllowsOpponentBuild(worker, tile);
        }

        return allowed;
    }

    public Player GetActivePlayer()
    {
        return _activePlayer;
    }

    public Tile GetNearestTileToPosition(Vector3 position)
    {
        Tile nearestTile = null;
        float minDistance = float.MaxValue;
        Vector2 positionFlat = new Vector2(position.x, position.z);
        foreach (Tile tile in _tiles)
        {
            Vector2 tilePositionFlat = new Vector2(tile.transform.position.x, tile.transform.position.z);
            float distance = Vector2.Distance(positionFlat, tilePositionFlat);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestTile = tile;
            }
        }

        return nearestTile;
    }

    public GameObject GetNextWorkerPrefab()
    {
        int numWorkers = _activePlayer.GetWorkers().Count;
        Worker.Colour colour = _activePlayer.GetColour();
        if (numWorkers == 0)
        {
            return GetWorkerPrefab(Worker.Gender.Female, colour);
        }
        else if (numWorkers == 1)
        {
            return GetWorkerPrefab(Worker.Gender.Male, colour);
        }

        // First and Second must be male and female, but any additional workers can be any gender
        return GetWorkerPrefab((Worker.Gender) UnityEngine.Random.Range(0, 1), colour);
    }

    GameObject GetWorkerPrefab(Worker.Gender gender, Worker.Colour colour)
    {
        foreach(WorkerPrefab workerPrefab in _workerPrefabs)
        {
            if (workerPrefab.gender == gender && workerPrefab.colour == colour)
            {
                return workerPrefab.prefab;
            }
        }

        Debug.LogErrorFormat("Found no Worker prefab with these settings:\nGender: {0}, Colour: {1}", gender.ToString(), colour.ToString());
        return null;
    }
}
