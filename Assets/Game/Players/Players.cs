using UnityEngine;
using CrazyGoat.Variables;

public class Players : MonoBehaviour
{
    public StringList players;

    public GameObject playerPrefab;

    public void Awake() {
      for (int i = 0; i < players.Value.Count; i++)
      {
          GameObject player = GameObject.Instantiate(playerPrefab, _getPlayerPosition(i), _getPlayerRotation(i), transform);
          player.name = players.Value[i];
      }
    }

    private Vector3 _getPlayerPosition (int index) {
      switch (index)
        {
            case 0:
                return new Vector3(1, 0, 1);
            case 1:
                return new Vector3(Map.instance.sizeX - 2, 0, 1);
            case 2:
                return new Vector3(1, 0, Map.instance.sizeZ -2);
            case 3:
                return new Vector3(Map.instance.sizeX -2, 0, Map.instance.sizeZ -2);
        }
      return new Vector3(0, 0, 0);
    }

    private Quaternion _getPlayerRotation (int index) {
      switch (index)
      {
        case 0:
            return Quaternion.Euler(Vector3.zero);
        case 1:
            return Quaternion.Euler(-Vector3.up * 90);
        case 2:
            return Quaternion.Euler(Vector3.up * 90);
        case 3:
            return Quaternion.Euler(-Vector3.up * 180);
      }

      return Quaternion.Euler(Vector3.zero);
    }
}
