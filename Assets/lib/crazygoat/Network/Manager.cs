using UnityEngine;
using UnityEngine.Events;
using CrazyGoat.Variables;


namespace CrazyGoat.Network {
  public class Manager : GenericSingletonClass<Manager>
  {
      //public static Manager instance;
      public WsServer wsServer;

      /*Manager() {
        if (instance != null) {
          Destroy(gameObject);
        } else {
          instance = this;
          DontDestroyOnLoad(gameObject);
        }
      }*/

      void Start() {
        wsServer.Start();
      }

      void  Update() {
        /*if (Instance._currentConnectionStatus != connectionStatus.Value) {
          onConnectionStatusChanged.Invoke();
          Instance._currentConnectionStatus = connectionStatus.Value;

          if (Instance._currentConnectionStatus == "Disconnected") {
            wsServer.Start();
          }
        }*/
      }

      void OnApplicationQuit()
      {
        wsServer.Close();
        //onConnectionStatusChanged.Invoke();
      }
  }
}