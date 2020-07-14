using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class God
{
    protected abstract void OnStartNewTurn();
    protected abstract void PlaceWorker(List<Worker> workers);
    protected abstract void TurnSequence(List<Worker> workers);
    protected abstract void Select(List<Worker> workers);
    protected abstract void Move(List<Worker> workers);
    protected abstract void Build(List<Worker> workers);
}