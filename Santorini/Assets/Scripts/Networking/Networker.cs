using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

#pragma warning disable 0618

public class Networker : NetworkBehaviour
{
    [SyncVar]
    public int currentPlayer = 0;

    public void SetCurrentPlayer(int player)
    {
        CmdSetCurrentPlayer(player);
    }

    [Command]
    void CmdSetCurrentPlayer(int player)
    {
        currentPlayer = player;
    }

    public void SpawnObject(GameObject gameObject)
    {
        NetworkServer.Spawn(gameObject);
    }
}
#pragma warning restore 0618