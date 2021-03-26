using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using CrazyGoat.Variables;
using CrazyGoat.Events;

public class GameState : MonoBehaviour
{
    public static GameState instance;
    public StringReference connectionStatus;

    GameState() {
        if (instance == null) {
            instance = this;
        }
    }

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
