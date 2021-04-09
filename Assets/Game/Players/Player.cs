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
    public NetFloatVariable yRotation;
    private int netPositionInterval = 100;
    private float lastPositionSend = 0;
    public int index = 0;
    public bool isMe = false;
    public ColorList colors;
    public int speed = 4;
    public Color color;

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

        zPosition = ScriptableObject.CreateInstance<NetFloatVariable>();
        zPosition.service = service;
        zPosition.DatabaseFieldName = "z";

        yRotation = ScriptableObject.CreateInstance<NetFloatVariable>();
        yRotation.service = service;
        yRotation.DatabaseFieldName = "yRotation";

        if (name == UserId.Value) {
          isMe = true;
          score.autoSend = true;
          xPosition.autoSend = true;
          zPosition.autoSend = true;
          yRotation.autoSend = true;
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
          updateRequest.floatVariables.Add(yRotation);

          // Add the service to the network manager
          Manager.Instance.services.Add(service);

          //Deactivate camera
          transform.GetChild(2).gameObject.SetActive(false);
        }

        color = colors.Value[index];
        setColor();
    }
    void Update() {

        if (isMe) {
          // Rotate
          if (Input.GetKey("left")) {
              move("left");
          }
          if (Input.GetKey("right")) {
              move("right");
          }
        }

        lastPositionSend += Time.deltaTime;
    }

    void FixedUpdate() {
        // Supress the physics
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        if (isMe) {
          // Go forward / backward
          if (Input.GetKey("up")) {
              move("up");
          }
          if (Input.GetKey("down")) {
              move("down");
          } 
        }
    }

    public void setNetworkPosition(){
      transform.localPosition = new Vector3(xPosition.Value, transform.position.y, zPosition.Value);
      transform.eulerAngles = new Vector3(
        transform.eulerAngles.x,
        yRotation.Value,
        transform.eulerAngles.z
      );
    }

    public void move(string key) {
        if (isMe) {
          switch (key)
          {
              case "up":
                  GetComponent<Rigidbody>().MovePosition(
                      transform.position + transform.forward * speed * Time.deltaTime
                  );
                  break;
              case "down":
                  GetComponent<Rigidbody>().MovePosition(
                      transform.position - transform.forward * speed * Time.deltaTime
                  );
                  break;
              case "left":
                  transform.Rotate(new Vector3(0, -Time.deltaTime * speed * 40, 0));
                  break;
              case "right":
                  transform.Rotate(new Vector3(0, Time.deltaTime * speed * 40, 0));
                  break;
          }

          // Delay between two position sending
          if (lastPositionSend > netPositionInterval / 1000) {
            xPosition.Value = transform.localPosition.x;
            zPosition.Value = transform.localPosition.z;
            yRotation.Value = transform.rotation.eulerAngles.y;
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

    public void setColor() {  
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
    }
}

