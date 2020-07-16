using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingState : State
{
    public override void EnterState(InputSystem input, Ground ground)
    {
        Debug.Log("Entering Building State");
        input.ResetMouse0Click();
    }

    public override void ExitState() { return; }

    public override int UpdateState(InputSystem input, Ground ground)
    {
        if (!input.Mouse0ClickedOnBoard()) { return -1; }

        Vector3 clickedPosition = input.GetMouse0ClickedPositionBoard();
        Tile nearestTileToClick = ground.GetNearestTileToPosition(clickedPosition);
        Player activePlayer = ground.GetActivePlayer();
        Worker selectedWorker = activePlayer.GetSelectedWorker();

        Worker workerOnTile = nearestTileToClick.GetWorkerOnTile();
        if (activePlayer.GetGod().AllowsBuild(nearestTileToClick, selectedWorker) && 
            ground.AllowsBuild(selectedWorker, nearestTileToClick) &&
            ground.OpponentsAllowBuild(selectedWorker, nearestTileToClick))
        {
            // God, Ground, and opponents all agree that the build is legal
            nearestTileToClick.AddTowerPiece();
            activePlayer.GetGod().RegisterBuild();
            if(activePlayer.GetGod().DoneBuilding())
            {
                selectedWorker.DisableHighlight();
                return (int)Player.StateId.DoneTurn;
            }
        }

        return -1;
    }

    public override int GetStateId()
    {
        return (int)Player.StateId.Building;
    }
}
