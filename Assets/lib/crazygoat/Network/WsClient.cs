using System.Text;
using UnityEngine;

using CrazyGoat.Variables;

namespace CrazyGoat.Network {
  [System.Serializable]
  [CreateAssetMenu(menuName = "CrazyGoat/Network/Server")]
  public class WsClient : ScriptableObject
  {
      public StringReference address;
      public IntReference port;
  }
}
