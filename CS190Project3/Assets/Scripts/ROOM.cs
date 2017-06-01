using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ROOM : MonoBehaviour {

    public DOOR UpDoor;
    public DOOR DownDoor;
    public DOOR RightDoor;
    public DOOR LeftDoor;

    public string coordinate;
    public int roomType;

    public GameObject floor;
    public List<Sprite> floors;
    public List<Sprite> wetFloors;

    public GameObject dripping;
    public GameObject ghosties;
	// Use this for initialization
	void Start () {
          if (roomType == 0)
          {
               floor.GetComponent<SpriteRenderer>().sprite = wetFloors[Random.Range(0, wetFloors.Capacity)];
               dripping.GetComponent<ParticleSystem>().Play();
          }
          else if (roomType == 3)
          {
               floor.GetComponent<SpriteRenderer>().sprite = floors[roomType];
               ghosties.GetComponent<ParticleSystem>().Play();
          }
          else
          {
               floor.GetComponent<SpriteRenderer>().sprite = floors[roomType];
          }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Standing()
    {
        GetComponent<_STOP_DEADEND>().SetDeadend();
        Debug.Log("I AM STANDING");
    }

    public void Enter()
    {
        GetComponent<_WALK>().Walk();
        Debug.Log("I AM WALKING");
    }

    public void Listen()
    {
        GetComponent<_CHECK_DEADEND>().SetDeadend();
        Debug.Log("I AM LISTENING");
    }
}
