using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

#pragma warning disable 0618

public class NetworkedBoard : NetworkBehaviour
{
    public class WorkerRequest
    {
        public Worker.Colour colour;
        public Worker.Gender gender;
    }

    public bool addWorkerRequestSucceeded = false;
    public bool addWorkerRequestCompleted = false;
    
    // Client

    public void SendAddWorkerRequest(Worker.Colour workerColour, Worker.Gender workerGender)
    {
        CmdAddWorkerRequest(workerColour, workerGender);
    }

    public void SendRemoveWorkerRequest(Worker.Colour workerColour, Worker.Gender workerGender)
    {
        CmdRemoveWorkerRequest(workerColour, workerGender);
    }

    [ClientRpc]
    public void RpcAddWorkerResponse(bool succeeded)
    {
        addWorkerRequestCompleted = true;
        addWorkerRequestSucceeded = succeeded;
    }

    [ClientRpc]
    public void RpcRemoveWorkerResponse(bool succeeded)
    {

    }

    // Server

    public WorkerRequest addWorkerRequest;
    public WorkerRequest removeWorkerRequest;

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

    public void SendAddWorkerResponse(bool succeeded)
    {
        RpcAddWorkerResponse(succeeded);
    }

    public void SendRemoveWorkerResponse(bool succeeded)
    {
        RpcRemoveWorkerResponse(succeeded);
    }
}
#pragma warning restore 0618