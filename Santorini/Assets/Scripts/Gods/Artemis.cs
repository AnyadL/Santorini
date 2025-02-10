public class Artemis : God
{
    Tile _lastTile = null;

    bool _midMove = false;

    public override void EnableRealTurns()
    {
        base.EnableRealTurns();
        _maxMoves = 2;
    }

    public override bool AllowsReturnToSelectingState()
    {
        // Player is not allowed to return to the Selecting State if they've already moved once
        return base.AllowsReturnToSelectingState() && !_midMove;
    }

    public override void InitializeMoves()
    {
        base.InitializeMoves();

        _lastTile = null;
        _midMove = false;
    }

    public override void RegisterMove(Tile fromTile, Tile toTile)
    {
        base.RegisterMove(fromTile, toTile);

        _lastTile = fromTile;
        _midMove = true;
    }

    public override bool AllowsMove(Tile fromTile, Tile toTile)
    {
        return base.AllowsMove(fromTile, toTile) && toTile != _lastTile;
    }
}
