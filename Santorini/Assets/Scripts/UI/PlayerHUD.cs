using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField]
    GameObject _endTurn = default;

    bool _readyToEndTurn = false;

    public void Reset()
    {
        ;
    }

    public void EnableEndTurnButton()
    {
        if (!_endTurn.activeInHierarchy)
        {
            _endTurn.SetActive(true);
            _readyToEndTurn = false;
        }
    }

    public void DisableEndTurnButton()
    {
        if (_endTurn.activeInHierarchy)
        {
            _endTurn.SetActive(false);
            _readyToEndTurn = false;
        }
    }

    public void EndTurnPressed()
    {
        _readyToEndTurn = true;
    }

    public bool PressedEndTurn()
    {
        return _readyToEndTurn;
    }
}
