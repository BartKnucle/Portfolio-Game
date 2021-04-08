using System;
using System.Collections.Generic;
using UnityEngine;
using CrazyGoat.Variables;

namespace CrazyGoat.Network {
    [CreateAssetMenu(menuName = "CrazyGoat/Network/Service")]
    public class Service : ScriptableObject
    {
      public string api;
      public StringVariable _id;
      public List<Request> requests = new List<Request>();
    }
}

