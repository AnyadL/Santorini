using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

        public void SetTile(Tile tile)
        {
            _tile = tile;
        }

        public Direction GetDirection()
        {
            return _direction;
        }

        public void SetDirection(Direction direction)
        {
            _direction = direction;
        }

        public bool IsDirectlyNeighbouring()
        {
            return _directlyNeighbouring;
        }

        public void SetDirectlyNeighbouring(bool directlyNeighbouring)
        {
            _directlyNeighbouring = directlyNeighbouring;
        }
    }

    [SerializeField]
    TileNeighbour[] _neighbours;
    
    Worker _workerOnTile = null;
    List<ITowerPiece> _towerPiecesOnTile;

    bool _blocked = false;

    public void OnStart()
    {
        _towerPiecesOnTile = new List<ITowerPiece>();
    }

    public void OnUpdate() { }

    public void OnFixedUpdate()
    {
        foreach (TileNeighbour tileNeighbour in _neighbours)
        {
            tileNeighbour.GetTile().transform.Find("TileMesh").gameObject.SetActive(false);
        }

        transform.Find("TileMesh").gameObject.SetActive(false);
    }

    public void OnMouseClick()
    {
        foreach (TileNeighbour tileNeighbour in _neighbours)
        {
            tileNeighbour.GetTile().transform.Find("TileMesh").gameObject.SetActive(true);
        }

        transform.Find("TileMesh").gameObject.SetActive(true);
    }

    public TileNeighbour[] GetNeighbours()
    {
        return _neighbours;
    }

    public void SetNeighbours(TileNeighbour[] neighbours)
    {
        _neighbours = neighbours;
    }

    public void OnMoveWorker(Worker worker, int previousLevel)
    {
        if(_workerOnTile != null)
        {
            Debug.LogError("Tried to move worker onto already occupied tile");
        }

        if(_towerPiecesOnTile.Select(t => t.GetLevel()).Max() > previousLevel + 1)
        {
            Debug.LogError("Worker tried to move up more than one level");
        }

        _workerOnTile = worker;
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
            _blocked = true;
        }
    }
}
