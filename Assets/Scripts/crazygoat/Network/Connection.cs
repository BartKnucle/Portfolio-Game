using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using HybridWebSocket;

namespace CrazyGoat {
    public class Connection : MonoBehaviour
    {
        public string server = "ws://localhost";
        public int port = 3000;
        WebSocket _ws;

        public static Connection instance;
        void Awake() {
            if (!instance) {
                instance = this;
            } else {
                Destroy(gameObject);
            }

            // Create the websocket
            _ws = WebSocketFactory.CreateInstance(server + ":" + port);
            //  Binding the events
            _ws.OnOpen += onOpen;
            _ws.OnClose += onClose;
            _ws.OnMessage += onMessage;
        }

        void Start() {
            // Connecting to the server
            _ws.Connect();
        }

        void OnApplicationQuit()
        {
            _ws.Close();
        }

        public void send(string msg) {
            _ws.Send(Encoding.UTF8.GetBytes(msg));
        }

        private void onOpen() {}

        private void onClose(WebSocketCloseCode code) {}

        private void onMessage(byte[] msg) {}

        private void onError(string errMsg) {
            Debug.Log("WS error: " + errMsg);
        }
    }
}
