using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

#pragma warning disable 0618

public class NetworkedTile : NetworkBehaviour
{
    public class WorkerRequest
    {
        public Worker.Colour colour;
        public Worker.Gender gender;
    }

    public WorkerRequest addWorkerRequest;
    public WorkerRequest removeWorkerRequest;

    public void SendAddWorkerRequest(Worker.Colour workerColour, Worker.Gender workerGender)
    {
        CmdAddWorkerRequest(workerColour, workerGender);
    }

    public void SendRemoveWorkerRequest(Worker.Colour workerColour, Worker.Gender workerGender)
    {
        CmdRemoveWorkerRequest(workerColour, workerGender);
    }

    [Command]
    public void CmdAddWorkerRequest(Worker.Colour workerColour, Worker.Gender workerGender)
    {
        addWorkerRequest = new WorkerRequest() { colour = workerColour, gender = workerGender };
    }

    [Command]
    public void CmdRemoveWorkerRequest(Worker.Colour workerColour, Worker.Gender workerGender)
    {
        removeWorkerRequest = new WorkerRequest() { colour = workerColour, gender = workerGender };
    }
}
#pragma warning restore 0618