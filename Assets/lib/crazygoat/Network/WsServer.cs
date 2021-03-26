using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using HybridWebSocket;
using SimpleJSON;
using CrazyGoat.Variables;

namespace CrazyGoat.Network {
  [System.Serializable]
  [CreateAssetMenu(menuName = "CrazyGoat/Network/Server")]
  public class WsServer : ScriptableObject
  {
      public StringReference wsServer;
      public IntReference wsServerPort;
      
      public StringReference status;
      public WebSocket webSocket;

      public Double startedAt;
      public UnityEvent<string, JSONNode> onMsgReceive = new UnityEvent<string, JSONNode>();

      void OnEnable() {
        status.Variable.Value = "Disconnected";
        webSocket = WebSocketFactory.CreateInstance(wsServer.Variable.Value + ":" + wsServerPort.Variable.Value);
        //  Binding the events
        webSocket.OnOpen += onOpen;
        webSocket.OnClose += onClose;
        webSocket.OnError += onError;
        webSocket.OnMessage += onMessage;
      }

      public void Start() {
        if (status.Variable.Value == "Disconnected") {
          status.Variable.Value = "Connecting";
          webSocket.Connect();
        }
      }

      public void Close() {
          if (status.Variable.Value == "Connected") {
            webSocket.Close();
            status.Variable.Value = "Disconnected";
          }
      }

      private void onOpen() {
        startedAt = Time.Now();
        status.Variable.Value = "Connected";
      }

      private void onClose(WebSocketCloseCode code) {
        status.Variable.Value = "Disconnected";
      }

      private void onMessage(byte[] msg) {
        string strMsg = Encoding.UTF8.GetString(msg);
        JSONNode msgObject = JSON.Parse(strMsg);
        string service = msgObject["data"]["service"];
        onMsgReceive.Invoke(service, msgObject);

        //dynamic msgObject = JsonUtility.FromJson<dynamic>(Encoding.UTF8.GetString(msg));
      }

      private void onError(string errMsg) {
        status.Variable.Value = "Error";
      }

      public void Send(string data) {
        if (status.Variable.Value == "Connected")
        {
          webSocket.Send(Encoding.UTF8.GetBytes(data));
        }
      }
  }
}
