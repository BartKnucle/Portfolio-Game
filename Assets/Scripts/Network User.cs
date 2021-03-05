using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkUser : MonoBehaviour
{
    public static NetworkUser instance;
    void Awake()
    {
      if (instance) {
        Destroy(gameObject);
      } else {
        instance = this;
      }
    }
}
