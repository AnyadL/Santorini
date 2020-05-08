using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Ground : MonoBehaviour
{
    [SerializeField]
    Tile[] _tiles = null;
    
    public void OnStart()
    {
        foreach(Tile tile in _tiles)
        {
            tile.OnStart();
        }
    }

    public void OnUpdate()
    {
        foreach (Tile tile in _tiles)
        {
            tile.OnUpdate();
        }
    }

    public void OnFixedUpdate(bool clicked, Vector3 clickedPosition)
    {
        Tile nearestTile = null;
        float minDistance = float.MaxValue;
        Vector2 clickedPositionFlat = new Vector2(clickedPosition.x, clickedPosition.z);
        foreach (Tile tile in _tiles)
        {
            if (clicked)
            {
                Vector2 tilePositionFlat = new Vector2(tile.transform.position.x, tile.transform.position.z);
                float distance = Vector2.Distance(clickedPositionFlat, tilePositionFlat);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestTile = tile;
                }
            }

            tile.OnFixedUpdate();
        }

        if(nearestTile != null)
        {
            nearestTile.OnMouseClick();
        }
    }
}
