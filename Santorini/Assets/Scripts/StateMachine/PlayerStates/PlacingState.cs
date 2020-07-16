using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacingState : State
{
    public override void EnterState(InputSystem input, Ground ground)
    {
        Debug.Log("Entering Placing State");
        input.ResetMouse0Click();
    }

    public override void ExitState() { return; }

    public override int UpdateState(InputSystem input, Ground ground)
    {
        Player activePlayer = ground.GetActivePlayer();

        if(activePlayer.GetGod().DonePlacing())
        {
            return (int)Player.StateId.Selecting;
        }

        if (!input.Mouse0ClickedOnBoard()) { return -1; }

        Vector3 clickedPosition = input.GetMouse0ClickedPositionBoard();
        Tile nearestTileToClick = ground.GetNearestTileToPosition(clickedPosition);

        if (activePlayer.GetGod().AllowsMove(nearestTileToClick) && ground.OpponentsAllowMove(nearestTileToClick))
        {
            Worker newWorker = nearestTileToClick.PlaceWorker(ground.GetNextWorkerPrefab());
            newWorker.SetPlayer(activePlayer);
            activePlayer.AddWorker(newWorker);
            activePlayer.GetGod().RegisterPlacedWorker();

            if (activePlayer.GetGod().DonePlacingThisTurn())
            {
                return (int)Player.StateId.DoneTurn;
            }
        }
        
        return -1;
    }

    public override int GetStateId()
    {
        return (int)Player.StateId.Placing;
    }
}
