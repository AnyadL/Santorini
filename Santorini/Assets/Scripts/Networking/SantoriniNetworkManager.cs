using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

#pragma warning disable 0618

public class SantoriniNetworkManager : NetworkManager
{
    public override void OnClientConnect(NetworkConnection conn)
    {
        GameObject networker = new GameObject("Networker");
        networker.AddComponent<Networker>();
        NetworkServer.Spawn(networker);

        Santorini santorini = FindObjectOfType<Santorini>();
        santorini.SetNetworker(networker.GetComponent<Networker>());

        foreach( Tile tile in santorini.GetGround().GetTiles())
        {
            GameObject networkedTileGO = new GameObject("Networked Tile");
            NetworkedTile networkedTile = networkedTileGO.AddComponent<NetworkedTile>();
            networkedTileGO.transform.parent = tile.transform;
            tile.SetNetworkedTile(networkedTile);

            NetworkServer.Spawn(networkedTileGO);
        }

        base.OnClientConnect(conn);
    }
}
#pragma warning restore 0618