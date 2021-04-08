using UnityEngine;
using CrazyGoat.Events;
using CrazyGoat.Variables;
using CrazyGoat.Colors;
using CrazyGoat.Network;
using CrazyGoat.Network.Variables;
using UnityEngine.Events;


public class Player : MonoBehaviour
{
    public StringList players;
    public Service service;
    public Request updateRequest;
    public GameEvent onNetPlayerUpdate;
    public GameEventListener onNetPlayerUpdateListener;
    public StringVariable UserId;
    public StringVariable playerId;
    public NetIntVariable score;
    public NetFloatVariable xPosition;
    public NetFloatVariable zPosition;
    private int netPositionInterval = 1000;
    private float lastPositionSend = 0;
    public int index = 0;
    public bool isMe = false;
    public ColorList colors;
    public int speed = 4;
    public Color color;
    public Vector2 gridPosition;

    void Awake() {
      // Set objects names
      
    }

    void Start() {
        for (int i = 0; i < players.Value.Count; i++)
        {
            if (name == players.Value[i]) {
              this.index = i;
            }
        }

        playerId = ScriptableObject.CreateInstance<StringVariable>();
        playerId.Value = name;

        // Setting the service bound to the player
        service = ScriptableObject.CreateInstance<Service>();
        service._id = playerId;
        service.api = "/api/players";
        
        // The network variables bound to the request, wih auto update or not
        score = ScriptableObject.CreateInstance<NetIntVariable>();
        score.service = service;
        score.DatabaseFieldName = "score";
        score.Value = 0;
        

        xPosition = ScriptableObject.CreateInstance<NetFloatVariable>();
        xPosition.service = service;
        xPosition.DatabaseFieldName = "x";
        xPosition.Value = 0;

        zPosition = ScriptableObject.CreateInstance<NetFloatVariable>();
        zPosition.service = service;
        zPosition.DatabaseFieldName = "z";
        zPosition.Value = 0;

        if (name == UserId.Value) {
          isMe = true;
          score.autoSend = true;
          xPosition.autoSend = true;
          zPosition.autoSend = true;
        } else {
          updateRequest = ScriptableObject.CreateInstance<Request>();
          updateRequest.ServerRequestName = "update";
          updateRequest.service = service;
          service.requests.Add(updateRequest);
          // Setting the update for the network playes
          onNetPlayerUpdate = ScriptableObject.CreateInstance<GameEvent>();
          updateRequest.onReception = onNetPlayerUpdate;

          // Add the listener
          onNetPlayerUpdateListener = gameObject.AddComponent<GameEventListener>();
          onNetPlayerUpdateListener.Event = onNetPlayerUpdate;
          onNetPlayerUpdateListener.Response.AddListener(setNetworkPosition);
          onNetPlayerUpdate.RegisterListener(onNetPlayerUpdateListener);

          updateRequest.intVariables.Add(score);
          updateRequest.floatVariables.Add(xPosition);
          updateRequest.floatVariables.Add(zPosition);

          // Add the service to the network manager
          Manager.Instance.services.Add(service);

          //Deactivate camera
          transform.GetChild(2).gameObject.SetActive(false);
        }

        color = colors.Value[index];

        reset();
    }
    void Update() {
        gridPosition.x = (int)Mathf.Round(transform.localPosition.x);
        gridPosition.y = (int)Mathf.Round(transform.localPosition.z);

        if (isMe) {
          // Rotate
          if (Input.GetKey("left")) {
              //transform.Rotate(new Vector3(0, -Time.deltaTime * speed * 50, 0));
              move("left");
          }
          if (Input.GetKey("right")) {
              //transform.Rotate(new Vector3(0, Time.deltaTime * speed * 50, 0));
              move("right");
          }
          /*if (transform.localPosition.x < 0 || transform.localPosition.x > Map.instance.sizeX || transform.localPosition.z < 0 || transform.localPosition.z > Map.instance.sizeZ) {
              reset();
          }*/
        }

        lastPositionSend += Time.deltaTime;
    }

    public void setNetworkPosition(){
      transform.localPosition = new Vector3(xPosition.Value, transform.position.y, zPosition.Value);
    }

    void FixedUpdate() {
        if (isMe) {
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

          // Delay between two position sending
          if (lastPositionSend > netPositionInterval / 1000) {
            xPosition.Value = transform.localPosition.x;
            zPosition.Value = transform.localPosition.z;
            lastPositionSend = 0;
          }
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

    // Is the player ower of the brick
    /*public float isMine(int x, int z) {
        if (Map.instance.getItem(x, z).GetComponent<Brick>().owner == transform.GetComponent<Player>()) {
            return 1;
        } else {
            return 0;
        }
    }*/

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

