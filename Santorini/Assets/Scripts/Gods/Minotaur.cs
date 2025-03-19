using System.Runtime.InteropServices.WindowsRuntime;

public class Minotaur : God
{
    Tile _tileToForceTo = null;

    public override void InitializeMoves()
    {
        base.InitializeMoves();

        _tileToForceTo = null;
    }

    public override bool AllowsMove(Tile fromTile, Tile toTile)
    {
        if(fromTile != null && fromTile.IsTileDirectlyNeighbouring(toTile))
        {
            if(toTile.HasWorkerOnTile() && toTile.GetWorkerOnTile().GetPlayer() != _player)
            {
                _tileToForceTo = GetForceTile(fromTile, toTile);
                if (_tileToForceTo == null)
                {
                    // There's no tile the Minotaur can force the opponent worker on to
                    return false;
                }

                return true;
            }
        }

        return base.AllowsMove(fromTile, toTile);
    }

    Tile GetForceTile(Tile fromTile, Tile toTile)
    {
        Tile.TileNeighbour.Direction direction = Tile.TileNeighbour.Direction.Default;
        foreach (Tile.TileNeighbour neighbour in fromTile.GetTileNeighbours())
        {
            if(neighbour.GetTile() == toTile)
            {
                direction = neighbour.GetDirection();
            }
        }

        foreach (Tile.TileNeighbour neighbour in toTile.GetTileNeighbours())
        {
            if(neighbour.GetDirection() == direction)
            {
                // confirm that the tile the minotaur would force the worker into is a valid tile to be forced into
                // i.e. it's direcly neighbouring the toTile, it doesn't have a worker on it, and it isn't domed
                Tile tile = neighbour.GetTile();
                if(neighbour.IsDirectlyNeighbouring() && !tile.HasWorkerOnTile() && !tile.IsDomed())
                {
                    return tile;
                }
            }
        }

        return null;
    }

    public override Tile TileToMoveOpponentWorkerTo() 
    { 
        return _tileToForceTo; 
    }

}