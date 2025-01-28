public abstract class State
{
    public abstract int UpdateState(InputSystem input, Board board);
    public abstract void EnterState(InputSystem input, Board board);
    public abstract void ExitState();
    public abstract int GetStateId();
}