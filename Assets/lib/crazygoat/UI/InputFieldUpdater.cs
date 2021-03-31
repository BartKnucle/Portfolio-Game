using UnityEngine;
using UnityEngine.UI;
using CrazyGoat.Variables;

namespace CrazyGoat.UI
{
    [RequireComponent(typeof(UnityEngine.UI.InputField), typeof(CrazyGoat.Events.GameEventListener))][AddComponentMenu("CrazyGoat/UI/SetInputField")]
    public class InputFieldUpdater : MonoBehaviour {
        public GenericVariable variable;

        void Start() {
          SetComponentText();
        }

        public void SetComponentText() {
            InputField inputField = gameObject.GetComponent<InputField>();
            inputField.SetTextWithoutNotify(variable.GetString());
        }

        public void SetVariable(string value) {
            variable.setFromString(value);
        }
    }    
}