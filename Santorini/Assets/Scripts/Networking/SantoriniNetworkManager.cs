using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

#pragma warning disable 0618

public class SantoriniNetworkManager : NetworkManager
{
    public override void OnClientConnect(NetworkConnection conn)
    {
        Santorini santorini = FindObjectOfType<Santorini>();

        GameObject networkedBoardGO = new GameObject("Networked Board");
        NetworkedBoard networkedBoard = networkedBoardGO.AddComponent<NetworkedBoard>();
        santorini.GetBoard().SetNetworkedBoard(networkedBoard);

        NetworkServer.Spawn(networkedBoardGO);

        base.OnClientConnect(conn);
    }
}
#pragma warning restore 0618