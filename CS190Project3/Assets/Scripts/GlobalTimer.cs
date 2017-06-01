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
            if (timer >= 4 && !GONG)
            {
                 foreach (ROOM r in Rooms.GetComponent<RoomGen>().rooms)
                 {
                      r.UpDoor.GetComponent<Animator>().SetInteger("DoorState", 1);
                      r.LeftDoor.GetComponent<Animator>().SetInteger("DoorState", 1);
                      r.DownDoor.GetComponent<Animator>().SetInteger("DoorState", 1);
                      r.RightDoor.GetComponent<Animator>().SetInteger("DoorState", 1);
                 }
            }
            if (timer >= 5 && !GONG)
            {
                // Signal for all to move Debug
                //Debug.Log("GONG");
                GONG = true;
                foreach (ROOM r in Rooms.GetComponent<RoomGen>().rooms)
                {
                     r.UpDoor.GetComponent<Animator>().SetInteger("DoorState", -1);
                     r.LeftDoor.GetComponent<Animator>().SetInteger("DoorState", -1);
                     r.DownDoor.GetComponent<Animator>().SetInteger("DoorState", -1);
                     r.RightDoor.GetComponent<Animator>().SetInteger("DoorState", -1);
                }
                foreach (GameObject thing in moving)
                {

                    if (thing.name == "SamplePlayer") // TODO: Replace with actual name later
                    {
                        thing.GetComponent<Movement>().SwitchMove();
                        if (thing.GetComponent<Movement>().currentRoom == Rooms.GetComponent<RoomGen>().exit)
                            outside = true;
                        Player = thing;
                    }
                    else
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

                if (monsterDistance < 1)
                    Debug.Log("YOU DIED");
            }
            if (timer >= 6 && GONG)
            {
                 foreach (ROOM r in Rooms.GetComponent<RoomGen>().rooms)
                 {
                      r.UpDoor.GetComponent<Animator>().SetInteger("DoorState", 0);
                      r.LeftDoor.GetComponent<Animator>().SetInteger("DoorState", 0);
                      r.DownDoor.GetComponent<Animator>().SetInteger("DoorState", 0);
                      r.RightDoor.GetComponent<Animator>().SetInteger("DoorState", 0);
                 }
            }
            if (timer >= 10)
            {
                // Signal that animations should have ended Debug
                //Debug.Log("UNGONG");
                GONG = false;
                doorSound = false;
                timer = 0;
                second = 0;
            }
        }
	}
}
