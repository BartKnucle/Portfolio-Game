using UnityEngine;
using UnityEngine.UIElements;
using CrazyGoat.Variables;

[RequireComponent(typeof(UIDocument))]
public class UIConnectionStatus : MonoBehaviour
{
    public StringVariable status;
    private Label lblConnectionStatus;

    private void OnEnable() {
      var rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
      lblConnectionStatus = rootVisualElement.Q<Label>("lbl-connection-status");

      setConnectionStatus();
    }

    public void setConnectionStatus() {
      lblConnectionStatus.text = status.Value;
    }
}
