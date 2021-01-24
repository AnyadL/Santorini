using TMPro;
using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField]
    GameObject _endTurn = default;
    [SerializeField]
    GameObject _endingTurnCountdown = default;
    [SerializeField]
    TextMeshProUGUI _endingTurnCountdownNumber = default;

    bool _readyToEndTurn = false;

    public void Reset()
    {
        ;
    }

    public void EnableCountdownText(bool enable)
    {
        _endingTurnCountdown.SetActive(enable);
        _endTurn.SetActive(!enable);
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

    public void SetCountdownText(int time)
    {
        _endingTurnCountdownNumber.text = time.ToString();
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
