﻿public class WaitingState : State
{
    public override void EnterState(InputSystem input, Board board)
    {
        if(board.GetActivePlayer() != null)
        {
            board.GetActivePlayer().GetGod().ResetCounters();
        }

        input.ResetMouse0Click();
    }

    public override void ExitState() { return; }

    public override int UpdateState(InputSystem input, Board board)
    {
        return (int)Player.StateId.Placing;
    }

    public override int GetStateId()
    {
        return (int)Player.StateId.Waiting; 
    }
}
