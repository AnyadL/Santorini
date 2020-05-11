using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class Ground : MonoBehaviour
{
    [SerializeField]
    Tile[] _tiles = null;

    Tile _nearestTileToLastClick = default;

    public void OnStart(float groundWorkerY, float level1WorkerY, float level2WorkerY, float level3WorkerY, float level1TowerPieceY, float level2TowerPieceY, float level3TowerPieceY, float domeY)
    {
        foreach (Tile tile in _tiles)
        {
            tile.OnStart(groundWorkerY, level1WorkerY, level2WorkerY, level3WorkerY, level1TowerPieceY, level2TowerPieceY, level3TowerPieceY, domeY);
        }
    }

    public void OnUpdate(bool clicked, Vector3 clickedPosition)
    {
        foreach (Tile tile in _tiles)
        {
            tile.OnUpdate();
        }

        if (clicked)
        {
            _nearestTileToLastClick = GetNearestTileToPosition(clickedPosition);

            //if (_nearestTileToLastClick != null)
            //{
            //    _nearestTileToLastClick.HighlightNeighbours();
            //}
        }
    }

    public bool TryPlaceWorkerOnTile(GameObject workerPrefab)
    {
        return _nearestTileToLastClick.TryPlaceWorker(workerPrefab);
    }

    Tile GetNearestTileToPosition(Vector3 position)
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
}
