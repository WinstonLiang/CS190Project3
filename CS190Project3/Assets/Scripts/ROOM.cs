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

	// Use this for initialization
	void Start () {
          floor.GetComponent<SpriteRenderer>().sprite = floors[Random.Range(0, floors.Count)];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
