using UnityEngine;

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

        if(board.PressedEndBuild() || activePlayer.GetGod().EndBuildEarly())
        {
            activePlayer.GetGod().EndBuild();
            selectedWorker.DisableHighlight();
            return (int)Player.StateId.WaitingOnConfirmation;
        }

        if (!input.Mouse0ClickedOnBoard()) { return -1; }

        Vector3 clickedPosition = input.GetMouse0ClickedPositionBoard();
        Tile nearestTileToClick = board.GetNearestTileToPosition(clickedPosition);

        Worker workerOnTile = nearestTileToClick.GetWorkerOnTile();
        if (activePlayer.GetGod().AllowsBuild(nearestTileToClick, selectedWorker) && 
            board.AllowsBuild(selectedWorker, nearestTileToClick) &&
            board.OpponentsAllowBuild(selectedWorker, nearestTileToClick))
        {
            // God, Board, and opponents all agree that the build is legal
            nearestTileToClick.AddTowerPiece();
            activePlayer.GetGod().RegisterBuild(nearestTileToClick);
            if(activePlayer.GetGod().DoneBuilding())
            {
                selectedWorker.DisableHighlight();
                return (int)Player.StateId.WaitingOnConfirmation;
            }
        }

        return -1;
    }

    public override int GetStateId()
    {
        return (int)Player.StateId.Building;
    }
}
