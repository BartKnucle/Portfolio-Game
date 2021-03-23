using System;
using System.Text;
using System.Reflection;
using UnityEngine;
using SimpleJSON;

namespace CrazyGoat.Network
{
  [Serializable]
  public class NetMonoBehaviour : MonoBehaviour
  {
    public string service = "";
    public string state = "";

    public void Awake() {
      Manager.instance.connection.onMsgReceive.AddListener(Receive);
    }

    public void Start() {
      //Manager.instance.connection.onMsgReceive.AddListener(Receive);
    }

    virtual public void Receive(string service, JSONNode msgObject) {}

    public void Sync(string state = null) {
      Message msg = new Message();
      this.state = state;
      msg.Send(JsonUtility.ToJson(this));
    }
  }
}
