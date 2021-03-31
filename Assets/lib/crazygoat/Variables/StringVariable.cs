using UnityEngine;

namespace CrazyGoat.Variables
{
    [CreateAssetMenu(menuName = "CrazyGoat/StringVariable")]
    public class StringVariable : GenericVariable
    {
        private string value;
        public string Value
        {
          get { return value; }   // get method
          set { SetValue(value); }  // set method
        }

        virtual public void SetValue(string value)
        {
            this.value = value;
        }
    }
}