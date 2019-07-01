using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void OnStart()
    {

    }

    public void OnUpdate()
    {

    }

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
}
