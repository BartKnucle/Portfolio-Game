using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrazyGoat.Network {
  public class Manager : MonoBehaviour
  {
      public static Manager instance;
      public Connection connection = new Connection();
      public string server = "ws://localhost";
      public int port = 3000;

      Manager () {
        if (!instance) {
          instance = this;
        } else {
          Destroy(gameObject);
        }
      }

      void Awake() {
        connection.Create(server, port);
        connection.Start();
      }
      void OnApplicationQuit()
      {
        connection.Close();
      }
  }
}