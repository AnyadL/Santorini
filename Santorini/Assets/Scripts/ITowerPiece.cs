using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITowerPiece
{
    int GetLevel();
    void OnStart();
    void OnUpdate();
    void OnFixedUpdate();
}
