using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public ROOM currentRoom;
    public RoomGen RoomCoords;

    public Camera followme;
    public GameObject shroud;

    string currentPosition;

    int x = 0;
    int y = 0;

    float constant = 4.5f;

    string direction;

    // Use this for initialization
    void Start () {
        currentPosition = currentRoom.coordinate;

        Int32.TryParse(currentPosition[0].ToString(), out x);
        Int32.TryParse(currentPosition[1].ToString(), out y);

        transform.position = new Vector2(x * constant, y * constant);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("up"))
        {
            direction = "up";
            transform.position = currentRoom.UpDoor.spot.transform.position;
        }
        if (Input.GetKeyDown("down"))
        {
            direction = "down";
            transform.position = currentRoom.DownDoor.spot.transform.position;
        }
        if (Input.GetKeyDown("right"))
        {
            direction = "right";
            transform.position = currentRoom.RightDoor.spot.transform.position;
        }
        if (Input.GetKeyDown("left"))
        {
            direction = "left";
            transform.position = currentRoom.LeftDoor.spot.transform.position;
        }

        float x = currentRoom.transform.position.x;
        float y = currentRoom.transform.position.y;

        followme.transform.position = new Vector3(x, y, -10);
        shroud.transform.position = new Vector3(x, y, 0);
    }

    public void SwitchMove()
    {
        // Debug to check movement and direction
        Debug.Log("I AM MOVING " + direction);

        int x = 0;
        int y = 0;

        currentPosition = currentRoom.coordinate;

        Int32.TryParse(currentPosition[0].ToString(), out x);
        Int32.TryParse(currentPosition[1].ToString(), out y);

        string opposite;

        switch(direction)
        {
            case "up":
                y += 1;
                opposite = "down";
                break;
            case "down":
                y -= 1;
                opposite = "up";
                break;
            case "right":
                x += 1;
                opposite = "left";
                break;
            case "left":
                x -= 1;
                opposite = "right";
                break;
            default:
                opposite = "up";
                break;
        }

        string tryCoordinate = x.ToString() + y.ToString();

        if(RoomCoords.coordinates.ContainsKey(tryCoordinate))
        {
                currentRoom = RoomCoords.coordinates[tryCoordinate];
                direction = opposite;
                switch (direction)
                {
                    case "up":
                        transform.position = currentRoom.UpDoor.spot.transform.position;
                        break;
                    case "down":
                        transform.position = currentRoom.DownDoor.spot.transform.position;
                        break;
                    case "right":
                        transform.position = currentRoom.RightDoor.spot.transform.position;
                        break;
                    case "left":
                        transform.position = currentRoom.LeftDoor.spot.transform.position;
                        break;
                    default:
                        transform.position = currentRoom.UpDoor.spot.transform.position;
                        break;
                }
        }
        else
        {
            // Dead end debug
            Debug.Log("TRY AGAIN SOMEWHERE ELSE");
        }
        
    }
}
