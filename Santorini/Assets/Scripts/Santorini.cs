using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Santorini : MonoBehaviour
{
    [SerializeField]
    Ground _ground = default;

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
            TestMouseClick();
            _ground.OnUpdate();
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
            Debug.Break();
        }
    }

    void TestMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            float raycastDistance = 100f;
            if (Physics.Raycast(ray, out hit, raycastDistance))
            {
                Debug.DrawLine(ray.origin, hit.point);
                Debug.Log(hit.point);
            }
        }
    }
}