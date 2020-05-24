using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

#pragma warning disable 0618

public class SantoriniNetworkManager : NetworkManager
{
    public override void OnClientConnect(NetworkConnection conn)
    {
        GameObject networkerGO = new GameObject("Networker");
        networkerGO.AddComponent<Networker>();
        GameObject networker = Instantiate(networkerGO);
        NetworkServer.Spawn(networker);

        Santorini santorini = FindObjectOfType<Santorini>();
        santorini.SetNetworker(networker.GetComponent<Networker>());

        base.OnClientConnect(conn);
    }
}
#pragma warning restore 0618