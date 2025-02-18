using UnityEngine;

public class PlacingState : State
{
    public override void EnterState(InputSystem input, Board board)
    {
        input.ResetMouse0Click();
    }

    public override void ExitState() { return; }

    public override int UpdateState(InputSystem input, Board board)
    {
        Player activePlayer = board.GetActivePlayer();

        if(activePlayer.GetGod().DonePlacing())
        {
            return (int)Player.StateId.Selecting;
        }

        if (input.Mouse0ClickedOnBoard())
        {
            Vector3 clickedPosition = input.GetMouse0ClickedPositionBoard();
            Tile nearestTileToClick = board.GetNearestTileToPosition(clickedPosition);

            if (activePlayer.GetGod().AllowsMove(null, nearestTileToClick) && board.OpponentsAllowMove(nearestTileToClick))
            {
                GameObject newWorkerGO = board.GetNextWorkerPrefab(out Worker.Gender gender, out Worker.Colour colour);
                Worker newWorker = nearestTileToClick.PlaceWorker(newWorkerGO, gender, colour);
                newWorker.SetPlayer(activePlayer);
                activePlayer.AddWorker(newWorker);
                activePlayer.GetGod().RegisterPlacedWorker();

                if (activePlayer.GetGod().DonePlacingThisTurn())
                {
                    return (int)Player.StateId.WaitingOnConfirmation;
                }
            }
        } 
        else if (input.Mouse0HoveredOnBoard())
        {
            Vector3 hoverPosition = input.GetMouse0HoverPositionBoard();
            Tile nearestTileToHover = board.GetNearestTileToPosition(hoverPosition);

            if (activePlayer.GetGod().AllowsMove(null, nearestTileToHover) && board.OpponentsAllowMove(nearestTileToHover))
            {
                board.GetNextWorkerPrefab(out Worker.Gender gender, out Worker.Colour colour);
                GameObject newWorkerGhost = board.GetWorkerGhostPrefab(gender, colour);
                nearestTileToHover.AddGhostWorker(newWorkerGhost);
            }
        }
        
        return -1;
    }

    public override int GetStateId()
    {
        return (int)Player.StateId.Placing;
    }
}
