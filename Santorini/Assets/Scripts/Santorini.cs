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

    List<Player> _players = default;

    Player _activePlayer = null;

    Vector3 _mouseClickedPosition = default;

    void Start()
    {
        _ground.OnStart();
        _players = new List<Player>() { new Player(), new Player() };

        foreach (Player player in _players)
        {
            player.OnStart();
        }

        _activePlayer = _players[0];
    }
    
    void Update()
    {
        try
        {
            _ground.OnUpdate();
            foreach (Player player in _players)
            {
                player.OnUpdate();
            }
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
            Debug.Break();
        }
    }

    void FixedUpdate()
    {
        try
        {
            _ground.OnFixedUpdate(MouseClickedOnBoard(), _mouseClickedPosition);
            
            // Rotate Camera if Right Click
            if (Input.GetMouseButton(1))
            {
                _camera.RotateAroundTransform(_ground.transform, Input.GetAxis("Mouse X"));
            }

            // Zoom Camera if Mouse Wheel
            if (Input.mouseScrollDelta.y > Mathf.Epsilon)
            {
                _camera.ZoomIn(_ground.transform.position);
            }
            else if (Input.mouseScrollDelta.y < -Mathf.Epsilon)
            {
                _camera.ZoomOut(_ground.transform.position);
            }
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
            Debug.Break();
        }
    }

    bool MouseClickedOnBoard()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            float raycastDistance = 150f;
            if (Physics.Raycast(ray, out hit, raycastDistance))
            {
                _mouseClickedPosition = hit.point;
                return true;
            }
        }

        return false;
    }
}