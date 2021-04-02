using System.Collections.Generic;
using UnityEngine;

namespace CrazyGoat.Variables
{
    [CreateAssetMenu(menuName = "CrazyGoat/Variables Lists/StringList")]
    public class StringList : ScriptableObject
    {
        public string DatabaseFieldName;
        public string DatabaseSubObjectField;
        public List<string> Value;

        public void SetValue(List<string> value)
        {
            Value = value;
        }
    }
}