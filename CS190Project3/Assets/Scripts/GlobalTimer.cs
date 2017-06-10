using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalTimer : MonoBehaviour {

    public float timer = 0;
    public List<GameObject> moving;
    public int second = 0;
    public GameObject Rooms;
    public List<ROOM> threatened;
    public bool outside;

    bool GONG;
    bool doorSound;
    bool moved = false;
    bool winPlayed;

    public GameObject Player, Monster;

	// Use this for initialization
	void Start () {
        timer = 0;
        GONG = false;
        second = 0;
        outside = false;
        doorSound = false;
        winPlayed = false;
	}

     void FixedUpdate()
     {
          if (timer >= 4.25f && !GONG)
          {
               foreach (ROOM r in Rooms.GetComponent<RoomGen>().rooms)
               {
                    r.UpDoor.GetComponent<Animator>().SetInteger("DoorState", 1);
                    r.LeftDoor.GetComponent<Animator>().SetInteger("DoorState", 1);
                    r.DownDoor.GetComponent<Animator>().SetInteger("DoorState", 1);
                    r.RightDoor.GetComponent<Animator>().SetInteger("DoorState", 1);
               }
          }
          if (timer >= 5f && !GONG)
          {
               GONG = true;
               Debug.Log("being_closing");
               foreach (ROOM r in Rooms.GetComponent<RoomGen>().rooms)
               {
                    r.UpDoor.GetComponent<Animator>().SetInteger("DoorState", -1);
                    r.LeftDoor.GetComponent<Animator>().SetInteger("DoorState", -1);
                    r.DownDoor.GetComponent<Animator>().SetInteger("DoorState", -1);
                    r.RightDoor.GetComponent<Animator>().SetInteger("DoorState", -1);
               }
          }
          if (timer >= 5.5f && GONG)
          {
               foreach (ROOM r in Rooms.GetComponent<RoomGen>().rooms)
               {
                    r.UpDoor.GetComponent<Animator>().SetInteger("DoorState", 0);
                    r.LeftDoor.GetComponent<Animator>().SetInteger("DoorState", 0);
                    r.DownDoor.GetComponent<Animator>().SetInteger("DoorState", 0);
                    r.RightDoor.GetComponent<Animator>().SetInteger("DoorState", 0);
               }
          }
     }


	// Update is called once per frame
	void Update () {
        if (outside && !winPlayed)
        {
            Debug.Log("Freedom.");
            AkSoundEngine.SetRTPCValue("Outside_Listen", 3);
            GetComponent<_OUTSIDE>().TheBirds();
            winPlayed = true;
        }

        if (!outside && !Player.GetComponent<Movement>().dead)
        {
            timer += Time.deltaTime;
            if (timer > second + 1)
            {
                second += 1;
                if (second % 2 == 0)
                {
                    GetComponent<_TICK_EVEN>().Tick();
                }
                else
                {
                    GetComponent<_TOCK_ODD>().Tick();
                }
            }
            if (timer >= 0f && !doorSound)
            {
                 GetComponent<_WALK>().Walk();
                 doorSound = true;
            }

            if (timer >= 5.25f && timer <= 6.5f && !moved)
            {
                // Signal for all to move Debug
                //Debug.Log("GONG");
                 moved = true;
                foreach (GameObject thing in moving)
                {

                    if (thing.name == "SamplePlayer") // TODO: Replace with actual name later
                    {
                        thing.GetComponent<Movement>().SwitchMove();
                        if (thing.GetComponent<Movement>().currentRoom == Rooms.GetComponent<RoomGen>().exit)
						{
							outside = true;
						}
                        Player = thing;
                    }
                    if (thing.name == "SampleMonster")
                    {
                        thing.GetComponent<MonsterMovement>().SwitchMove();
                        Monster = thing;
                    }
                }

                int playerX = Player.GetComponent<Movement>().x;
                int playerY = Player.GetComponent<Movement>().y;

                int monsterX = Monster.GetComponent<MonsterMovement>().x;
                int monsterY = Monster.GetComponent<MonsterMovement>().y;

                //Debug.Log(monsterX);
                //Debug.Log(monsterY);

                threatened = new List<ROOM>(5);

                threatened.Add(Monster.GetComponent<MonsterMovement>().currentRoom);

                for(int i = 1; i < 3; i++)
                {
                    float mult = Mathf.Pow(-1, i);
                    //Debug.Log("Mult:" + mult);
                    int x = monsterX + (int)mult;
                    int y = monsterY + (int)mult;

                    //Debug.Log("X:" + x);
                    //Debug.Log("Y:" + y);

                    string tryCoordinateUD = monsterX.ToString() + y.ToString();
                    string tryCoordinateLR = x.ToString() + monsterY.ToString();

                    if(Rooms.GetComponent<RoomGen>().coordinates.ContainsKey(tryCoordinateUD))
                        threatened.Add(Rooms.GetComponent<RoomGen>().coordinates[tryCoordinateUD]);
                    if(Rooms.GetComponent<RoomGen>().coordinates.ContainsKey(tryCoordinateLR))
                        threatened.Add(Rooms.GetComponent<RoomGen>().coordinates[tryCoordinateLR]);
                }
				
				bool sameRoom = Monster.GetComponent<MonsterMovement>().currentRoom == Player.GetComponent<Movement>().currentRoom;

                if (sameRoom && !Player.GetComponent<Movement>().dead)
				{
                    Debug.Log("YOU DIED");
					Player.GetComponent<Movement>().dead = true;
                    GetComponent<_STOP_DEADEND>().SetDeadend();
				}
            }

            if (timer >= 10)
            {
                // Signal that animations should have ended Debug
                //Debug.Log("UNGONG");
                 moved = false;
                GONG = false;
                doorSound = false;
                timer = 0;
                second = 0;
            }
        }
	}
}
