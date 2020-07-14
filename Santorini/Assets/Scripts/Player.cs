using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
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
    }

    public God GetGod()
    {
        return _god;
    }

    public List<Worker> GetWorkers()
    {
        return _workers;
    }
}
