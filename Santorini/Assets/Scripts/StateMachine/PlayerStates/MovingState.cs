using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingState : State
{
    public override void EnterState(InputSystem input, Ground ground)
    {
        Debug.Log("Entering Moving State");
        input.ResetMouse0Click();
    }

    public override void ExitState()
    {
        return;
    }

    public override int UpdateState(InputSystem input, Ground ground)
    {
        if (!input.Mouse0ClickedOnBoard()) { return -1; }

        Vector3 clickedPosition = input.GetMouse0ClickedPositionBoard();
        Tile nearestTileToClick = ground.GetNearestTileToPosition(clickedPosition);
        Player activePlayer = ground.GetActivePlayer();
        Worker selectedWorker = activePlayer.GetSelectedWorker();

        Worker workerOnTile = nearestTileToClick.GetWorkerOnTile();
        if(workerOnTile != null && activePlayer.GetWorkers().Contains(workerOnTile))
        {
            // Player has decided to reselect which worker they're using
            return (int)Player.StateId.Selecting;
        }

        if(activePlayer.GetGod().AllowsMove(nearestTileToClick, selectedWorker) && 
            ground.AllowsMove(selectedWorker, nearestTileToClick) &&
            ground.OpponentsAllowMove(selectedWorker, nearestTileToClick))
        {
            // God and Ground agree that the move is legal
            selectedWorker.GetTile().RemoveWorker();
            nearestTileToClick.AddWorker(selectedWorker);
            activePlayer.GetGod().RegisterMove();
            selectedWorker.SetTile(nearestTileToClick);

            if(activePlayer.GetGod().DoneMoving())
            {
                return (int)Player.StateId.Building;
            }
        }

        return -1;
    }

    public override int GetStateId()
    {
        return (int)Player.StateId.Moving;
    }
}
