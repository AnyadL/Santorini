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
        public Tile tile;

        [SerializeField]
        public Direction direction;

        [SerializeField]
        public bool directlyNeighbouring = false;
    }

    [SerializeField]
    public TileNeighbour[] neighbours;

    public void OnStart()
    {

    }

    public void OnUpdate()
    {

    }

    public void OnFixedUpdate()
    {

    }
}
