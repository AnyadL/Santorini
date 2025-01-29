public class Hephaestus : God
{
    Tile _firstBuildTile = null;
    bool _endBuildEarly = false;

    public override void EnableRealTurns()
    {
        base.EnableRealTurns();
        _maxBuilds = 2;
    }

    public override void InitializeBuilds()
    {
        base.InitializeBuilds();

        _firstBuildTile = null;
        _endBuildEarly = false;
    }

    public override void RegisterBuild(Tile tile)
    {
        base.RegisterBuild(tile);

        _firstBuildTile = tile;

        // Hephaestus can only build a second time if it's on top of his first build and isn't a dome.
        // So if his first build resulted in a full (but not domed) tower, he can't build again.
        // And if his first build was a dome, he can't build again.
        if(tile.NextTowerPieceIsDome() || tile.IsDomed())
        {
            _endBuildEarly = true;
        }
    }

    public override bool AllowsBuild(Tile tile, Worker worker)
    {
        if(_firstBuildTile != null)
        {
            // we're on the second build, which must be on top of the first
            return base.AllowsBuild(tile, worker) && tile == _firstBuildTile;
        }

        return base.AllowsBuild(tile, worker);
    }

    public override bool EndBuildEarly()
    {
        return _endBuildEarly;
    }
}
