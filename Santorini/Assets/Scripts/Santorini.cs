using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Santorini : MonoBehaviour
{
    [SerializeField]
    Ground _ground = default;

    [SerializeField]
    PlayerCamera _camera = default;

    [SerializeField]
    InputSystem _input = default;

    [Header("Prefabs")]
    [SerializeField]
    GameObject _player1Worker1 = default;
    [SerializeField]
    GameObject _player1Worker2 = default;
    [SerializeField]
    GameObject _player2Worker1 = default;
    [SerializeField]
    GameObject _player2Worker2 = default;
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

    List<God> _gods = default;
    God _activeGod = null;

    void Start()
    {
        _ground.OnStart(_groundWorkerPosition.position.y, _level1WorkerPosition.position.y, _level2WorkerPosition.position.y, _level3WorkerPosition.position.y, _level1TowerPiecePosition.position.y, _level2TowerPiecePosition.position.y, _level3TowerPiecePosition.position.y, _domePosition.position.y);
        _gods = new List<God>() { new BaseGod(), new BaseGod() };
        _activeGod = _gods[0];

        _gods[0].OnStart(_player1Worker1, _player1Worker2);
        _gods[1].OnStart(_player2Worker1, _player2Worker2);
    }
    
    void Update()
    {
        try
        {
            _ground.OnUpdate(_input.Mouse0ClickedOnBoard(), _input.GetMouse0ClickedPosition());
            _camera.OnUpdate(_input.Mouse1Clicked(), _input.GetMouseScrollDeltaY(), _ground.transform);

            _activeGod.PlayTurn(_ground);
            if(_activeGod.GetStatus() == God.GodStatus.DoneTurn)
            {
                _activeGod = GetNextGod();
            }
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
            Debug.Break();
        }
    }
    
    God GetNextGod()
    {
        for(int i = 0; i < _gods.Count; i++)
        {
            if(_gods[i-1] == _activeGod)
            {
                return _gods[i];
            }
        }

        return _activeGod;
    }
}