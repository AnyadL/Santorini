using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Santorini : MonoBehaviour
{
    [SerializeField]
    Ground _ground = default;

    Vector3? _clickLocation;

    void Start()
    {
        _ground.OnStart();
    }
    
    void Update()
    {
        try
        {
            _ground.OnUpdate();
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