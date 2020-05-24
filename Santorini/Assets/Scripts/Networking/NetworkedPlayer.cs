using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

#pragma warning disable 0618

public class NetworkedPlayer : NetworkBehaviour
{
    int _playerId = default;

    void Start()
    {
        Santorini santorini = GameObject.FindObjectOfType<Santorini>();
        _playerId = santorini.RegisterPlayer(this);
    }
}
#pragma warning restore 0618