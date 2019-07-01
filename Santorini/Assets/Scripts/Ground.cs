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

    public void OnFixedUpdate()
    {
        foreach (Tile tile in _tiles)
        {
            tile.OnFixedUpdate();
        }
    }
}
