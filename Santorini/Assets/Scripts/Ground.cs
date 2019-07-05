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

    public void OnFixedUpdate(Vector3? mousePosition)
    {
        Tile nearestTile = null;
        float minDistance = float.MaxValue;
        foreach (Tile tile in _tiles)
        {
            if (mousePosition != null)
            {
                float distance = Vector3.Distance((Vector3) mousePosition, tile.transform.position);
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
