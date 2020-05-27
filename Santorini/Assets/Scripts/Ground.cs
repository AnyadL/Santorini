using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class Ground : MonoBehaviour
{
    [SerializeField]
    Tile[] _tiles = null;

    [Header("Tower Prefabs")]
    [SerializeField]
    GameObject _tower1Piece = default;
    [SerializeField]
    GameObject _tower2Piece = default;
    [SerializeField]
    GameObject _tower3Piece = default;
    [SerializeField]
    GameObject _dome = default;

    [Header("Positions")]
    [SerializeField]
    Transform _groundWorkerPosition = default;
    [SerializeField]
    Transform _level1WorkerPosition = default;
    [SerializeField]
    Transform _level2WorkerPosition = default;
    [SerializeField]
    Transform _level3WorkerPosition = default;
    [SerializeField]
    Transform _level1TowerPiecePosition = default;
    [SerializeField]
    Transform _level2TowerPiecePosition = default;
    [SerializeField]
    Transform _level3TowerPiecePosition = default;
    [SerializeField]
    Transform _domePosition = default;

    GameObject _blueFemale = null;
    GameObject _whiteFemale = null;

    List<Worker> _workers = null;

    Tile _nearestTileToLastClick = default;
    Networker _networker = default;

    bool _requestPlaceWorkerSucceeded = false;

    public void OnStart(GameObject blueFemale, GameObject whiteFemale)
    {
        _blueFemale = blueFemale;
        _whiteFemale = whiteFemale;

        foreach (Tile tile in _tiles)
        {
            tile.OnStart();
            tile.SetYPositions(_groundWorkerPosition.position.y, _level1WorkerPosition.position.y, _level2WorkerPosition.position.y, _level3WorkerPosition.position.y, _level1TowerPiecePosition.position.y, _level2TowerPiecePosition.position.y, _level3TowerPiecePosition.position.y, _domePosition.position.y);
            tile.SetPrefabs(_tower1Piece, _tower2Piece, _tower3Piece, _dome);
        }
    }

    public void OnUpdate(bool clicked, Vector3 clickedPosition)
    {
        foreach (Tile tile in _tiles)
        {
            tile.OnUpdate();
            NetworkedTile networkedTile = tile.GetNetworkedTile();
            if(networkedTile.addWorkerRequest != null)
            {
                AddWorkerToTile(networkedTile.addWorkerRequest, tile);
                networkedTile.addWorkerRequest = null;
            }
            if (networkedTile.removeWorkerRequest != null)
            {
                RemoveWorkerFromFile(networkedTile.addWorkerRequest, tile);
                networkedTile.removeWorkerRequest = null;
            }

        }

        if (clicked)
        {
            _nearestTileToLastClick = GetNearestTileToPosition(clickedPosition);
        }
    }

    void RemoveWorkerFromFile(NetworkedTile.WorkerRequest WorkerRequest, Tile tile)
    {

    }


    void AddWorkerToTile(NetworkedTile.WorkerRequest workerRequest, Tile tile)
    {
        GameObject workerPrefab;
        if(workerRequest.colour == Worker.Colour.Blue)
        {
            workerPrefab = _blueFemale;
        }
        else
        {
            workerPrefab = _whiteFemale;
        }

        GameObject newWorker = Instantiate(workerPrefab, new Vector3(tile.transform.position.x, tile.GetWorkerY(), tile.transform.position.z), Quaternion.identity);
        //_workerOnTile = newWorker.GetComponent<Worker>();
        //_workerOnTile.SetTile(this);

        _networker.SpawnObject(newWorker);
        //_networkedTile.AddWorker();
        //return true;
    }

    public void SetNetworker(Networker networker)
    {
        _networker = networker;

        foreach (Tile tile in _tiles)
        {
            tile.SetNetworker(networker);
        }
    }

    public Tile[] GetTiles()
    {
        return _tiles;
    }

    public Tile GetNearestTiltToLastClick()
    {
        return _nearestTileToLastClick;
    }

    public IEnumerator TryPlaceWorkerAtLastClick(Worker.Colour workerColour, Worker.Gender workerGender)
    {
        if (_nearestTileToLastClick.TryRequestPlaceWorker(workerColour, workerGender))
        {
            if (!_nearestTileToLastClick.RequestPlaceWorkerCompleted())
            {
                yield return null;
            }

            _requestPlaceWorkerSucceeded = _nearestTileToLastClick.RequestPlaceWorkerSucceeded();
            yield return _nearestTileToLastClick.RequestPlaceWorkerSucceeded();
        }
    }

    public bool GetRequestPlaceWorkerSucceeded()
    {
        return _requestPlaceWorkerSucceeded;
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
