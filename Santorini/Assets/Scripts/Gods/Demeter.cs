public class Demeter : God
{
    Tile _firstBuildTile = null;

    public override void EnableRealTurns()
    {
        base.EnableRealTurns();
        _maxBuilds = 2;
    }

    public override void InitializeBuilds()
    {
        base.InitializeBuilds();

        _firstBuildTile = null;
    }

    public override void RegisterBuild(Tile tile)
    {
        base.RegisterBuild(tile);

        _firstBuildTile = tile;
    }

    public override bool AllowsBuild(Tile tile, Worker worker)
    {
        return base.AllowsBuild(tile, worker) && tile != _firstBuildTile;
    }
}
