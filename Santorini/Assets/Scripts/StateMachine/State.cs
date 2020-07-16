using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public abstract int UpdateState(InputSystem input, Ground ground);
    public abstract void EnterState(InputSystem input, Ground ground);
    public abstract void ExitState();
    public abstract int GetStateId();
}