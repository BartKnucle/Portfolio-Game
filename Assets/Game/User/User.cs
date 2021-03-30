using System;
using UnityEngine;
using CrazyGoat.Network;
using CrazyGoat.Variables;

public class User : GenericSingletonClass<User>
{
    public CrazyGoat.Network.Variables.StringVariable _id;
    public CrazyGoat.Network.Variables.StringVariable nickname;

    public Request setId;
           
    bool _team;

    new void Awake() {
        base.Awake();
    }

    void Start() {
      setId.execute();
      if (!PlayerPrefs.HasKey("user")) {
        _id.Value = Guid.NewGuid().ToString();
        PlayerPrefs.SetString("user", _id.Value);
        PlayerPrefs.Save();
      } else {
          _id.Value = PlayerPrefs.GetString("user");
      }

      if (PlayerPrefs.HasKey("username")) {
          nickname.Value = PlayerPrefs.GetString("username");
      }
    }

    public void setName(string nickname) {
      this.nickname.Value = nickname;
      PlayerPrefs.SetString("username", nickname);
      PlayerPrefs.Save();
    }

    public void setTeam(bool team) {
      this._team = team;
    }

    public void joinLobby() {
      //Sync("joinLobby");
    }
    public void quitLobby() {
      //Sync("quitLobby");
    }
}
