﻿public class DoneTurnState : State
{
    public override void EnterState(InputSystem input, Board board)
    {
        input.ResetMouse0Click();
    }

    public override void ExitState() { return; }

    public override int UpdateState(InputSystem input, Board board)
    {
        if(!board.GetActivePlayer().GetGod().FinishedTurn())
        {
            if(board.GetActivePlayer() != null)
            {
                board.GetActivePlayer().OnTurnEnd();
            }
            
            return (int)Player.StateId.Placing;
        }

        return -1;
    }

    public override int GetStateId()
    {
        return (int)Player.StateId.DoneTurn;
    }
}
