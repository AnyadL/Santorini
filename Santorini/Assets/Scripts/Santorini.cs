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

    [Header("Player Prefabs")]
    [SerializeField]
    GameObject _player1Worker1 = default;
    [SerializeField]
    GameObject _player1Worker2 = default;
    [SerializeField]
    GameObject _player2Worker1 = default;
    [SerializeField]
    GameObject _player2Worker2 = default;

    List<God> _gods = default;
    God _activeGod = null;

    void Start()
    {
        _ground.OnStart();
        _gods = new List<God>() { new BaseGod(), new BaseGod() };
        _activeGod = _gods[0];

        _gods[0].OnStart(_player1Worker1, _player1Worker2, _input, _ground);
        _gods[1].OnStart(_player2Worker1, _player2Worker2, _input, _ground);
    }
    
    void Update()
    {
        try
        {
            _ground.OnUpdate(_input.Mouse0ClickedOnBoard(), _input.GetMouse0ClickedPosition());
            _camera.OnUpdate(_input.Mouse1Clicked(), _input.GetMouseScrollDeltaY(), _ground.transform);

            _activeGod.PlayTurn();
            if(_activeGod.GetStatus() == God.GodStatus.Won)
            {
                //Ensure other gods don't prevent the win
                Debug.Log("You Win!!!");
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            }
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
        if(_gods[0] == _activeGod)
        {
            return _gods[1];
        }

        return _gods[0];
    }
}