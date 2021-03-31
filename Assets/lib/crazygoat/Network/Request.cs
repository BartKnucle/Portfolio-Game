using System;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using CrazyGoat.Variables;
using CrazyGoat.Events;

namespace CrazyGoat.Network {
    [CreateAssetMenu(menuName = "CrazyGoat/Network/Request")]
    public class Request : ScriptableObject
    {
      public string ServerRequestName;
      public Service service;

      public List<StringVariable> stringVariables;

      public GameEvent onReception;

      public void execute() {
        Message msg = new Message();
        JSONNode dataJSON = JSON.Parse("{}");
        dataJSON["service"] = service.api;
        dataJSON["request"] = this.ServerRequestName;

        foreach (var item in stringVariables)
        {
            dataJSON[item.DatabaseFieldName] = item.Value;
        }

        string msgString = dataJSON.ToString();
        msg.Send(msgString);
      }
    }
}

