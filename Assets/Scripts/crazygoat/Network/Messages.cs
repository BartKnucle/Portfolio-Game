using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrazyGoat {
    public class Messages
    {
        public Messages instance;
        public Queue _incoming = new Queue();
        public Queue _outgoing = new Queue();
    }
}
