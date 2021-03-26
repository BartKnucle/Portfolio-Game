using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CrazyGoat.Network;
using CrazyGoat.Network.Variables;
using SimpleJSON;

namespace Game
{
  [System.Serializable]
  public class Player {
  public string _id;
  }
  public class Game : NetMonoBehaviour
  {
      [Tooltip("Game ID")]
      public StringReference _id;
      
      public int seed;
      [SerializeField]
      List<Player> players = new List<Player>(0);
      string _state;
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

      override public void Awake() {
        if (!instance) {
            instance = this;
        } else {
            Destroy(gameObject);
        }

        base.Awake();
          //_isTraining = GameObject.Find("/IA").GetComponent<ia>().isActiveAndEnabled == true;

          //_map = transform.GetChild(0).GetComponent<Map>();
          //_player = transform.GetChild(1).GetChild(0).GetComponent<Player>();
          //_player.color = Colors[0];
      }

      void Update() {
        if (_state == "created") {
          _state = "started";
          SceneManager.LoadSceneAsync("Game", LoadSceneMode.Single);
        }
      }

      void create(JSONNode msgObject) {
        foreach (JSONObject player in msgObject["data"]["players"])
        {
          Player newPlayer = new Player();
          newPlayer._id = player["_id"];
          this.players.Add(newPlayer);
        }
        this.seed = msgObject["data"]["seed"].AsInt;
        _state = "created";
      }

      public override void Receive(string service, JSONNode msgObject) {
        if (service == this.service) {
          switch ((string)msgObject["data"]["state"])
          {
            case "create":
              create(msgObject);
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