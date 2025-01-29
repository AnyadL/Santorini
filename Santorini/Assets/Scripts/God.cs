using System.Collections.Generic;

public abstract class God
{
    int _moves = 0;
    int _builds = 0;
    int _placedWorkersThisTurn = 0;
    int _placedWorkers = 0;

    protected int _maxMoves = 0;
    protected bool _movesStarted = false;
    protected bool _movesEnded = false;
    protected int _maxBuilds = 0;
    protected bool _buildsStarted = false;
    protected bool _buildsEnded = false;
    protected int _placedWorkersPerTurn = 1;
    protected int _maxPlacedWorkers = 2;

    public virtual void Initialize() { return; }

    public virtual void ResetCounters()
    {
        InitializeMoves();
        InitializeBuilds();
        InitializePlacedWorkersThisTurn();
    }

    public virtual void EnableRealTurns()
    {
        _maxMoves = 1;
        _maxBuilds = 1;
        _placedWorkersPerTurn = 0;
    }

    public virtual bool FinishedTurn()
    {
        return DonePlacingThisTurn() && DoneMoving() && DoneBuilding();
    }

    public virtual bool HasAvailableMove(Worker worker)
    {
        foreach(Tile.TileNeighbour neighbour in worker.GetTile().GetTileNeighbours())
        {
            if(AllowsMove(neighbour.GetTile(), worker))
            {
                return true;
            }
        }

        return false;
    }

    public virtual bool AllowsReturnToSelectingState()
    {
        // Most Gods can re-select their worker while in the Move State
        return _movesStarted && !_buildsStarted;
    }

    public virtual bool AllowedToUndoTurn()
    {
        // as long as they've started their turn, they're allowed to undo their turn
        return HasMoved();        
    }

    public virtual bool AllowsMove(Tile tile, Worker worker)
    {
        return AllowsMove(tile) && worker.GetTile().IsTileDirectlyNeighbouring(tile);
    }

    public virtual bool AllowsMove(Tile tile)
    {
        return !tile.HasWorkerOnTile();
    }

    public virtual bool AllowedToEndMove()
    {
        return AllowsMultipleMoves() && HasMoved() && !DoneMoving();
    }

    bool AllowsMultipleMoves()
    {
        return _maxMoves > 1;
    }

    bool HasMoved()
    {
        return _moves > 0;
    }

    public virtual bool AllowsOpponentMove(Tile tile) { return true; }
    public virtual bool AllowsOpponentMove(Worker worker, Tile tile) { return true; }
    
    public virtual bool AllowsBuild(Tile tile, Worker worker)
    {
        if(tile.HasWorkerOnTile())
        {
            return false;
        }

        if(!worker.GetTile().IsTileDirectlyNeighbouring(tile))
        {
            return false;
        }

        return true;
    }

    public virtual bool AllowedToEndBuild()
    {
        return AllowsMultipleBuilds() && HasBuilt() && !DoneBuilding();
    }

    bool AllowsMultipleBuilds()
    {
        return _maxBuilds > 1;
    }

    bool HasBuilt()
    {
        return _builds > 0;
    }

    public virtual bool AllowsOpponentBuild(Worker worker, Tile tile) { return true; }

    public virtual bool HasWon(Board board, List<Worker> workers)
    {
        foreach(Worker worker in workers)
        {
            if(worker.GetTile().GetLevel() == Tile.Level.Level3)
            {
                return true;
            }
        }

        return false;
    }

    public virtual bool PreventsWin(Player opponent) { return false; }

    public virtual void InitializeMoves() { _moves = 0; _movesEnded = false; _movesStarted = false; }
    public virtual void RegisterMove(Tile fromTile, Tile toTile) { ++_moves; _movesStarted = true; }
    public virtual bool DoneMoving() { return _movesEnded || _moves >= _maxMoves; }
    public virtual void EndMove() { _movesEnded = true; _movesStarted = true; }

    public virtual void InitializeBuilds() { _builds = 0; _buildsEnded = false; _buildsStarted = false; }
    public virtual void RegisterBuild(Tile tile) { ++_builds; _buildsStarted = true; }
    public virtual bool DoneBuilding() { return _buildsEnded || _builds >= _maxBuilds; }
    public virtual void EndBuild() { _buildsEnded = true; _buildsStarted = true; }

    public virtual void InitializePlacedWorkersThisTurn() { _placedWorkersThisTurn = 0; }
    public virtual void RegisterPlacedWorker() { ++_placedWorkers; ++_placedWorkersThisTurn; }
    public virtual bool DonePlacing() { return _placedWorkers >= _maxPlacedWorkers; }
    public virtual bool DonePlacingThisTurn() { return _placedWorkersThisTurn >= _placedWorkersPerTurn; }
}