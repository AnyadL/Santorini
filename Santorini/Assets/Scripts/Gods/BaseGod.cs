using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGod : MonoBehaviour, IGod
{
    public void TurnSequence()
    {
        Move();
        Build();
    }

    public void Build()
    {
        throw new System.NotImplementedException();
    }

    public void Move()
    {
        throw new System.NotImplementedException();
    }
}
