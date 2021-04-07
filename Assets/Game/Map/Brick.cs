using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    private Map _map;
    public Player owner;
    public int x;
    public int z;

    public void setBarriers() {
        for (int i = 0; i < 4; i++)
        {
            Barrier barriere = transform.GetChild(0).GetChild(i).GetComponent<Barrier>();
            barriere.check(i);
        }
    }

    public void setFloor() {
        if (tag != "Floor") {
            transform.localPosition = new Vector3(
                transform.localPosition.x,
                -1,
                transform.localPosition.z

            );

            //transform.root.GetComponent<Game>().maxScore += 1;
            tag = "Floor";
            gameObject.layer = 13;
        }
    }

    public void setWall() {
        if (tag != "Wall") {
            transform.localPosition = new Vector3(
                transform.localPosition.x,
                0,
                transform.localPosition.z
            );

            //transform.root.GetComponent<Game>().maxScore -= 1;
            tag = "Wall";
        }        
    }

    public void setOwnerShip(Player player) {
        if (tag != "Wall") {
            tag = "Floor";

            if (!owner) {
                if (owner) {
                    owner.addScore(-1);
                }

                owner = player;

                transform.GetComponent<MeshRenderer>().material.SetColor("playerColor", owner.color);
                owner.addScore(1);
                
                setBarriers();
            }
        }
    }

    public float isAvailable () {
        if (tag != "Wall") {
            return 1;
        } else {
            return 0;
        }
    }
}
