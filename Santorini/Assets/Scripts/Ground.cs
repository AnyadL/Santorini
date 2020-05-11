using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Ground : MonoBehaviour
{
    [SerializeField]
    Tile[] _tiles = null;

    float _groundWorkerY = 0.0f;
    float _level1WorkerY = 0.0f;
    float _level2WorkerY = 0.0f;
    float _level3WorkerY = 0.0f;
    float _level1TowerPieceY = 0.0f;
    float _level2TowerPieceY = 0.0f;
    float _level3TowerPieceY = 0.0f;
    float _domeY = 0.0f;

    public void OnStart(float groundWorkerY, float level1WorkerY, float level2WorkerY, float level3WorkerY, float level1TowerPieceY, float level2TowerPieceY, float level3TowerPieceY, float domeY)
    {
        _groundWorkerY = groundWorkerY;
        _level1WorkerY = level1WorkerY;
        _level2WorkerY = level2WorkerY;
        _level3WorkerY = level3WorkerY;
        _level1TowerPieceY = level1TowerPieceY;
        _level2TowerPieceY = level2TowerPieceY;
        _level3TowerPieceY = level3TowerPieceY;
        _domeY = domeY;

        foreach (Tile tile in _tiles)
        {
            tile.OnStart();
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
            Tile nearestTile = null;
            float minDistance = float.MaxValue;
            Vector2 clickedPositionFlat = new Vector2(clickedPosition.x, clickedPosition.z);
            foreach (Tile tile in _tiles)
            {
                Vector2 tilePositionFlat = new Vector2(tile.transform.position.x, tile.transform.position.z);
                float distance = Vector2.Distance(clickedPositionFlat, tilePositionFlat);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestTile = tile;
                }
            }

            if (nearestTile != null)
            {
                nearestTile.OnMouse0Click();
            }
        }
    }
}
