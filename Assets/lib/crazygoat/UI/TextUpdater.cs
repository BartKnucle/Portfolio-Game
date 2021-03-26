using UnityEngine;
using UnityEngine.UI;
using CrazyGoat.Variables;

namespace CrazyGoat.UI
{
    [RequireComponent(typeof(UnityEngine.UI.Text), typeof(CrazyGoat.Events.GameEventListener))]
    public class TextUpdater : MonoBehaviour {
        public StringReference text;

        public void SetText() {
            string test = gameObject.GetComponent<Text>().text;
            gameObject.GetComponent<Text>().text = text.Variable.Value;
        }
    }    
}