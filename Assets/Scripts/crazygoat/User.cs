using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrazyGoat {
    public class User : MonoBehaviour
    {
        private string _Id;
        public string nickName = "";
        public static User instance;
        void Awake() {
            if (!instance) {
                instance = this;
            } else {
                Destroy(gameObject);
            }

            // Set the user _Id
            if (!PlayerPrefs.HasKey("user")) {
                _Id = Guid.NewGuid().ToString();
                PlayerPrefs.SetString("user", _Id);
                PlayerPrefs.Save();
            } else {
                _Id = PlayerPrefs.GetString("user");
            }
        }

        // Send the user _Id to the server
        public void sendID () {
            Network.instance.send("api/player/connect/" + _Id);
        }

        public void setNickName() {}
    }

}
