using System;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using HybridWebSocket;
using SimpleJSON;

namespace CrazyGoat.Network {
  [System.Serializable]
  public class OnMsgReceive : UnityEvent<string, JSONNode>{}
  public class Connection
  {
      public bool connected = false;
      public WebSocket webSocket;

      public Double startedAt;
      public OnMsgReceive onMsgReceive = new OnMsgReceive();

      public void Create (string server, int port) {
        // Create the websocket
        webSocket = WebSocketFactory.CreateInstance(server + ":" + port);
        //  Binding the events
        webSocket.OnOpen += onOpen;
        webSocket.OnClose += onClose;
        webSocket.OnMessage += onMessage;
      }

      public void Start() {
        webSocket.Connect();
      }

      public void Close() {
          if (connected) {
            webSocket.Close();
            connected = false;
          }
      }

      private void onOpen() {
        startedAt = Time.Now();
        connected = true;
      }

      private void onClose(WebSocketCloseCode code) {
        connected = false;
      }

      private void onMessage(byte[] msg) {
        string strMsg = Encoding.UTF8.GetString(msg);
        JSONNode msgObject = JSON.Parse(strMsg);
        string service = msgObject["data"]["service"];
        onMsgReceive.Invoke(service, msgObject);

        //dynamic msgObject = JsonUtility.FromJson<dynamic>(Encoding.UTF8.GetString(msg));
      }

      private void onError(string errMsg) {
          Debug.Log("WS error: " + errMsg);
      }

      public void Send(string data) {
        if (connected) {
          webSocket.Send(Encoding.UTF8.GetBytes(data));
        }
      }
  }
}
