using UnityEngine;
using UnityEngine.Events;


namespace CrazyGoat.Network {
  public class Manager : MonoBehaviour
  {
      public static Manager instance;
      public WsServer wsServer;
      public UnityEvent onConnectionOpen;
      //public string server = "ws://localhost";
      //public int port = 3000;

      Manager () {
        if (!instance) {
          instance = this;
        } else {
          Destroy(gameObject);
        }
      }

      void OnApplicationQuit()
      {
        wsServer.Close();
      }
  }
}