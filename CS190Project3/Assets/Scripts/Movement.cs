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

    public List<Sprite> gameFrames;

    float fan = 0;
    bool anim = false;

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

        // transform.position = new Vector3(x * constant, y * constant, 0);
	}
	
	// Update is called once per frame
	void Update () {

        if (!playedSound)
        {
            CheckSpace();
            currentRoom.Standing();
        }

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
        if (currentRoom.roomType != 2)
        {
             shroud.GetComponent<SpriteRenderer>().sprite = gameFrames[currentRoom.roomType];
        }
        else
        {
             if (fan <= Time.time)
             {
                  if (!anim)
                  {
                       shroud.GetComponent<SpriteRenderer>().sprite = gameFrames[2];
                       anim = true;
                  }
                  else
                  {
                       shroud.GetComponent<SpriteRenderer>().sprite = gameFrames[5];
                       anim = false;
                  }
                  fan = Time.time + 0.05f;
             }
        }

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

        switch(direction)
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

        if (RoomCoords.coordinates.ContainsKey(tryCoordinate))
        {
            currentRoom = RoomCoords.coordinates[tryCoordinate];
            direction = "center";
            currentRoom.Enter();
        }
        else
        {
            // Dead end debug
            Debug.Log("TRY AGAIN SOMEWHERE ELSE");
        }

        float monsterDistance = Vector3.Distance(transform.position / constant, monster.transform.position / constant) * 2;
        // So the monster looks for the approximate change in position between the player and itself.
        // Reducing said change by the constant 4.5 that all rooms are in size.
        // Multiplied by 2 to get a more apparent "closeness" to the player.

        //Debug.Log(distanceToMonster);
        Debug.Log(monsterDistance);

        AkSoundEngine.SetRTPCValue("Monster_Coming", monsterDistance);

        if (monsterDistance <= 1)
            Debug.Log("YOU DIED");
    }

    void CheckSpace()
    {

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
                RoomCoords.coordinates[tryCoordinate].Listen();
            }
            else
            {
                GetComponent<_CHECK_DEADEND>().SetDeadend();
            }
        }
        else
        {
            AkSoundEngine.SetRTPCValue("Outside_Listen", 3);
            GetComponent<_OUTSIDE>().TheBirds();
        }

        if (RoomCoords.exit.coordinate == tryCoordinate)
        {
            GetComponent<_OUTSIDE>().TheBirds();
        }
        else
        {
            int exitX = 0;
            int exitY = 0;

            Int32.TryParse(RoomCoords.exit.coordinate[0].ToString(), out exitX);
            Int32.TryParse(RoomCoords.exit.coordinate[1].ToString(), out exitY);

            float distanceToExit = (float)Math.Sqrt((x - exitX) ^ 2 + (y - exitY) ^ 2);

            if(Math.Floor(distanceToExit) > 3)
                GetComponent<_WALK>().Walk();
            else
                GetComponent<_IS_NEAR_EXIT>().TheLight();

            AkSoundEngine.SetRTPCValue("Outside_Listen", 0);
        }

        playedSound = true;
    }
}
