using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoneTurnState : State
{
    public override void EnterState(InputSystem input, Ground ground)
    {
        Debug.Log("Entering Done Turn State");
        input.ResetMouse0Click();
    }

    public override void ExitState()
    {
        return;
    }

    public override int UpdateState(InputSystem input, Ground ground)
    {
        if(!ground.GetActivePlayer().GetGod().FinishedTurn())
        {
            return (int)Player.StateId.Placing;
        }

        return -1;
    }

    public override int GetStateId()
    {
        return (int)Player.StateId.DoneTurn;
    }
}
