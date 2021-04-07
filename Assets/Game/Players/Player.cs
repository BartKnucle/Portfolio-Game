using UnityEngine;
using CrazyGoat.Variables;
using CrazyGoat.Colors;
using CrazyGoat.Network.Variables;

public class Player : MonoBehaviour
{
    public StringList players;
    public StringVariable UserId;
    public NetIntVariable score;
    public int index = 0;
    public bool isMe = false;
    public ColorList colors;
    public int speed = 4;

    public Color color;

    public Vector2 gridPosition;

    void Awake() {
      score = ScriptableObject.CreateInstance<NetIntVariable>();
      score.Value = 0;
    }

    void Start() {
        for (int i = 0; i < players.Value.Count; i++)
        {
            if (name == players.Value[i]) {
              this.index = i;
            }
        }

        if (name == UserId.Value) {
          isMe = true;
        } else {
          transform.GetChild(2).gameObject.SetActive(false);
        }

        color = colors.Value[index];

        reset();
    }
    void Update() {
        gridPosition.x = (int)Mathf.Round(transform.localPosition.x);
        gridPosition.y = (int)Mathf.Round(transform.localPosition.z);
        // Rotate
        if (Input.GetKey("left")) {
            //transform.Rotate(new Vector3(0, -Time.deltaTime * speed * 50, 0));
            move("left");
        }
        if (Input.GetKey("right")) {
            //transform.Rotate(new Vector3(0, Time.deltaTime * speed * 50, 0));
            move("right");
        }

        if (transform.localPosition.x < 0 || transform.localPosition.x > Map.instance.sizeX || transform.localPosition.z < 0 || transform.localPosition.z > Map.instance.sizeZ) {
            reset();
        }
    }

    void FixedUpdate() {
        // Supress the physics
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        //transform.rotation = Quaternion.Euler(new Vector3(0,transform.localRotation.y,0));

        // Go forward / backward
        if (Input.GetKey("up")) {
            move("up");
        }
        if (Input.GetKey("down")) {
            move("down");
        }
    }

    public void  move(string key) {
        if (isMe) {
          switch (key)
          {
              case "up":
                  GetComponent<Rigidbody>().MovePosition(
                      transform.position + transform.forward * speed * Time.deltaTime
                  );
                  break;
              case "down":
                  //GetComponent<Rigidbody>().velocity = -transform.forward * speed * 30 * Time.deltaTime;
                  GetComponent<Rigidbody>().MovePosition(
                      transform.position - transform.forward * speed * Time.deltaTime
                  );
                  break;
              case "left":
                  //GetComponent<Rigidbody>().AddTorque(-Vector3.up * Time.deltaTime * speed * 30);
                  transform.Rotate(new Vector3(0, -Time.deltaTime * speed * 40, 0));
                  break;
              case "right":
                  //GetComponent<Rigidbody>().AddTorque(Vector3.up * Time.deltaTime * speed * 30);
                  transform.Rotate(new Vector3(0, Time.deltaTime * speed * 40, 0));
                  break;
          }

          gridPosition.x = (int)Mathf.Round(transform.localPosition.x);
          gridPosition.y = (int)Mathf.Round(transform.localPosition.z);
        }
    }

    public void addScore(int points) {
      score.Value += points;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Floor")  {
            other.GetComponent<Brick>().setOwnerShip(this);   
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Floor")  {
            other.GetComponent<Brick>().setBarriers();
        }
    }

    /*public void sendId() {
        // Notify the server to replace the old id by the new one;
        Network.instance.send("api/player/connect/" + id);
    }*/

    // Is the player ower of the brick
    public float isMine(int x, int z) {
        if (Map.instance.getItem(x, z).GetComponent<Brick>().owner == transform.GetComponent<Player>()) {
            return 1;
        } else {
            return 0;
        }
    }

    public void reset() {
        //Map map = transform.root.GetChild(0).GetComponent<Map>();
        switch (index)
        {
            case 0:
                //mat = Resources.Load<Material>("Materials/One");
                transform.localPosition = new Vector3(1, 0, 1);
                transform.localRotation = Quaternion.Euler(Vector3.zero);
                break;
            case 1:
                //mat = Resources.Load<Material>("Materials/Two");
                transform.localPosition = new Vector3(Map.instance.sizeX - 2, 0, 1);
                transform.localRotation = Quaternion.Euler(-Vector3.up * 90);
                break;
            case 2:
                //mat = Resources.Load<Material>("Materials/Tree");
                transform.localPosition = new Vector3(1, 0, Map.instance.sizeZ -2);
                transform.localRotation = Quaternion.Euler(Vector3.up * 90);
                break;
            case 3:
                //mat = Resources.Load<Material>("Materials/Four");
                transform.localPosition = new Vector3(Map.instance.sizeX -2, 0, Map.instance.sizeZ -2);
                transform.localRotation = Quaternion.Euler(-Vector3.up * 180);
                break;
            
        }
        
        transform.GetChild(1).GetChild(0).GetComponent<SkinnedMeshRenderer>().material.SetColor("playerColor", color);
        transform.GetChild(1).GetChild(2).GetComponent<SkinnedMeshRenderer>().material.SetColor("playerColor", color);
        transform.GetChild(1).GetChild(12).GetComponent<SkinnedMeshRenderer>().material.SetColor("playerColor", color);
        transform.GetChild(1).GetChild(13).GetComponent<SkinnedMeshRenderer>().material.SetColor("playerColor", color);
        transform.GetChild(1).GetChild(14).GetComponent<SkinnedMeshRenderer>().material.SetColor("playerColor", color);
        transform.GetChild(1).GetChild(16).GetComponent<SkinnedMeshRenderer>().material.SetColor("playerColor", color);
        transform.GetChild(1).GetChild(17).GetComponent<SkinnedMeshRenderer>().material.SetColor("playerColor", color);
        transform.GetChild(1).GetChild(18).GetComponent<SkinnedMeshRenderer>().material.SetColor("playerColor", color);
        transform.GetChild(1).GetChild(22).GetComponent<SkinnedMeshRenderer>().material.SetColor("playerColor", color);
        transform.GetChild(1).GetChild(23).GetComponent<SkinnedMeshRenderer>().material.SetColor("playerColor", color);
        transform.GetChild(1).GetChild(24).GetComponent<SkinnedMeshRenderer>().material.SetColor("playerColor", color);
        transform.GetChild(1).GetChild(25).GetComponent<SkinnedMeshRenderer>().material.SetColor("playerColor", color);
        
        //aiScore = 0;
        //aicollisions = 0;
        //_checkDestinations();
    }
}

