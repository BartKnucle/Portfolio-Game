using UnityEngine;
using UnityEngine.UIElements;
using CrazyGoat.Network;
using CrazyGoat.Network.Variables;

[RequireComponent(typeof(UIDocument))]
public class UIJoin : MonoBehaviour
{
    public NetStringVariable nickname;
    public NetBoolVariable team;
    public Request joinLobby;
    private TextField txtNickname;
    private Toggle tglTeam;
    private Button btnJoin;
    private void OnEnable() {
      var rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
      txtNickname = rootVisualElement.Q<TextField>("txt-nickname");
      tglTeam = rootVisualElement.Q<Toggle>("chk-team");
      btnJoin = rootVisualElement.Q<Button>("btn-joinlobby");

      txtNickname.RegisterCallback<InputEvent>(ev => setNickname());
      tglTeam.RegisterCallback<ChangeEvent<bool>>(ev => setTeam());
      btnJoin.RegisterCallback<ClickEvent>(ev => joinLobby.execute());
    }

    private void setNickname() {
      nickname.SetValue(txtNickname.text);
    }

    private void setTeam() {
      team.SetValue(tglTeam.value);
    }
}
