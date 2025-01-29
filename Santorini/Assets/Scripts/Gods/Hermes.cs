public class Hermes : God
{
    bool _hasMovedOnce = false;
    bool _hasMovedUpOrDown = false;

    public override void EnableRealTurns()
    {
        base.EnableRealTurns();
        _maxMoves = int.MaxValue;
    }

    public override void InitializeMoves()
    {
        base.InitializeMoves();

        _hasMovedOnce = false;
        _hasMovedUpOrDown = false;
    }

    public override bool AllowsReturnToSelectingState()
    {
        // Hermes is not allowed to return to the Selecting State if he's moved up or down
        // But unlike most Gods, he is allowed to return to the Selecting State while in the Building State
        return !_hasMovedUpOrDown;
    }

    public override bool AllowedToEndMove()
    {
        // Hermes is allowed to move anywhere from 0 to infinite times
        return !DoneMoving();
    }
    public override void RegisterMove(Tile fromTile, Tile toTile)
    {
        base.RegisterMove(fromTile, toTile);

        _hasMovedOnce = true;
        
        // If Hermes moves up or down, he's only allowed to move once
        if(fromTile.GetLevel() != toTile.GetLevel())
        {
            _hasMovedUpOrDown = true;
            EndMove();
        }
    }

    public override bool AllowsMove(Tile tile, Worker worker)
    {
        // Hermes cannot move up or down once he's decided to move more than once
        if(_hasMovedOnce && tile.GetLevel() != worker.GetTile().GetLevel())
        {
            return false;
        }

        return base.AllowsMove(tile, worker);
    }
}
