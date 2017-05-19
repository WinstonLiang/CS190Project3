using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour {

    public ROOM currentRoom;
    public RoomGen RoomCoords;

    public float currentSpeed;

    string currentPosition;

    bool playedSound;

    int x = 0;
    int y = 0;

    float constant = 4.5f;

    string direction;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (RoomCoords.GetComponent<GlobalTimer>().outside)
            GetComponent<_IS_NEAR_EXIT>().TheLight();
	}
}
