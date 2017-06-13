using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public ROOM currentRoom;
    public RoomGen RoomCoords;
    public GlobalTimer world;

    public Camera followme;
    public GameObject shroud;

    public GameObject monster, dead_text, live_text;

    public float currentSpeed;

    public List<Sprite> gameFrames;
	
	public bool dead, outside;

     public GameObject winParticles;

    float fan = 0;
    bool anim = false;

    string currentPosition;

    bool playedSound;
	bool playOnce;

    public int x = 0;
    public int y = 0;

    float constant = 4.5f;
    float timer = 0;

    string direction;

    bool fail = false;
    float movingTimer = 1f;

    bool died;
    float deadTimer;

    bool replay;

    // Use this for initialization
    void Start () {
         replay = false;
         died = false;
         deadTimer = 0;
        playedSound = false;
		playOnce = true;
        currentSpeed = 0.1f;
        currentPosition = currentRoom.coordinate;

        Int32.TryParse(currentPosition[0].ToString(), out x);
        Int32.TryParse(currentPosition[1].ToString(), out y);

        timer = 0;

        // transform.position = new Vector3(x * constant, y * constant, 0);
	}
	
	// Update is called once per frame
	void Update () {
          if (fail && movingTimer < 1f)
          {
               movingTimer += Time.deltaTime;
               if (movingTimer < 0.5f)
               {
                    Debug.Log("towards_door");
                    switch (direction)
                    {
                         case "up":
                              transform.position = Vector3.MoveTowards(transform.position, currentRoom.UpDoor.transform.position, currentSpeed);
                              break;
                         case "down":
                              transform.position = Vector3.MoveTowards(transform.position, currentRoom.DownDoor.transform.position, currentSpeed);
                              break;
                         case "right":
                              transform.position = Vector3.MoveTowards(transform.position, currentRoom.RightDoor.transform.position, currentSpeed);
                              break;
                         case "left":
                              transform.position = Vector3.MoveTowards(transform.position, currentRoom.LeftDoor.transform.position, currentSpeed);
                              break;
                    }
               }
               else
               {
                    Debug.Log("away_door");
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
                    }
               }
          }
          else
          {
               fail = false;
          }

        if (!playedSound && timer >= 1)
        {
            CheckSpace();
            // currentRoom.Standing();
        }
        if (replay)
        {
             if (Input.GetKeyDown("r"))
             {
                  UnityEngine.SceneManagement.SceneManager.LoadScene("map1");
             }
        }
        if (!fail && !dead && !outside)
        {
             if (Input.GetKey("up"))
             {
                  direction = "up";
                  if (!RoomCoords.GetComponent<GlobalTimer>().outside)
                  {
                       playedSound = false;
                  }
             }
             if (Input.GetKey("down"))
             {
                  direction = "down";
                  if (!RoomCoords.GetComponent<GlobalTimer>().outside)
                  {
                       playedSound = false;
                  }
             }
             if (Input.GetKey("right"))
             {
                  direction = "right";
                  if (!RoomCoords.GetComponent<GlobalTimer>().outside)
                  {
                       playedSound = false;
                  }
             }
             if (Input.GetKey("left"))
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
                  playedSound = false;
             }
        }
        if (!fail)
        {
             switch (direction)
             {
                  case "up":
                       transform.position = Vector3.MoveTowards(transform.position, currentRoom.UpDoor.spot.transform.position, currentSpeed);
                       transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, 0f, 0f), 1000f * Time.deltaTime);
                       break;
                  case "down":
                       transform.position = Vector3.MoveTowards(transform.position, currentRoom.DownDoor.spot.transform.position, currentSpeed);
                       transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, 0f, 180f), 1000f * Time.deltaTime);
                       break;
                  case "right":
                       transform.position = Vector3.MoveTowards(transform.position, currentRoom.RightDoor.spot.transform.position, currentSpeed);
                       transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, 0f, -90f), 1000f * Time.deltaTime);
                       break;
                  case "left":
                       transform.position = Vector3.MoveTowards(transform.position, currentRoom.LeftDoor.spot.transform.position, currentSpeed);
                       transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, 0f, 90f), 1000f * Time.deltaTime);
                       break;
                  case "center":
                       transform.position = Vector3.MoveTowards(transform.position, new Vector3(currentRoom.transform.position.x, currentRoom.transform.position.y, 0), currentSpeed);
                       break;
                  default:
                       transform.position = Vector3.MoveTowards(transform.position, new Vector3(currentRoom.transform.position.x, currentRoom.transform.position.y, 0), currentSpeed);
                       break;
             }
        }

        float myX = currentRoom.transform.position.x;
        float myY = currentRoom.transform.position.y;

        followme.transform.position = new Vector3(myX, myY, -10);
        shroud.transform.position = new Vector3(myX, myY, -5);
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

        timer += Time.deltaTime;

        if (dead)
        {
             //dead_text.SetActive(true);
             deadTimer += Time.deltaTime;
             if (deadTimer >= 8f)
             {
                  replay = true;
                  shroud.GetComponent<SpriteRenderer>().sortingOrder = 5;
                  shroud.GetComponent<SpriteRenderer>().sprite = gameFrames[6];
             }
             else
             {
                  shroud.GetComponent<SpriteRenderer>().sortingOrder = 3;
             }
             if(!died)
               shroud.GetComponent<ParticleSystem>().Play();
             died = true;
        }
        if (outside)
		{
			winParticles.SetActive(true);
			if(playOnce)
			{
				playOnce = false;
				AkSoundEngine.SetRTPCValue("Outside_Listen", 3);
				GetComponent<_OUTSIDE>().TheBirds();
			}
		}
			//live_text.SetActive(true);

        currentPosition = currentRoom.coordinate;

        Int32.TryParse(currentPosition[0].ToString(), out x);
        Int32.TryParse(currentPosition[1].ToString(), out y);
    }

    public void SwitchMove()
    {
         fail = true;
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
            currentRoom.Standing();
        }
        else
        {
            // Dead end debug
            Debug.Log("TRY AGAIN SOMEWHERE ELSE");
            fail = true;
            movingTimer = 0f;
        }

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
            if (!RoomCoords.coordinates.ContainsKey(tryCoordinate))
            {
                Debug.Log("THIS IS DEAD END");
                GetComponent<_CHECK_DEADEND>().SetDeadend();
            }
            else
            {
                if (RoomCoords.coordinates[tryCoordinate] == RoomCoords.exit)
                {
                    AkSoundEngine.SetRTPCValue("Outside_Listen", 3);
                    GetComponent<_OUTSIDE>().TheBirds();
                    RoomCoords.coordinates[tryCoordinate].Listen();
                }
                else if (RoomCoords.coordinates[tryCoordinate] != currentRoom)
                {
                    RoomCoords.coordinates[tryCoordinate].Listen();
                }
                else if (RoomCoords.coordinates[tryCoordinate] == currentRoom)
                {
                    currentRoom.Standing();
                    GetComponent<_WALK>().Walk();
                }

                if (world.threatened.Contains(RoomCoords.coordinates[tryCoordinate]))
                {
                    if (world.threatened[0] == RoomCoords.coordinates[tryCoordinate])
                    {
                        //Debug.Log("X:" + x);
                        //Debug.Log("Y:" + y);
                        //Debug.Log("MONSTER NEXT DOOR");
                        AkSoundEngine.SetRTPCValue("Monster_Coming", 3);
                    }
                    else
                    {
                        if (monster.GetComponent<MonsterMovement>().toMove == 0)
                            AkSoundEngine.SetRTPCValue("Monster_Coming", 1);
                        else
                            AkSoundEngine.SetRTPCValue("Monster_Coming", 2);
                    }
                    GetComponent<MONSTER_CLOSER>().Step();
                }
                else
                {
                    //Debug.Log("BYE");
                    GetComponent<MONSTER_TOO_FAR>().Bye();
                }
            }
        }
        else if (!outside)
        {
            AkSoundEngine.SetRTPCValue("Outside_Listen", 3);
            GetComponent<_OUTSIDE>().TheBirds();
			outside = true;
        }

        // if (RoomCoords.exit.coordinate == tryCoordinate && !outside)
        // {
            // GetComponent<_OUTSIDE>().TheBirds();
			// outside = true;
        // }
        // else
        // {
            // int exitX = 0;
            // int exitY = 0;

            // Int32.TryParse(RoomCoords.exit.coordinate[0].ToString(), out exitX);
            // Int32.TryParse(RoomCoords.exit.coordinate[1].ToString(), out exitY);

            // float distanceToExit = (float)Math.Sqrt((x - exitX) ^ 2 + (y - exitY) ^ 2);

            // if(Math.Floor(distanceToExit) > 3)
                // GetComponent<_WALK>().Walk();
                // //GetComponent<_IS_NEAR_EXIT>().TheLight();

            // AkSoundEngine.SetRTPCValue("Outside_Listen", 0);
        // }

        playedSound = true;
		timer = 0;
    }
}
