using System.Collections.Generic;
using UnityEditor;
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
            if (tile.gameObject.GetComponentInChildren<Transform>().gameObject.activeInHierarchy)
                Debug.Log(tile.name);
            Vector3 position = tile.transform.position;
            Debug.Log(position);

            //north
            Tile.TileNeighbour northNeighbour = new Tile.TileNeighbour();
            Tile northTile = FindTileAtPosition(new Vector3(position.x - 10f, position.y, position.z));
            northNeighbour.SetDirectlyNeighbouring(true);
            if (northTile == null)
            {
                northTile = FindTileAtPosition(new Vector3(position.x + 40f, position.y, position.z));
                northNeighbour.SetDirectlyNeighbouring(false);
            }

            northNeighbour.SetTile(northTile);
            northNeighbour.SetDirection(Tile.TileNeighbour.Direction.North);

            //northeast
            Tile.TileNeighbour northeastNeighbour = new Tile.TileNeighbour();
            Tile northeastTile = FindTileAtPosition(new Vector3(position.x - 10f, position.y, position.z + 10f));
            northeastNeighbour.SetDirectlyNeighbouring(true);
            if (northeastTile == null)
            {
                northeastTile = FindTileAtPosition(new Vector3(position.x + 40f, position.y, position.z + 10f));
                northeastNeighbour.SetDirectlyNeighbouring(false);
            }

            if (northeastTile == null)
            {
                northeastTile = FindTileAtPosition(new Vector3(position.x - 10f, position.y, position.z - 40f));
            }

            if (northeastTile == null)
            {
                northeastTile = FindTileAtPosition(new Vector3(position.x + 40f, position.y, position.z - 40f));
            }

            northeastNeighbour.SetTile(northeastTile);
            northeastNeighbour.SetDirection(Tile.TileNeighbour.Direction.NorthEast);

            //east
            Tile.TileNeighbour eastNeighbour = new Tile.TileNeighbour();
            Tile eastTile = FindTileAtPosition(new Vector3(position.x, position.y, position.z + 10f));
            eastNeighbour.SetDirectlyNeighbouring(true);
            if (eastTile == null)
            {
                eastTile = FindTileAtPosition(new Vector3(position.x, position.y, position.z - 40f));
                eastNeighbour.SetDirectlyNeighbouring(false);
            }

            eastNeighbour.SetTile(eastTile);
            eastNeighbour.SetDirection(Tile.TileNeighbour.Direction.East);

            //southeast
            Tile.TileNeighbour southeastNeighbour = new Tile.TileNeighbour();
            Tile southeastTile = FindTileAtPosition(new Vector3(position.x + 10f, position.y, position.z + 10f));
            southeastNeighbour.SetDirectlyNeighbouring(true);
            if (southeastTile == null)
            {
                southeastTile = FindTileAtPosition(new Vector3(position.x - 40f, position.y, position.z + 10f));
                southeastNeighbour.SetDirectlyNeighbouring(false);
            }

            if (southeastTile == null)
            {
                southeastTile = FindTileAtPosition(new Vector3(position.x + 10f, position.y, position.z - 40f));
            }

            if (southeastTile == null)
            {
                southeastTile = FindTileAtPosition(new Vector3(position.x - 40f, position.y, position.z - 40f));
            }

            southeastNeighbour.SetTile(southeastTile);
            southeastNeighbour.SetDirection(Tile.TileNeighbour.Direction.SouthEast);

            //south
            Tile.TileNeighbour southNeighbour = new Tile.TileNeighbour();
            Tile southTile = FindTileAtPosition(new Vector3(position.x + 10f, position.y, position.z));
            southNeighbour.SetDirectlyNeighbouring(true);
            if (southTile == null)
            {
                southTile = FindTileAtPosition(new Vector3(position.x - 40f, position.y, position.z));
                southNeighbour.SetDirectlyNeighbouring(false);
            }

            southNeighbour.SetTile(southTile);
            southNeighbour.SetDirection(Tile.TileNeighbour.Direction.South);

            //southwest
            Tile.TileNeighbour southwestNeighbour = new Tile.TileNeighbour();
            Tile southwestTile = FindTileAtPosition(new Vector3(position.x + 10f, position.y, position.z - 10f));
            southwestNeighbour.SetDirectlyNeighbouring(true);
            if (southwestTile == null)
            {
                southwestTile = FindTileAtPosition(new Vector3(position.x - 40f, position.y, position.z - 10f));
                southwestNeighbour.SetDirectlyNeighbouring(false);
            }

            if (southwestTile == null)
            {
                southwestTile = FindTileAtPosition(new Vector3(position.x + 10f, position.y, position.z + 40f));
            }

            if (southwestTile == null)
            {
                southwestTile = FindTileAtPosition(new Vector3(position.x - 40f, position.y, position.z + 40f));
            }

            southwestNeighbour.SetTile(southwestTile);
            southwestNeighbour.SetDirection(Tile.TileNeighbour.Direction.SouthWest);

            //west
            Tile.TileNeighbour westNeighbour = new Tile.TileNeighbour();
            Tile westTile = FindTileAtPosition(new Vector3(position.x, position.y, position.z - 10f));
            westNeighbour.SetDirectlyNeighbouring(true);
            if (westTile == null)
            {
                westTile = FindTileAtPosition(new Vector3(position.x, position.y, position.z + 40f));
                westNeighbour.SetDirectlyNeighbouring(false);
            }

            westNeighbour.SetTile(westTile);
            westNeighbour.SetDirection(Tile.TileNeighbour.Direction.West);

            //northwest
            Tile.TileNeighbour northwestNeighbour = new Tile.TileNeighbour();
            Tile northwestTile = FindTileAtPosition(new Vector3(position.x - 10f, position.y, position.z - 10f));
            northwestNeighbour.SetDirectlyNeighbouring(true);
            if (northwestTile == null)
            {
                northwestTile = FindTileAtPosition(new Vector3(position.x + 40f, position.y, position.z - 10f));
                northwestNeighbour.SetDirectlyNeighbouring(false);
            }

            if (northwestTile == null)
            {
                northwestTile = FindTileAtPosition(new Vector3(position.x - 10f, position.y, position.z + 40f));
            }

            if (northwestTile == null)
            {
                northwestTile = FindTileAtPosition(new Vector3(position.x + 40f, position.y, position.z + 40f));
            }

            northwestNeighbour.SetTile(northwestTile);
            northwestNeighbour.SetDirection(Tile.TileNeighbour.Direction.NorthWest);

            Tile.TileNeighbour[] neighbours = new Tile.TileNeighbour[8] { northNeighbour, northeastNeighbour, eastNeighbour, southeastNeighbour, southNeighbour, southwestNeighbour, westNeighbour, northwestNeighbour };
            foreach (Tile.TileNeighbour neighbour in neighbours)
            {
                Debug.Log(neighbour.GetDirection() + " -- " + neighbour.GetTile().name);
            }

            tile.SetNeighbours(neighbours);
            EditorUtility.SetDirty(tile);
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
        foreach (Tile tile in _tiles)
        {
            GameObject tileChildGO = tile.transform.Find("TileMesh").gameObject;
            if (tileChildGO.activeInHierarchy)
            {
                foreach (Tile.TileNeighbour neighbour in tile.GetNeighbours())
                {
                    activeNeighbours.Add(neighbour.GetTile().transform.Find("TileMesh").gameObject);
                }
            }
        }

        foreach (GameObject neighbour in activeNeighbours)
        {
            neighbour.SetActive(true);
        }
    }
}