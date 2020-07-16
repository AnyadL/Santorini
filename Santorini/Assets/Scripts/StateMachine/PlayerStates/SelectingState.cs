using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectingState : State
{
    public override void EnterState(InputSystem input, Ground ground)
    {
        Debug.Log("Entering Selecting State");
    }

    public override void ExitState()
    {
        return;
    }

    public override int UpdateState(InputSystem input, Ground ground)
    {
        if(!input.Mouse0ClickedOnBoard()) { return -1; }

        Vector3 clickedPosition = input.GetMouse0ClickedPositionBoard();
        Tile nearestTileToClick = ground.GetNearestTileToPosition(clickedPosition);
        Player activePlayer = ground.GetActivePlayer();

        Worker selectedWorker = ground.GetNearestTileToPosition(clickedPosition).GetWorkerOnTile();
        if(activePlayer.TrySelectWorker(selectedWorker))
        {
            return (int)Player.StateId.Moving;
        }

        return -1;
    }

    public override int GetStateId()
    {
        return (int)Player.StateId.Selecting;
    }
}
