using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using CrazyGoat.Variables;
using CrazyGoat.Events;

public class GameState : GenericSingletonClass<MonoBehaviour>
{
    public StringReference connectionStatus;

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

    public void onGameCreated() {
      SceneManager.LoadScene("Game");
    }
}
