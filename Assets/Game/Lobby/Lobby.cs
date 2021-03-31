using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CrazyGoat.Variables;
using CrazyGoat.Events;
using CrazyGoat.Network;

public class Lobby : MonoBehaviour
{
    public Request joinRequest;
    public Request quitRequest;

    public GameEvent onStatusChanged;
    // Start is called before the first frame update

    public void Join() {
        joinRequest.execute();
    }

    public void Quit () {
        quitRequest.execute();
    }
}
