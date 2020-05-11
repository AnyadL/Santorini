using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    Vector3 _mouse0ClickedPosition = default;

    public bool Mouse0ClickedOnBoard()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            float raycastDistance = 150f;
            if (Physics.Raycast(ray, out hit, raycastDistance))
            {
                _mouse0ClickedPosition = hit.point;
                return true;
            }
        }

        _mouse0ClickedPosition = new Vector3();
        return false;
    }

    public Vector3 GetMouse0ClickedPosition()
    {
        return _mouse0ClickedPosition;
    }

    public bool Mouse1Clicked()
    {
        return Input.GetMouseButton(1);
    }

    public float GetMouseScrollDeltaY()
    {
        return Input.mouseScrollDelta.y;
    }
}
