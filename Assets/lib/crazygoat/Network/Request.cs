using System;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using CrazyGoat.Variables;

namespace CrazyGoat.Network {
    [CreateAssetMenu(menuName = "CrazyGoat/Network/Request")]
    public class Request : ScriptableObject
    {
      public string request;
      public Service service;

      public List<StringVariable> stringVariables;

      public void execute() {
        Message msg = new Message();
        JSONNode dataJSON = JSON.Parse("{}");
        dataJSON["service"] = service.api;
        dataJSON["request"] = this.name;

        foreach (var item in stringVariables)
        {
            dataJSON[item.variableName] = item.Value;
        }

        string msgString = dataJSON.ToString();
        msg.Send(msgString);
      }
    }
}

