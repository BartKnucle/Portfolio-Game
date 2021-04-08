using System;
using UnityEngine;

namespace CrazyGoat.Variables
{
    [CreateAssetMenu(menuName = "CrazyGoat/IntVariable")]
    public class IntVariable : GenericVariable
    {
        private int value;
        public int Value
        {
          get { return value; }   // get method
          set { SetValue(value); }  // set method
        }

        virtual public bool SetValue(int value)
        {
          if (this.value != value) {
            this.value = value;
            return true;
          } else {
            return false;
          }
        }

        override public string GetString() {
          return Value.ToString();
        }
        override public void setFromString(string value) {
          SetValue(Convert.ToInt16(value));
        }

        public override void OnEnable ()
        {
        }
    }
}