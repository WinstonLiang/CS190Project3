using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalTimer : MonoBehaviour {

    public float timer = 0;
    public List<GameObject> moving;
    public int second = 0;
    public GameObject Rooms;

    public bool outside;

    bool GONG;
    bool doorSound;
    bool moved = false;

    GameObject Player, Monster;

    float monsterDistance = 4;

	// Use this for initialization
	void Start () {
        timer = 0;
        GONG = false;
        second = 0;
        outside = false;
        doorSound = false;
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

        if (!outside)
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
                //GetComponent<_WALK>().Walk();

                float oldDistance = monsterDistance;

                monsterDistance = Vector3.Distance(Player.transform.position / 4.5f, Monster.transform.position / 4.5f);
                // So the monster looks for the approximate change in position between the player and itself.
                // Reducing said change by the constant 4.5 that all rooms are in size.
                // Multiplied by 2 to get a more apparent "closeness" to the player.

                //Debug.Log(distanceToMonster);
                Debug.Log(monsterDistance);

                if (monsterDistance > 3)
                    GetComponent<MONSTER_TOO_FAR>().Bye();
                if (monsterDistance <= 3)
                {
                    if (oldDistance > monsterDistance)
                        GetComponent<MONSTER_CLOSER>().Step();
                    else if (oldDistance < monsterDistance)
                        GetComponent<MONSTER_FARTHER>().Step();
                }

                // AkSoundEngine.SetRTPCValue("Monster_Coming", monsterDistance);
				
				bool sameRoom = Monster.GetComponent<MonsterMovement>().currentRoom == Player.GetComponent<Movement>().currentRoom;

                if (monsterDistance < 1 || sameRoom)
				{
                    Debug.Log("YOU DIED");
					Player.GetComponent<Movement>().dead = true;
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
