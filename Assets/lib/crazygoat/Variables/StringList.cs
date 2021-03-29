using System.Collections.Generic;
using UnityEngine;

namespace CrazyGoat.Variables
{
    [CreateAssetMenu(menuName = "CrazyGoat/Variables Lists/StringList")]
    public class StringList : ScriptableObject
    {
#if UNITY_EDITOR
        [Multiline]
        public string DeveloperDescription = "";
#endif
        public List<string> Value;

        public void SetValue(List<string> value)
        {
            Value = value;
        }
    }
}