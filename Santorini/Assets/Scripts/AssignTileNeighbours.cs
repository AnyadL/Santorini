using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class AssignTileNeighbours : MonoBehaviour
{
    [SerializeField]
    bool _reassignTileNeighbours = false;

    [SerializeField]
    bool _testNeighbourAssignation = false;

    [SerializeField]
    bool _resetTest = false;

    Tile[] _tiles;

    void Update()
    {
        Debug.LogWarning("AssignTileNeighbours is Running");
        if (_reassignTileNeighbours || _testNeighbourAssignation || _resetTest)
        {
            _tiles = FindObjectsOfType<Tile>();

            if (_reassignTileNeighbours)
            {
                AssignNeighbours();
            }

            if (_testNeighbourAssignation)
            {
                TestNeighbours();
            }

            if (_resetTest)
            {
                foreach (Tile tile in _tiles)
                {
                    tile.transform.Find("TileMesh").gameObject.SetActive(false);
                }
            }
        }
    }

    void AssignNeighbours()
    { 
        foreach (Tile tile in _tiles)
        {
            if(tile.gameObject.GetComponentInChildren<Transform>().gameObject.activeInHierarchy)
            Debug.Log(tile.name);
            Vector3 position = tile.transform.position;
            Debug.Log(position);

            //north
            Tile.TileNeighbour northNeighbour = new Tile.TileNeighbour();
            Tile northTile = FindTileAtPosition(new Vector3(position.x - 10f, position.y, position.z));
            northNeighbour.directlyNeighbouring = true;
            if (northTile == null)
            {
                northTile = FindTileAtPosition(new Vector3(position.x + 40f, position.y, position.z));
                northNeighbour.directlyNeighbouring = false;
            }

            northNeighbour.tile = northTile;
            northNeighbour.direction = Tile.TileNeighbour.Direction.North;

            //northeast
            Tile.TileNeighbour northeastNeighbour = new Tile.TileNeighbour();
            Tile northeastTile = FindTileAtPosition(new Vector3(position.x - 10f, position.y, position.z + 10f));
            northeastNeighbour.directlyNeighbouring = true;
            if (northeastTile == null)
            {
                northeastTile = FindTileAtPosition(new Vector3(position.x + 40f, position.y, position.z + 10f));
                northeastNeighbour.directlyNeighbouring = false;
            }

            if (northeastTile == null)
            {
                northeastTile = FindTileAtPosition(new Vector3(position.x - 10f, position.y, position.z - 40f));
            }

            if (northeastTile == null)
            {
                northeastTile = FindTileAtPosition(new Vector3(position.x + 40f, position.y, position.z - 40f));
            }

            northeastNeighbour.tile = northeastTile;
            northeastNeighbour.direction = Tile.TileNeighbour.Direction.NorthEast;

            //east
            Tile.TileNeighbour eastNeighbour = new Tile.TileNeighbour();
            Tile eastTile = FindTileAtPosition(new Vector3(position.x, position.y, position.z + 10f));
            eastNeighbour.directlyNeighbouring = true;
            if (eastTile == null)
            {
                eastTile = FindTileAtPosition(new Vector3(position.x, position.y, position.z - 40f));
                eastNeighbour.directlyNeighbouring = false;
            }

            eastNeighbour.tile = eastTile;
            eastNeighbour.direction = Tile.TileNeighbour.Direction.East;

            //southeast
            Tile.TileNeighbour southeastNeighbour = new Tile.TileNeighbour();
            Tile southeastTile = FindTileAtPosition(new Vector3(position.x + 10f, position.y, position.z + 10f));
            southeastNeighbour.directlyNeighbouring = true;
            if (southeastTile == null)
            {
                southeastTile = FindTileAtPosition(new Vector3(position.x - 40f, position.y, position.z + 10f));
                southeastNeighbour.directlyNeighbouring = false;
            }

            if (southeastTile == null)
            {
                southeastTile = FindTileAtPosition(new Vector3(position.x + 10f, position.y, position.z - 40f));
            }

            if (southeastTile == null)
            {
                southeastTile = FindTileAtPosition(new Vector3(position.x - 40f, position.y, position.z - 40f));
            }

            southeastNeighbour.tile = southeastTile;
            southeastNeighbour.direction = Tile.TileNeighbour.Direction.SouthEast;

            //south
            Tile.TileNeighbour southNeighbour = new Tile.TileNeighbour();
            Tile southTile = FindTileAtPosition(new Vector3(position.x + 10f, position.y, position.z));
            southNeighbour.directlyNeighbouring = true;
            if (southTile == null)
            {
                southTile = FindTileAtPosition(new Vector3(position.x - 40f, position.y, position.z));
                southNeighbour.directlyNeighbouring = false;
            }

            southNeighbour.tile = southTile;
            southNeighbour.direction = Tile.TileNeighbour.Direction.South;

            //southwest
            Tile.TileNeighbour southwestNeighbour = new Tile.TileNeighbour();
            Tile southwestTile = FindTileAtPosition(new Vector3(position.x + 10f, position.y, position.z - 10f));
            southwestNeighbour.directlyNeighbouring = true;
            if (southwestTile == null)
            {
                southwestTile = FindTileAtPosition(new Vector3(position.x - 40f, position.y, position.z - 10f));
                southwestNeighbour.directlyNeighbouring = false;
            }

            if (southwestTile == null)
            {
                southwestTile = FindTileAtPosition(new Vector3(position.x + 10f, position.y, position.z + 40f));
            }

            if (southwestTile == null)
            {
                southwestTile = FindTileAtPosition(new Vector3(position.x - 40f, position.y, position.z + 40f));
            }

            southwestNeighbour.tile = southwestTile;
            southwestNeighbour.direction = Tile.TileNeighbour.Direction.SouthWest;

            //west
            Tile.TileNeighbour westNeighbour = new Tile.TileNeighbour();
            Tile westTile = FindTileAtPosition(new Vector3(position.x, position.y, position.z - 10f));
            westNeighbour.directlyNeighbouring = true;
            if (westTile == null)
            {
                westTile = FindTileAtPosition(new Vector3(position.x, position.y, position.z + 40f));
                westNeighbour.directlyNeighbouring = false;
            }

            westNeighbour.tile = westTile;
            westNeighbour.direction = Tile.TileNeighbour.Direction.West;

            //northwest
            Tile.TileNeighbour northwestNeighbour = new Tile.TileNeighbour();
            Tile northwestTile = FindTileAtPosition(new Vector3(position.x - 10f, position.y, position.z - 10f));
            northwestNeighbour.directlyNeighbouring = true;
            if (northwestTile == null)
            {
                northwestTile = FindTileAtPosition(new Vector3(position.x + 40f, position.y, position.z - 10f));
                northwestNeighbour.directlyNeighbouring = false;
            }

            if (northwestTile == null)
            {
                northwestTile = FindTileAtPosition(new Vector3(position.x - 10f, position.y, position.z + 40f));
            }

            if (northwestTile == null)
            {
                northwestTile = FindTileAtPosition(new Vector3(position.x + 40f, position.y, position.z + 40f));
            }

            northwestNeighbour.tile = northwestTile;
            northwestNeighbour.direction = Tile.TileNeighbour.Direction.NorthWest;

            Tile.TileNeighbour[] neighbours = new Tile.TileNeighbour[8] { northNeighbour, northeastNeighbour, eastNeighbour, southeastNeighbour, southNeighbour, southwestNeighbour, westNeighbour, northwestNeighbour };
            foreach(Tile.TileNeighbour neighbour in neighbours)
            {
                Debug.Log(neighbour.direction + " -- " + neighbour.tile.name);
            }

            tile.neighbours = neighbours;
            Debug.Log("---------------------------------");
        }

        Debug.Log("****************************");
    }

    Tile FindTileAtPosition(Vector3 position)
    {
        foreach (Tile tile in _tiles)
        {
            if (tile.transform.position == position)
            {
                return tile;
            }
        }

        return null;
    }

    void TestNeighbours()
    {
        List<GameObject> activeNeighbours = new List<GameObject>();
        foreach(Tile tile in _tiles)
        {
            GameObject tileChildGO = tile.transform.Find("TileMesh").gameObject;
            if (tileChildGO.activeInHierarchy)
            {
                foreach(Tile.TileNeighbour neighbour in tile.neighbours)
                {
                    activeNeighbours.Add(neighbour.tile.transform.Find("TileMesh").gameObject);
                }
            }
        }

        foreach(GameObject neighbour in activeNeighbours)
        {
            neighbour.SetActive(true);
        }
    }
}