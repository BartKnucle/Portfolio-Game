using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CrazyGoat.Variables;
using CrazyGoat.Events;
using CrazyGoat.Network;

public class Lobby : MonoBehaviour
{
    public StringVariable status;
    public Request joinRequest;
    public Request quitRequest;

    public GameEvent onStatusChanged;
    // Start is called before the first frame update
    void Start()
    {
        status.Value = "";
    }

    public void Join() {
        joinRequest.execute();
        status.Value = "join";
    }

    public void Quit () {
        quitRequest.execute();
        status.Value = "quit";
    }
}
