public class Artemis : God
{
    Tile _lastTile = null;

    public override void EnableRealTurns()
    {
        base.EnableRealTurns();
        _maxMoves = 2;
    }

    public override void InitializeMoves()
    {
        base.InitializeMoves();

        _lastTile = null;
    }

    public override void RegisterMove(Tile fromTile, Tile toTile)
    {
        base.RegisterMove(fromTile, toTile);

        _lastTile = fromTile;
    }
    public override bool AllowsMove(Tile tile, Worker worker)
    {
        return base.AllowsMove(tile, worker) && tile != _lastTile;
    }
}
