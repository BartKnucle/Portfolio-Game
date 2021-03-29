using System;
using System.Collections.Generic;
using UnityEngine;

namespace CrazyGoat.Network {
    public class Message
    {
      public void Send(string data) {
        Manager.Instance.wsServer.Send(data);
      }
    }
}

