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
        if (!instance) {
          instance = this;
        } else {
          Destroy(gameObject);
        }
      }

      void  Update() {
        if (_currentConnectionStatus != connectionStatus.Variable.Value) {
          onConnectionStatusChanged.Invoke();
          _currentConnectionStatus = connectionStatus.Variable.Value;

          if (_currentConnectionStatus == "Disconnected") {
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