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
      public List<Service> services;
      public WebSocket webSocket;
      public GameEvent onStatusChanged;
      public StringVariable status;
      public Double startedAt;
      Queue<Action> jobs = new Queue<Action>();
      public Queue<String> messages = new Queue<String>();

      internal void AddJob(Action newJob) {
        jobs.Enqueue(newJob);
      }

      new void Awake() {
        base.Awake();
      }

      void Start() {
        webSocket = WebSocketFactory.CreateInstance(wsServer.address.Value + ":" + wsServer.port.Value);
        //  Binding the events
        webSocket.OnOpen += onConnectionOpen;
        webSocket.OnClose += onConnectionClose;
        webSocket.OnError += onConnectionError;
        webSocket.OnMessage += onMessage;
        status.Value = "";
        webSocket.Connect();
      }

      void Update() {
        while (jobs.Count > 0) 
            jobs.Dequeue().Invoke();

        if (status.Value == "Connected") {
          while (messages.Count > 0) 
          {
            Send(messages.Dequeue());
          }
        }
      }

      private void Send(string data) {
          webSocket.Send(Encoding.UTF8.GetBytes(data));
      }

      void changeStatus(string status) {
        this.status.Value = status;
        jobs.Enqueue(onStatusChanged.Raise);
      }

      private void onConnectionOpen() {
        startedAt = Time.Now();
        changeStatus("Connected");
      }

      private void onConnectionClose(WebSocketCloseCode code) {
        changeStatus("Disconnected");
        //this.Start();
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
        if (status.Value == "Connected") {
          webSocket.Close();
        }
      }
  }
}