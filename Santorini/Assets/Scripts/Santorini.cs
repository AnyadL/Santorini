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

    List<Player> _players = default;
    Player _activePlayer = null;

    void Start()
    {
        _ground.OnStart();
        _players = new List<Player>() { new Player(), new Player() };
        _activePlayer = _players[0];

        _players[0].Initialize(_input, _ground, Worker.Colour.Blue);
        _players[1].Initialize(_input, _ground, Worker.Colour.White);
    }
    
    void Update()
    {
        try
        {
            _ground.OnUpdate(_input.Mouse0ClickedOnBoard(), _input.GetMouse0ClickedPosition());
            _camera.OnUpdate(_input.Mouse1Clicked(), _input.GetMouseScrollDeltaY(), _ground.transform);

            foreach (Player player in _players)
            {
                player.PlayTurn(player == _activePlayer ? true : false);
            }


            if(_activePlayer.GetStatus() == Player.Status.Won)
            {
                //Ensure other gods don't prevent the win
                Debug.Log("You Win!!!");
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            }
            if(_activePlayer.GetStatus() == Player.Status.DoneTurn)
            {
                _activePlayer = GetNextPlayer();
            }
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
            Debug.Break();
        }
    }
    
    Player GetNextPlayer()
    {
        if(_players[0] == _activePlayer)
        {
            return _players[1];
        }

        return _players[0];
    }
}