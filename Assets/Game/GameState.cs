using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using CrazyGoat.Variables;
using CrazyGoat.Events;

public class GameState : GenericSingletonClass<MonoBehaviour>
{
    //public static GameState instance;
    public StringReference connectionStatus;

    /*GameState() {
      if (instance != null) {
        Destroy(gameObject);
      } else {
        instance = this;
        DontDestroyOnLoad(gameObject);
      }
    }*/

    public void onConnectionChanged() {
        switch (connectionStatus.Value)
        {
            case "Connected":
                SceneManager.LoadScene("Lobby");
                break;
            case "Disconnected":
                SceneManager.LoadScene("Intro");
                break;
            default:
                break;
        }
    }
}
