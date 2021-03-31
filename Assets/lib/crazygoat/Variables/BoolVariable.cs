using UnityEngine;

namespace CrazyGoat.Variables
{
    [CreateAssetMenu(menuName = "CrazyGoat/BoolVariable")]
    public class BoolVariable : GenericVariable
    {
        public bool Value;

        public void SetValue(bool value)
        {
            Value = value;
        }

        public void SetValue(BoolVariable value)
        {
            Value = value.Value;
        }
    }
}