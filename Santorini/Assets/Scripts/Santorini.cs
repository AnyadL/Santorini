using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Santorini : MonoBehaviour
{
    [SerializeField]
    Ground _ground = default;

    [SerializeField]
    List<Player> _players = default;

    Player _activePlayer = null;

    void Start()
    {
        _ground.OnStart();
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
            _ground.OnFixedUpdate(GetMouseClick());
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
            Debug.Break();
        }
    }

    Vector3? GetMouseClick()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            float raycastDistance = 100f;
            if (Physics.Raycast(ray, out hit, raycastDistance))
            {
                return hit.point;
            }
        }

        return null;
    }
}