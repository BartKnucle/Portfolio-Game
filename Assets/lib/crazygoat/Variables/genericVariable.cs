using UnityEngine;

namespace CrazyGoat.Variables
{
    public class GenericVariable : ScriptableObject
    {
#if UNITY_EDITOR
        [Multiline]
        public string DeveloperDescription = "";
#endif

      public string variableName;
    }
}