using UnityEngine;

namespace CrazyGoat.Variables
{
    [CreateAssetMenu(menuName = "CrazyGoat/BoolVariable")]
    public class BoolVariable : ScriptableObject
    {
#if UNITY_EDITOR
        [Multiline]
        public string DeveloperDescription = "";
#endif
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