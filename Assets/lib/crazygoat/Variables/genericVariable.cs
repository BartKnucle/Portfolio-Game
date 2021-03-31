using UnityEngine;

namespace CrazyGoat.Variables
{
    public abstract class GenericVariable : ScriptableObject
    {
#if UNITY_EDITOR
        [Multiline]
        public string DeveloperDescription = "";
#endif

      public string DatabaseFieldName;
      public abstract string GetString();

      public abstract void setFromString(string value);
    }
}