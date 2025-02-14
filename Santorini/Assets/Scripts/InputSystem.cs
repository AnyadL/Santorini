using UnityEngine;

public class InputSystem : MonoBehaviour
{
    bool _mouse0ClickedThisFrame = false;
    bool _mouse0ClickedBoard = false;
    bool _mouse0HoveredBoard = false;
    Vector3 _mouse0ClickedPositionScreen = default;
    Vector3 _mouse0ClickedPositionBoard = default;

    Vector3 _mouse0HoverPositionBoard = default;
   
    public void OnUpdate()
    {
        ResetMouse0Click();

        _mouse0ClickedThisFrame = Input.GetMouseButtonDown(0);
        if (_mouse0ClickedThisFrame)
        {
            _mouse0ClickedPositionScreen = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(_mouse0ClickedPositionScreen);
            RaycastHit hit;
            float raycastDistance = 150f;
            if (Physics.Raycast(ray, out hit, raycastDistance))
            {
                _mouse0ClickedBoard = true;
                _mouse0ClickedPositionBoard = hit.point;
                _mouse0HoverPositionBoard = hit.point;
            }
        } 
        else
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            float raycastDistance = 150f;
            if (Physics.Raycast(ray, out hit, raycastDistance))
            {
                _mouse0HoveredBoard = true;
                _mouse0HoverPositionBoard = hit.point;
            }
        }
    }

    public bool Mouse0ClickedThisFrame()
    {
        return _mouse0ClickedThisFrame;
    }

    public bool Mouse0ClickedOnBoard()
    {
        return _mouse0ClickedBoard;
    }

    public bool Mouse0HoveredOnBoard()
    {
        return _mouse0HoveredBoard;
    }

    public Vector3 GetMouse0ClickedPositionBoard()
    {
        return _mouse0ClickedPositionBoard;
    }

    public Vector3 GetMouse0ClickedPositionScreen()
    {
        return _mouse0ClickedPositionScreen;
    }

    public Vector3 GetMouse0HoverPositionBoard()
    {
        return _mouse0HoverPositionBoard;
    }

    public bool Mouse1Clicked()
    {
        return Input.GetMouseButton(1);
    }

    public float GetMouseScrollDeltaY()
    {
        return Input.mouseScrollDelta.y;
    }

    public void ResetMouse0Click()
    {
        _mouse0ClickedThisFrame = false;
        _mouse0ClickedBoard = false;
        _mouse0HoveredBoard = false;
        _mouse0ClickedPositionBoard = new Vector3();
        _mouse0ClickedPositionScreen = new Vector3();
        _mouse0HoverPositionBoard = new Vector3();
    }
}
