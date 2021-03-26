using System;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using HybridWebSocket;
using SimpleJSON;
using CrazyGoat.Variables;
using CrazyGoat.Events;

namespace CrazyGoat.Network {
  [System.Serializable]
  [CreateAssetMenu(menuName = "CrazyGoat/Network/Server")]
  public class WsServer : ScriptableObject
  {
      public StringReference wsServer;
      public IntReference wsServerPort;
      public bool isConnected = false;
      public WebSocket webSocket;

      public Double startedAt;
      public UnityEvent<string, JSONNode> onMsgReceive = new UnityEvent<string, JSONNode>();
      public UnityEvent connected;
      public UnityEvent disconnected;

      void OnEnable() {
        webSocket = WebSocketFactory.CreateInstance(wsServer.Variable.Value + ":" + wsServerPort.Variable.Value);
        //  Binding the events
        webSocket.OnOpen += onOpen;
        webSocket.OnClose += onClose;
        webSocket.OnMessage += onMessage;
        webSocket.Connect();
      }

      public void Close() {
          if (isConnected) {
            webSocket.Close();
            isConnected = false;
          }
      }

      private void onOpen() {
        startedAt = Time.Now();
        isConnected = true;
        connected.Invoke();
      	UnityMainThreadDispatcher.Instance().Enqueue(() => connected.Invoke());
      }

      private void onClose(WebSocketCloseCode code) {
        isConnected = false;
        UnityMainThreadDispatcher.Instance().Enqueue(() => disconnected.Invoke());
        //this.serverDisconnected.Invoke();
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
        if (isConnected) {
          webSocket.Send(Encoding.UTF8.GetBytes(data));
        }
      }
  }
}
