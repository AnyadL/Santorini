using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingState : State
{
    public override void EnterState(InputSystem input, Ground ground)
    {
        Debug.Log("Entering Waiting State");
        ground.GetActivePlayer().GetGod().ResetCounters();
        input.ResetMouse0Click();
    }

    public override void ExitState() { return; }

    public override int UpdateState(InputSystem input, Ground ground)
    {
        return (int)Player.StateId.Placing;
    }

    public override int GetStateId()
    {
        return (int)Player.StateId.Waiting; 
    }
}
