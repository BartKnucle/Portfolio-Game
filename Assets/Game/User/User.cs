using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CrazyGoat.Network;

public class User : NetMonoBehaviour
{
    public static User instance;
    public string username = "";
    [SerializeField][HideInInspector]
    string _id;
    [SerializeField][HideInInspector]
    bool _team;

    new void Awake() {
          if (!instance) {
          instance = this;
        } else {
            Destroy(gameObject);
        }

        base.Awake();

        if (!PlayerPrefs.HasKey("user")) {
            _id = Guid.NewGuid().ToString();
            PlayerPrefs.SetString("user", _id);
            PlayerPrefs.Save();
        } else {
            _id = PlayerPrefs.GetString("user");
        }

        if (PlayerPrefs.HasKey("username")) {
            username = PlayerPrefs.GetString("username");
        }
    }

    void Start() {
      Sync("setId");
    }

    public void setName(string username) {
      this.username = username;
      PlayerPrefs.SetString("username", username);
      PlayerPrefs.Save();
      Sync();
    }

    public void setTeam(bool team) {
      this._team = team;
    }

    public void joinLobby() {
      Sync("joinLobby");
    }
    public void quitLobby() {
      Sync("quitLobby");
    }
}
