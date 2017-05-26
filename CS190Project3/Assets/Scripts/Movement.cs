using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public ROOM currentRoom;
    public RoomGen RoomCoords;

    public Camera followme;
    public GameObject shroud;

    public GameObject monster;

    public float currentSpeed;

    string currentPosition;

    bool playedSound;

    int x = 0;
    int y = 0;

    float constant = 4.5f;

    string direction;

    // Use this for initialization
    void Start () {
        playedSound = false;
        currentSpeed = 0.1f;
        currentPosition = currentRoom.coordinate;

        Int32.TryParse(currentPosition[0].ToString(), out x);
        Int32.TryParse(currentPosition[1].ToString(), out y);

        transform.position = new Vector3(x * constant, y * constant, 0);
	}
	
	// Update is called once per frame
	void Update () {

        if (!playedSound)
            CheckSpace();

        float step = 2 * Time.deltaTime;

        float playerStep = currentSpeed * Time.deltaTime;


            if (Input.GetKeyDown("up"))
            {
                direction = "up";
                if (!RoomCoords.GetComponent<GlobalTimer>().outside)
                {
                    playedSound = false;
                }
            }
            if (Input.GetKeyDown("down"))
            {
                direction = "down";
                if (!RoomCoords.GetComponent<GlobalTimer>().outside)
                {
                    playedSound = false;
                }
            }
            if (Input.GetKeyDown("right"))
            {
                direction = "right";
                if (!RoomCoords.GetComponent<GlobalTimer>().outside)
                {
                    playedSound = false;
                }
            }
            if (Input.GetKeyDown("left"))
            {
                direction = "left";
                if (!RoomCoords.GetComponent<GlobalTimer>().outside)
                {
                    playedSound = false;
                }
            }
            if (!Input.GetKey("up") && !Input.GetKey("down") && !Input.GetKey("left") && !Input.GetKey("right"))
            {
                direction = "center";
                //playedSound = false;
            }

        switch (direction)
        {
            case "up":
                transform.position = Vector3.MoveTowards(transform.position, currentRoom.UpDoor.spot.transform.position, currentSpeed);
                //transform.position = currentRoom.UpDoor.spot.transform.position;
                break;
            case "down":
                transform.position = Vector3.MoveTowards(transform.position, currentRoom.DownDoor.spot.transform.position, currentSpeed);
                //transform.position = currentRoom.DownDoor.spot.transform.position;
                break;
            case "right":
                transform.position = Vector3.MoveTowards(transform.position, currentRoom.RightDoor.spot.transform.position, currentSpeed);
                //transform.position = currentRoom.RightDoor.spot.transform.position;
                break;
            case "left":
                transform.position = Vector3.MoveTowards(transform.position, currentRoom.LeftDoor.spot.transform.position, currentSpeed);
                //transform.position = currentRoom.LeftDoor.spot.transform.position;
                break;
             case "center":
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(currentRoom.transform.position.x, currentRoom.transform.position.y, 0), currentSpeed);
                //transform.position = new Vector3(currentRoom.transform.position.x, currentRoom.transform.position.y, 0);
                break;
            default:
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(currentRoom.transform.position.x, currentRoom.transform.position.y, 0), currentSpeed);
                //transform.position = new Vector3(currentRoom.transform.position.x, currentRoom.transform.position.y, 0);
                break;
        }

        float x = currentRoom.transform.position.x;
        float y = currentRoom.transform.position.y;

        followme.transform.position = new Vector3(x, y, -10);
        shroud.transform.position = new Vector3(x, y, -5);

        //followme.transform.position = Vector3.MoveTowards(followme.transform.position, new Vector3(x, y, -10), step);
        //shroud.transform.position = Vector3.MoveTowards(shroud.transform.position, new Vector3(x, y, -5), step);

        if(currentRoom.coordinate == RoomCoords.exit.coordinate)
            AkSoundEngine.SetRTPCValue("IsExit", 2);
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
            direction = "center";
            GetComponent<_WALK>().Walk();
            
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
        }

        int monsterX = 0;
        int monsterY = 0;

        string monsterPosition = monster.GetComponent<MonsterMovement>().currentRoom.coordinate;

        Int32.TryParse(monsterPosition[0].ToString(), out monsterX);
        Int32.TryParse(monsterPosition[1].ToString(), out monsterY);

        float distanceToMonster = (float)Math.Sqrt((x - monsterX) ^ 2 + (y - monsterY) ^ 2);

        AkSoundEngine.SetRTPCValue("Monster_Coming", distanceToMonster);

        if (distanceToMonster == 0)
            Debug.Log("YOU DIED");
    }

    void CheckSpace()
    {
        GetComponent<_STOP_DEADEND>().SetDeadend();
        GetComponent<_IS_NEAR_EXIT>().TheLight();

        int x = 0;
        int y = 0;

        currentPosition = currentRoom.coordinate;

        Int32.TryParse(currentPosition[0].ToString(), out x);
        Int32.TryParse(currentPosition[1].ToString(), out y);

        switch (direction)
        {
            case "up":
                y += 1;
                break;
            case "down":
                y -= 1;
                break;
            case "right":
                x += 1;
                break;
            case "left":
                x -= 1;
                break;
            default:
                break;
        }

        string tryCoordinate = x.ToString() + y.ToString();

        if (!RoomCoords.GetComponent<GlobalTimer>().outside)
        {
            if (RoomCoords.coordinates.ContainsKey(tryCoordinate))
            {
                GetComponent<_STOP_DEADEND>().SetDeadend();
            }
            else
            {
                GetComponent<_CHECK_DEADEND>().SetDeadend();
            }
        }
        else
        {
            GetComponent<_STOP_DEADEND>().SetDeadend();
        }

        if (RoomCoords.exit.coordinate == tryCoordinate)
        {
            AkSoundEngine.SetRTPCValue("Outside_Listen", 3);
        }
        else
        {
            AkSoundEngine.SetRTPCValue("Outside_Listen", 0);
        }

        playedSound = true;
    }
}
