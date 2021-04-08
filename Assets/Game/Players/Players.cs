using UnityEngine;
using CrazyGoat.Variables;

public class Players : MonoBehaviour
{
    public StringList players;

    public GameObject playerPrefab;

    public void Awake() {
      foreach (string playerId in players.Value)
      {
        GameObject player = GameObject.Instantiate(playerPrefab, transform);
        player.name = playerId;
      }
    }
}
