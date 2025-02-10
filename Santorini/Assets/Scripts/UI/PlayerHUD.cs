using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField]
    GameObject _endTurn = default;
    [SerializeField]
    GameObject _undoTurn = default;
    [SerializeField]
    GameObject _endMove = default;
    [SerializeField]
    GameObject _endBuild = default;
    [SerializeField]
    GameObject _buildUnique = default;

    bool _readyToEndTurn = false;
    bool _readyToUndoTurn = false;
    bool _readyToEndMove = false;
    bool _readyToUndoBuild = false;
    bool _readyToBuildUnique = false;

    Color32 _blue = new Color32(30,144,255,255);
    Color32 _white = new Color32(255,255,255,255);

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

    public void EnableUndoTurnButton()
    {
        if (!_undoTurn.activeInHierarchy)
        {
            _undoTurn.SetActive(true);
            _readyToUndoTurn = false;
        }
    }

    public void DisableUndoTurnButton()
    {
        if (_undoTurn.activeInHierarchy)
        {
            _undoTurn.SetActive(false);
            _readyToUndoTurn = false;
        }
    }

    public void UndoTurnPressed()
    {
        _readyToUndoTurn = true;
    }

    public bool PressedUndoTurn()
    {
        return _readyToUndoTurn;
    }

    
    public void EnableEndMoveButton()
    {
        if (!_endMove.activeInHierarchy)
        {
            _endMove.SetActive(true);
            _readyToEndMove = false;
        }
    }

    public void DisableEndMoveButton()
    {
        if (_endMove.activeInHierarchy)
        {
            _endMove.SetActive(false);
            _readyToEndMove = false;
        }
    }

    public void EndMovePressed()
    {
        _readyToEndMove = true;
    }

    public bool PressedEndMove()
    {
        return _readyToEndMove;
    }

    
    public void EnableEndBuildButton()
    {
        if (!_endBuild.activeInHierarchy)
        {
            _endBuild.SetActive(true);
            _readyToUndoBuild = false;
        }
    }

    public void DisableEndBuildButton()
    {
        if (_endBuild.activeInHierarchy)
        {
            _endBuild.SetActive(false);
            _readyToUndoBuild = false;
        }
    }

    public void EndBuildPressed()
    {
        _readyToUndoBuild = true;
    }

    public bool PressedEndBuild()
    {
        return _readyToUndoBuild;
    }
    
    public void EnableBuildUniqueButton()
    {
        if (!_buildUnique.activeInHierarchy)
        {
            _buildUnique.SetActive(true);
            _readyToBuildUnique = false;
            _buildUnique.GetComponent<UnityEngine.UI.Image>().color = _white;
        }
    }

    public void DisableBuildUniqueButton()
    {
        if (_buildUnique.activeInHierarchy)
        {
            _buildUnique.SetActive(false);
            _readyToBuildUnique = false;
        }
    }

    public void BuildUniquePressed()
    {
        _readyToBuildUnique = !_readyToBuildUnique;
        _buildUnique.GetComponent<UnityEngine.UI.Image>().color = _readyToBuildUnique ? _blue : _white;
    }

    public bool PressedUniqueBuild()
    {
        return _readyToBuildUnique;
    }

    public void SetBuildUniqueText(string text)
    {
        _buildUnique.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = text;
    }
}
