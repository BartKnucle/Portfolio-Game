using System;
using UnityEngine;

namespace CrazyGoat.Variables
{
    [CreateAssetMenu(menuName = "CrazyGoat/FloatVariable")]
    public class FloatVariable : GenericVariable
    {
        private float value;
        public float Value
        {
          get { return value; }   // get method
          set { SetValue(value); }  // set method
        }

        virtual public bool SetValue(float value)
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
          SetValue(Convert.ToSingle(value));
        }
        public override void OnEnable ()
        {
            Debug.Log("Awake");
        }
    }
}