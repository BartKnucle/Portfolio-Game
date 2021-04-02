﻿using System;
using UnityEngine;
using SimpleJSON;

namespace CrazyGoat.Network
{
  [CreateAssetMenu(menuName = "CrazyGoat/Network/Object")]
  public class NetMonoBehaviour : ScriptableObject
  {
    public string service = "";

    virtual public void Awake() {
      //Manager.Instance.wsServer.onMsgReceive.AddListener(Receive);
    }

    virtual public void Receive(string service, JSONNode msgObject) {}

    public void Sync(string state = null) {
      Message msg = new Message();
      JSONNode dataJSON = JSON.Parse(JsonUtility.ToJson(this));
      dataJSON["service"] = service;
      dataJSON["state"] = state;
      string msgString = dataJSON.ToString();
      msg.Send(msgString);
    }
  }
}
