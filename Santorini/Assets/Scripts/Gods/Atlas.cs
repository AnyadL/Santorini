public class Atlas : God
{
    bool _hasBuilt = false;

    public override void InitializeBuilds()
    {
        base.InitializeBuilds();

        _hasBuilt = false;;
    }

    public override void RegisterBuild(Tile tile)
    {
        base.RegisterBuild(tile);

        _hasBuilt = true;
    }

    public override bool HasUniqueBuild() { return true; }

    public override bool AllowedToUniqueBuild() { return !_hasBuilt; }

    public override string GetUniqueBuildText()
    {
        return "Build Dome";
    }

    public override int GetUniqueBuildLevel()
    {
        // Atlas' unique build is a dome.
        return 3;
    }
}
