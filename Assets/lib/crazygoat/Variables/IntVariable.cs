using UnityEngine;

namespace CrazyGoat.Variables
{
    [CreateAssetMenu(menuName = "CrazyGoat/IntVariable")]
    public class IntVariable : GenericVariable
    {
        public int Value;

        public void SetValue(int value)
        {
            Value = value;
        }

        public void SetValue(IntVariable value)
        {
            Value = value.Value;
        }
    }
}