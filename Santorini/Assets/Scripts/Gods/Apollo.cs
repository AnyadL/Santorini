public class Apollo : God
{
    Tile _tileMovingFrom = null;

    public override void InitializeMoves()
    {
        base.InitializeMoves();

        _tileMovingFrom = null;
    }

    public override bool AllowsMove(Tile fromTile, Tile toTile)
    {
        _tileMovingFrom = fromTile;

        if(fromTile != null)
        {
            // Apollo can't move onto his own workers
            if(toTile.HasWorkerOnTile() && toTile.GetWorkerOnTile().GetPlayer() == _player)
            {
                return false;
            }

            // Apollo can move onto other workers and force them into his previous position
            return fromTile.IsTileDirectlyNeighbouring(toTile);
        }

        // If there is no fromTile, that means we're still Placing workers
        // In this case Apollo does need to check for blocking workers
        return !toTile.HasWorkerOnTile();
    }

    public override Tile TileToMoveOpponentWorkerTo() 
    { 
        return _tileMovingFrom; 
    }

}