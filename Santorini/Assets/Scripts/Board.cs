﻿using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Board : MonoBehaviour
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

    [Header("Ghost Tower Prefabs")]
    [SerializeField]
    GameObject _tower1PieceGhost = default;
    [SerializeField]
    GameObject _tower2PieceGhost = default;
    [SerializeField]
    GameObject _tower3PieceGhost = default;
    [SerializeField]
    GameObject _domeGhost = default;

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

    [SerializeField]
    List<WorkerPrefab> _workerGhostPrefabs = default;

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

    [Header("UI")]
    [SerializeField]
    PlayerHUD _playerHUD = default;
    [SerializeField]
    TextMeshProUGUI _playerColourText = default;
    [SerializeField]
    TextMeshProUGUI _stateText = default;
    [SerializeField]
    TextMeshProUGUI _godText = default;

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
            tile.SetTowerPiecePrefabs(_tower1Piece, _tower2Piece, _tower3Piece, _dome);
            tile.SetTowerPieceGhostPrefabs(_tower1PieceGhost, _tower2PieceGhost, _tower3PieceGhost, _domeGhost);
        }
    }

    public void OnUpdate(Player activePlayer)
    {
        _activePlayer = activePlayer;

        foreach (Tile tile in _tiles)
        {
            tile.OnUpdate();
        }

        UpdateUI();
    }

    public void UpdateUI()
    {
        _playerColourText.text = _activePlayer.GetColour().ToString();
        _stateText.text = _activePlayer.GetCurrentState().ToString();
        _godText.text = _activePlayer.GetGod().ToString();

        if(_activePlayer.IsWaitingOnConfirmation())
        {
            _playerHUD.DisableEndMoveButton();
            _playerHUD.DisableEndBuildButton();
            _playerHUD.DisableBuildUniqueButton();

            _playerHUD.EnableEndTurnButton();
        }
        else
        {
            _playerHUD.DisableEndTurnButton();
            
            if(_activePlayer.GetGod().AllowedToUndoTurn())
            {
                _playerHUD.EnableUndoTurnButton();
            }
            else
            {
                _playerHUD.DisableUndoTurnButton();
            }

            if(_activePlayer.IsMoving())
            {
                if(_activePlayer.GetGod().AllowedToEndMove())
                {
                    _playerHUD.EnableEndMoveButton();
                }
            }
            else if(_activePlayer.IsBuilding())
            {
                _playerHUD.DisableEndMoveButton();
                
                if(_activePlayer.GetGod().AllowedToEndBuild())
                {
                    _playerHUD.EnableEndBuildButton();
                }

            }
            
            God activePlayerGod = _activePlayer.GetGod();
            if(_activePlayer.IsBuilding() && activePlayerGod.HasUniqueBuild() && activePlayerGod.AllowedToUniqueBuild())
            {
                _playerHUD.SetBuildUniqueText(activePlayerGod.GetUniqueBuildText());
                _playerHUD.EnableBuildUniqueButton();
            }
            else
            {
                _playerHUD.DisableBuildUniqueButton();
            }
        }
    }

    public bool PressedEndTurn()
    {
        return _playerHUD.PressedEndTurn();
    }   
    
    public bool PressedUndoTurn()
    {
        return _playerHUD.PressedUndoTurn();
    }

    public bool PressedEndMove()
    {
        return _playerHUD.PressedEndMove();
    }

    public bool PressedEndBuild()
    {
        return _playerHUD.PressedEndBuild();
    }
    
    public bool PressedUniqueBuild()
    {
        return _playerHUD.PressedUniqueBuild();
    }

    public void ResetPlayerHUD()
    {
        _playerHUD.Reset();
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

    public List<Tile> GetAvailableMoves(Worker worker)
    {
        List<Tile> possibleMoves = new List<Tile>();
        foreach (Tile.TileNeighbour neighbour in worker.GetTile().GetTileNeighbours())
        {
            Tile tile = neighbour.GetTile();
            if (AllowsMove(worker, tile) && OpponentsAllowMove(worker, tile))
            {
                possibleMoves.Add(tile);
            }
        }

        return possibleMoves;
    }

    public bool OpponentsAllowMove(Tile tile)
    {
        foreach(Player opponent in _players)
        {
            if(opponent == _activePlayer) { continue; }
            if(!opponent.GetGod().AllowsOpponentMove(tile))
            {
                return false;
            }
        }

        return true;
    }

    public bool OpponentsAllowMove(Worker worker, Tile tile)
    {
        foreach (Player opponent in _players)
        {
            if (opponent == _activePlayer) { continue; }
            if(!opponent.GetGod().AllowsOpponentMove(worker, tile))
            {
                return false;
            }
        }

        return true;
    }

    public bool AllowsBuild(Worker worker, Tile tile)
    {
        return !tile.IsDomed();
    }
    
    public bool OpponentsAllowBuild(Worker worker, Tile tile)
    {
        foreach (Player opponent in _players)
        {
            if (opponent == _activePlayer) { continue; }
            if(!opponent.GetGod().AllowsOpponentBuild(worker, tile))
            {
                return false;
            }
        }

        return true;
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

    public Tile[] GetTiles()
    {
        return _tiles;
    }

    public GameObject GetNextWorkerPrefab(out Worker.Gender gender, out Worker.Colour colour)
    {
        int numWorkers = _activePlayer.GetWorkers().Count;
        colour = _activePlayer.GetColour();
        if (numWorkers == 0)
        {
            gender = Worker.Gender.Female;
        }
        else if (numWorkers == 1)
        {
            gender = Worker.Gender.Male;
        }
        else
        {
            // First and Second must be male and female, but any additional workers can be any gender
            gender = (Worker.Gender)UnityEngine.Random.Range(0, 1);
        }

        return GetWorkerPrefab(gender, colour);
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

    public GameObject GetWorkerGhostPrefab(Worker.Gender gender, Worker.Colour colour)
    {
        foreach(WorkerPrefab workerGhost in _workerGhostPrefabs)
        {
            if (workerGhost.gender == gender && workerGhost.colour == colour)
            {
                return workerGhost.prefab;
            }
        }

        Debug.LogErrorFormat("Found no Worker prefab with these settings:\nGender: {0}, Colour: {1}", gender.ToString(), colour.ToString());
        return null;
    }
}
