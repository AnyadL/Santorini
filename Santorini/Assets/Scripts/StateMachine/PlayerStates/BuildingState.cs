﻿using UnityEngine;

public class BuildingState : State
{
    public override void EnterState(InputSystem input, Board board)
    {
        input.ResetMouse0Click();
    }

    public override void ExitState() { return; }

    public override int UpdateState(InputSystem input, Board board)
    {
        Player activePlayer = board.GetActivePlayer();
        Worker selectedWorker = activePlayer.GetSelectedWorker();

        if(board.PressedEndBuild() || activePlayer.GetGod().DoneBuilding())
        {
            activePlayer.GetGod().EndBuild();
            selectedWorker.DisableHighlight();
            return (int)Player.StateId.WaitingOnConfirmation;
        }

        if (input.Mouse0ClickedOnBoard()) 
        {
            Vector3 clickedPosition = input.GetMouse0ClickedPositionBoard();
            Tile nearestTileToClick = board.GetNearestTileToPosition(clickedPosition);

            Worker workerOnTile = nearestTileToClick.GetWorkerOnTile();
            if(workerOnTile != null && activePlayer.GetWorkers().Contains(workerOnTile))
            {
                // Check if you're allowed to go back to the Selecting State
                if(activePlayer.GetGod().AllowsReturnToSelectingState())
                {
                    // Player has decided to reselect which worker they're using
                    selectedWorker.DisableHighlight();
                    return (int)Player.StateId.Selecting;
                }
            }
                
            // God, Board, and opponents all agree that the build is legal
            if (activePlayer.GetGod().AllowsBuild(nearestTileToClick, selectedWorker) && 
                board.AllowsBuild(selectedWorker, nearestTileToClick) &&
                board.OpponentsAllowBuild(selectedWorker, nearestTileToClick))
            {
                if(board.PressedUniqueBuild())
                {
                    nearestTileToClick.AddSpecificPiece(activePlayer.GetGod().GetUniqueBuildLevel());
                }
                else
                {
                    nearestTileToClick.AddTowerPiece();
                }
                
                activePlayer.GetGod().RegisterBuild(nearestTileToClick);
                if(activePlayer.GetGod().DoneBuilding())
                {
                    selectedWorker.DisableHighlight();
                    return (int)Player.StateId.WaitingOnConfirmation;
                }
            }
        }
        else if (input.Mouse0HoveredOnBoard())
        {
            Vector3 hoverPosition = input.GetMouse0HoverPositionBoard();
            Tile nearestTileToHover = board.GetNearestTileToPosition(hoverPosition);

            Worker workerOnTile = nearestTileToHover.GetWorkerOnTile();
               
            // God, Board, and opponents all agree that the hypothetical build would be legal
            if (workerOnTile == null && activePlayer.GetGod().AllowsBuild(nearestTileToHover, selectedWorker) && 
                board.AllowsBuild(selectedWorker, nearestTileToHover) &&
                board.OpponentsAllowBuild(selectedWorker, nearestTileToHover))
            {
                if(board.PressedUniqueBuild())
                {
                    nearestTileToHover.AddSpecificGhostPiece(activePlayer.GetGod().GetUniqueBuildLevel());
                }
                else
                {
                    nearestTileToHover.AddTowerGhostPiece();
                }
            }
        }

        return -1;
    }

    public override int GetStateId()
    {
        return (int)Player.StateId.Building;
    }
}
