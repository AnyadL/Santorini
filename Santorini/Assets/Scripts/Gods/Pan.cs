using System.Collections.Generic;
public class Pan : God
{
    bool _movedDown2Levels = false;

    public override void InitializeMoves()
    {
        base.InitializeMoves();
        _movedDown2Levels = false;
    }

    public override void RegisterMove(Tile fromTile, Tile toTile)
    {
        base.RegisterMove(fromTile, toTile);
        if(fromTile.GetTowerPieceCount() - toTile.GetTowerPieceCount() == 2)
        {
            _movedDown2Levels = true;
        }
    }

    public override bool HasWon(Board board, List<Worker> workers)
    {
        if (_movedDown2Levels)
        {
            // If Pan has moved down 2 levels, he has won.
            // But, if an opponent blocks the win, then he has to move down 2 levels again, so reset the bool
            _movedDown2Levels = false;
            return true;
        }

        // Pan can also win the normal way
        return base.HasWon(board, workers);
    }
}