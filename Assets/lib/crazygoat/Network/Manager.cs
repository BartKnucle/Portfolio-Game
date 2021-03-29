using System;
using System.Text;
using System.Collections.Generic;
using CrazyGoat.Variables;
using CrazyGoat.Events;
using HybridWebSocket;
using SimpleJSON;


namespace CrazyGoat.Network {
  public class Manager : GenericSingletonClass<Manager>
  {
      //public static Manager instance;
      public WsServer wsServer;
      public WebSocket webSocket;
      public GameEvent onStatusChanged;
      public StringReference status;
      public Double startedAt;
      Queue<Action> jobs = new Queue<Action>();

      internal void AddJob(Action newJob) {
        jobs.Enqueue(newJob);
      }

      public override void Awake() {
        base.Awake();
        status.Variable.Value = "Disconnected";
        webSocket = WebSocketFactory.CreateInstance(wsServer.address.Value + ":" + wsServer.port.Value);
        //  Binding the events
        webSocket.OnOpen += onConnectionOpen;
        webSocket.OnClose += onConnectionClose;
        webSocket.OnError += onConnectionError;
        webSocket.OnMessage += onMessage;
      }

      void Start() {
        if (status.Variable.Value == "Disconnected") {
          webSocket.Connect();
        }
      }

      void Update() {
        while (jobs.Count > 0) 
            jobs.Dequeue().Invoke();
      }

      public void Send(string data) {
        if (status.Variable.Value == "Connected")
        {
          webSocket.Send(Encoding.UTF8.GetBytes(data));
        }
      }

      void changeStatus(string status) {
        this.status.Variable.Value = status;
        jobs.Enqueue(onStatusChanged.Raise);
      }

      private void onConnectionOpen() {
        startedAt = Time.Now();
        changeStatus("Connected");
      }

      private void onConnectionClose(WebSocketCloseCode code) {
        changeStatus("Disconnected");
        this.Start();
      }

      private void onMessage(byte[] msg) {
        string strMsg = Encoding.UTF8.GetString(msg);
        JSONNode msgObject = JSON.Parse(strMsg);
        string service = msgObject["data"]["service"];
        //onMsgReceive.Invoke(service, msgObject);

        //dynamic msgObject = JsonUtility.FromJson<dynamic>(Encoding.UTF8.GetString(msg));
      }

      private void onConnectionError(string errMsg) {
        changeStatus(errMsg);
      }
      
      void OnApplicationQuit()
      {
        if (status.Variable.Value == "Connected") {
          webSocket.Close();
        }
      }
  }
}