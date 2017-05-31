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

    public GameObject dripping;
	// Use this for initialization
	void Start () {
          floor.GetComponent<SpriteRenderer>().sprite = floors[roomType];
          switch (roomType)
          {
               case 0:
                    dripping.GetComponent<ParticleSystem>().Play();
                    break;
          }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Standing()
    {
        GetComponent<_STOP_DEADEND>().SetDeadend();
    }

    public void Enter()
    {
        GetComponent<_WALK>().Walk();
    }

    public void Listen()
    {
        GetComponent<_CHECK_DEADEND>().SetDeadend();
    }
}
