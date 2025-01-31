public class Athena : God
{
    bool _movedUp = false;

    public override void OnTurnStart()
    {
        base.OnTurnStart();

        _movedUp = false;
    }

    public override void RegisterMove(Tile fromTile, Tile toTile)
    {
        base.RegisterMove(fromTile, toTile);

        if (fromTile.GetTowerPieceCount() - toTile.GetTowerPieceCount() < 0)
        {
            _movedUp = true;
        }
    }

    public override bool AllowsOpponentMove(Worker worker, Tile tile)
    {
        // If Athena has moved up this turn, opponents are not allowed to move up
        if(_movedUp && worker.GetTile().GetTowerPieceCount() - tile.GetTowerPieceCount() < 0)
        {
            return false;
        }

        return base.AllowsOpponentMove(worker, tile);
    }
}
