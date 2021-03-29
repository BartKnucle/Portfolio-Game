using UnityEngine;
using SimpleJSON;

namespace CrazyGoat.Network.Variables
{
    [CreateAssetMenu(menuName = "CrazyGoat/Network/StringVariable")]
    public class StringVariable : CrazyGoat.Variables.StringVariable {

      public bool autoSend = false;
      public Service service;

      override public void SetValue(string value)
      {
          base.SetValue(value);
          if (autoSend) {
            Send();
          }
      }

      public void Send() {
        Message msg = new Message();
        JSONNode dataJSON = JSON.Parse("{}");
        dataJSON["service"] = service.api;
        if (service._id) {
          dataJSON["_id"] = service._id.Value;
        }
        dataJSON[this.name] = this.Value;
        string msgString = dataJSON.ToString();
        msg.Send(msgString);
      }
    }
}