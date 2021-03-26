using UnityEngine;
using UnityEngine.Events;
using CrazyGoat.Variables;


namespace CrazyGoat.Network {
  public class Manager : MonoBehaviour
  {
      public static Manager instance;
      public WsServer wsServer;
      public UnityEvent onConnectionStatusChanged;
      public StringReference connectionStatus;
      
      string _currentConnectionStatus;

      Manager () {
        if (instance == null) {
          instance = this;
        }
      }

      void Start() {
        wsServer.Start();
      }

      void  Update() {
        if (instance._currentConnectionStatus != connectionStatus.Value) {
          onConnectionStatusChanged.Invoke();
          instance._currentConnectionStatus = connectionStatus.Value;

          if (instance._currentConnectionStatus == "Disconnected") {
            wsServer.Start();
          }
        }
      }

      void OnApplicationQuit()
      {
        wsServer.Close();
        onConnectionStatusChanged.Invoke();
      }
  }
}