using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public ROOM currentRoom;
    public RoomGen RoomCoords;

    public Camera followme;
    public GameObject shroud;

    public float currentSpeed;

    string currentPosition;

    int x = 0;
    int y = 0;

    float constant = 4.5f;

    string direction;

    // Use this for initialization
    void Start () {
        currentSpeed = 2;
        currentPosition = currentRoom.coordinate;

        Int32.TryParse(currentPosition[0].ToString(), out x);
        Int32.TryParse(currentPosition[1].ToString(), out y);

        transform.position = new Vector3(x * constant, y * constant, 0);
	}
	
	// Update is called once per frame
	void Update () {

        float step = 2 * Time.deltaTime;

        float playerStep = currentSpeed * Time.deltaTime;

        if (Input.GetKeyDown("up"))
        {
            direction = "up";
        }
        if (Input.GetKeyDown("down"))
        {
            direction = "down";
        }
        if (Input.GetKeyDown("right"))
        {
            direction = "right";
        }
        if (Input.GetKeyDown("left"))
        {
            direction = "left";
        }

        switch (direction)
        {
            case "up":
                transform.position = Vector3.MoveTowards(transform.position, currentRoom.UpDoor.spot.transform.position, currentSpeed);
                break;
            case "down":
                transform.position = Vector3.MoveTowards(transform.position, currentRoom.DownDoor.spot.transform.position, currentSpeed);
                break;
            case "right":
                transform.position = Vector3.MoveTowards(transform.position, currentRoom.RightDoor.spot.transform.position, currentSpeed);
                break;
            case "left":
                transform.position = Vector3.MoveTowards(transform.position, currentRoom.LeftDoor.spot.transform.position, currentSpeed);
                break;
            default:
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(currentRoom.transform.position.x, currentRoom.transform.position.y, 0), currentSpeed);
                break;
        }

        float x = currentRoom.transform.position.x;
        float y = currentRoom.transform.position.y;

        followme.transform.position = Vector3.MoveTowards(followme.transform.position, new Vector3(x, y, -10), step);
        shroud.transform.position = Vector3.MoveTowards(shroud.transform.position, new Vector3(x, y, -5), step);
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
                opposite = "center";
                break;
        }

        string tryCoordinate = x.ToString() + y.ToString();

        if(RoomCoords.coordinates.ContainsKey(tryCoordinate))
        {
            currentRoom = RoomCoords.coordinates[tryCoordinate];
            direction = opposite;
            this.GetComponent<_WALK>().Walk();

            AkSoundEngine.SetRTPCValue("Distance", 0);
            //switch (direction)
            //{
            //    case "up":
            //        transform.position = Vector3.MoveTowards(transform.position, currentRoom.UpDoor.spot.transform.position, currentSpeed);
            //        break;
            //    case "down":
            //        transform.position = Vector3.MoveTowards(transform.position, currentRoom.DownDoor.spot.transform.position, currentSpeed);
            //        break;
            //    case "right":
            //        transform.position = Vector3.MoveTowards(transform.position, currentRoom.RightDoor.spot.transform.position, currentSpeed);
            //        break;
            //    case "left":
            //        transform.position = Vector3.MoveTowards(transform.position, currentRoom.LeftDoor.spot.transform.position, currentSpeed);
            //        break;
            //    default:
            //        transform.position = Vector3.MoveTowards(transform.position, currentRoom.transform.position, currentSpeed);
            //        break;
            //}
        }
        else
        {
            // Dead end debug
            Debug.Log("TRY AGAIN SOMEWHERE ELSE");
            AkSoundEngine.SetRTPCValue("Distance", 10);
            this.GetComponent<_CHECK_DEADEND>().SetDeadend();
        }
        
    }
}
