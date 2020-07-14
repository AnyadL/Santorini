using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum Status
    {
        Waiting,
        Placing,
        Selecting,
        Moving,
        Building,
        DoneTurn,
        Won
    }

    public Status _status = Status.Waiting;

    StateMachine _stateMachine = default;

    God _god = default;
    List<Worker> _workers = default;

    protected InputSystem _input = default;
    protected Ground _ground = default;

    public void Initialize(InputSystem input, Ground ground, Worker.Colour colour)
    {
        _input = input;
        _ground = ground;
        _workers = new List<Worker>() { new Worker(), new Worker() };
        _workers[0].Initialize(Worker.Gender.Female, colour);
        _workers[1].Initialize(Worker.Gender.Male, colour);

        _stateMachine = new StateMachine();
    }

    public void PlayTurn(bool activePlayer)
    {
        if(!activePlayer)
        {
            return;
        }

        _stateMachine.UpdateCurrentState();
    }

    public God GetGod()
    {
        return _god;
    }

    public List<Worker> GetWorkers()
    {
        return _workers;
    }

    public Status GetStatus()
    {
        return _status;
    }
}
