using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artemis : God
{
    public override void EnableRealTurns()
    {
        base.EnableRealTurns();
        _maxMoves = 2;
    }
}
