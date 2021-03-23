﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CrazyGoat.Network;
using SimpleJSON;

namespace CrazyGoat {
  public class Game : NetMonoBehaviour
  {
      string _id;
      //private Map _map;
      //private Player _player;
      //public string ID = "";
      //public bool teams;
      //public bool bot;
      //public int totalScore = 0;
      //public int maxScore = 0;
      //public float time = 0;
      //public float bestScore = 0;

      //public List<Color> Colors = new List<Color>();
      //private bool _isTraining = false;

      public static Game instance;

      Game() {
        if (!instance) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
      }

      new void Awake() {
        base.Awake();
          //_isTraining = GameObject.Find("/IA").GetComponent<ia>().isActiveAndEnabled == true;

          //_map = transform.GetChild(0).GetComponent<Map>();
          //_player = transform.GetChild(1).GetChild(0).GetComponent<Player>();
          //_player.color = Colors[0];
      }

      public override void Receive(string service, JSONNode msgObject) {
        if (service == this.service) {
          switch ((string)msgObject["data"]["state"])
          {
            case "newGame":
              break;
          }
        }
      }

      /*void Start() {
          _map.generateMaze();

          _player.name = "Player0";
          _player.reset();

          for (int i = 1; i < 4; i++)
          {
              Player player = GameObject.Instantiate(_player, transform.GetChild(1));
              player.name = "Player" + i;
              player.color = Colors[i];
              player.index = i;
              player.reset();
          }
      }*/

      /*void Update () {
          time += Time.deltaTime;
          _checkEndGame();
      }*/

      public void load() {
          SceneManager.LoadScene("Game", LoadSceneMode.Single);
      }
      
      /*public void create(string id, int seed) {
          this.ID = id;
          _map.seed = seed;
      }*/

      /*public void reset() {
          time = 0;
          totalScore = 0;
          maxScore = 0;
          bestScore = 0;
          this.ID = "";
          //_map.generateMaze();
          _map.reset();
          transform.GetChild(6).GetComponent<Minimap>().reset();
          for (int i = 0; i < 4; i++)
          {
              transform.GetChild(1).GetChild(i).GetComponent<Player>().reset();
          }
      }*/

      /*private void _checkEndGame() {
          if (totalScore == maxScore) {
              reset();
          }
      }*/

      /*public void addScore() {
          totalScore += 1;
          //_checkEndGame();
      }*/

      /*public void rmScore() {
          totalScore += 1;
          //_checkEndGame();
      }*/
  }
}