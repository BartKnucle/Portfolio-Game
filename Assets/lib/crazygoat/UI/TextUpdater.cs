using UnityEngine;
using UnityEngine.UI;
using CrazyGoat.Variables;

namespace CrazyGoat.UI
{
    [RequireComponent(typeof(UnityEngine.UI.Text), typeof(CrazyGoat.Events.GameEventListener))][AddComponentMenu("CrazyGoat/UI/SetText")]
    public class TextUpdater : MonoBehaviour {
        public GenericVariable variable;

        public void SetText() {
            gameObject.GetComponent<Text>().text = variable.GetString();
        }
    }    
}