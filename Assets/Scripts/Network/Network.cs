using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using HybridWebSocket;
using CrazyGoat;

    public class Network : MonoBehaviour
    {
        public string server = "ws://localhost";
        public int port = 3000;
        public Queue messagesQ = new Queue();
        //private Messages _messages;
        //private Player _player;

        public WebSocket ws;

        public static Network instance;

        void Awake() 
        {
            if (instance) {
            Destroy(gameObject);
            } else {
            instance = this;
            }

            // Create the websocket
            ws = WebSocketFactory.CreateInstance(server + ":" + port);
            //  Binding the events
            ws.OnOpen += _onOpen;
            ws.OnClose += _onClose;
            ws.OnMessage += _onMessage;
            //ws.OnError += _onError;
        }
        

        void Start()
        {
            // Connecting to the server
            ws.Connect();
        }

        void OnApplicationQuit()
        {
            ws.Close();
        }

        public void send(string msg) {
            ws.Send(Encoding.UTF8.GetBytes(msg));
        }

        private void _onOpen() {
            User.instance.sendID();
        }

        private void _onClose(WebSocketCloseCode code) {}

        private void _onMessage(byte[] msg) {
            string message = Encoding.UTF8.GetString(msg);
            messagesQ.Enqueue(message);
            //_messages.dispatch(message);
        }

        private void _onError(string errMsg) {
            Debug.Log("WS error: " + errMsg);
        }

        public void joinLobby() {
            /*send("api/lobby/join/" +_ui.inTeam);
            _ui.hideLobby();*/
        }
    }