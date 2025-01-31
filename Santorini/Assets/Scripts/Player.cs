using System.Collections.Generic;

public class Player
{
    public enum StateId
    {
        Waiting = 0,
        Placing = 1,
        Selecting = 2,
        Moving = 3,
        Building = 4,
        WaitingOnConfirmation = 5,
        DoneTurn = 6
    }

    StateMachine _stateMachine = default;

    protected InputSystem _input = default;
    protected Board _board = default;

    God _god = default;
    List<Worker> _workers = default;
    Worker.Colour _colour = default;

    Worker _selectedWorker = default;
    bool _hasLost = false;

    public void Initialize(InputSystem input, Board board, Worker.Colour colour)
    {
        _input = input;
        _board = board;
        _workers = new List<Worker>();
        _colour = colour;

        if(colour == Worker.Colour.Blue)
        {
            _god = new Athena();
        } else {
            _god = new Pan();
        }

        _god.Initialize();

        _stateMachine = new StateMachine();
        _stateMachine.Initialize(input, board, colour);

        WaitingState waitingState = new WaitingState();
        _stateMachine.RegisterState(waitingState);

        PlacingState placingState = new PlacingState();
        _stateMachine.RegisterState(placingState);

        SelectingState selectingState = new SelectingState();
        _stateMachine.RegisterState(selectingState);
        
        MovingState movingState = new MovingState();
        _stateMachine.RegisterState(movingState);

        BuildingState buildingState = new BuildingState();
        _stateMachine.RegisterState(buildingState);

        WaitingOnConfirmationState waitingOnConfirmationState = new WaitingOnConfirmationState();
        _stateMachine.RegisterState(waitingOnConfirmationState);

        DoneTurnState doneTurnState = new DoneTurnState();
        _stateMachine.RegisterState(doneTurnState);

        _stateMachine.SetState((int)StateId.Waiting);
    }

    public void UpdatePlayer(bool activePlayer)
    {
        if (!activePlayer)
        {
            return;
        }

        _stateMachine.UpdateCurrentState();
    }

    public void OnTurnStart()
    {
        _god.OnTurnStart();
    }

    public void OnTurnEnd()
    {
        _god.OnTurnEnd();
    }

    public bool PreventsWin(Player opponent)
    {
        return _god.PreventsWin(opponent);
    }

    public bool HasWon()
    {
        return _god.HasWon(_board, _workers);
    }

    public bool HasLost()
    {
        return _hasLost;
    }

    public void SetHasLost(bool lost)
    {
        _hasLost = lost;
    }

    public bool IsDoneTurn()
    {
        return _stateMachine.GetCurrentStateId() == (int)StateId.DoneTurn;
    }

    public bool IsWaiting()
    {
        return _stateMachine.GetCurrentStateId() == (int)StateId.Waiting;
    }

    public bool IsBuilding()
    {
        return _stateMachine.GetCurrentStateId() == (int)StateId.Building;
    }

    public bool IsMoving()
    {
        return _stateMachine.GetCurrentStateId() == (int)StateId.Moving;
    }

    public bool IsWaitingOnConfirmation()
    {
        return _stateMachine.GetCurrentStateId() == (int)StateId.WaitingOnConfirmation;
    }

    public bool HasAvailableMove()
    {
        foreach(Worker worker in _workers)
        {
            List<Tile> possibleMoves = _board.GetAvailableMoves(worker);

            foreach(Tile tile in possibleMoves)
            {
                if(_god.AllowsMove(tile, worker))
                {
                    return true;
                }
            }
        }

        return false;
    }
    
    public void FinalizeTurn()
    {
        if(_god.DonePlacing())
        {
            _god.EnableRealTurns();
        }
        
        _stateMachine.SetState((int)StateId.Waiting);
    }

    public bool TrySelectWorker(Worker worker)
    {
        if(_workers.Contains(worker))
        {
            _selectedWorker = worker;
            return true;
        }

        return false;
    }

    public Worker GetSelectedWorker()
    {
        return _selectedWorker;
    }

    public God GetGod()
    {
        return _god;
    }

    public List<Worker> GetWorkers()
    {
        return _workers;
    }

    public void AddWorker(Worker worker)
    {
        _workers.Add(worker);
    }

    public Worker.Colour GetColour()
    {
        return _colour;
    }

    public Player.StateId GetCurrentState()
    {
        return (Player.StateId) _stateMachine.GetCurrentStateId();
    }
}
