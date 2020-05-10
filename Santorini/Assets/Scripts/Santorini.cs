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

    List<God> _gods = default;
    God _activeGod = null;

    Vector3 _mouseClickedPosition = default;

    void Start()
    {
        _ground.OnStart();
        _gods = new List<God>() { new BaseGod(), new BaseGod() };
        _activeGod = _gods[0];
    }
    
    void Update()
    {
        try
        {
            _ground.OnUpdate();

            // If Workers aren't placed -- _activeGod.PlaceWorkers() and switch _activeGod

            _activeGod.PlayTurn();
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