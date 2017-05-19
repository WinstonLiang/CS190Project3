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

	// Use this for initialization
	void Start () {
        timer = 0;
        GONG = false;
        second = 0;
        outside = false;
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

            if (timer >= 5 && !GONG)
            {
                // Signal for all to move Debug
                //Debug.Log("GONG");
                GONG = true;

                foreach (GameObject thing in moving)
                {
                    thing.GetComponent<Movement>().SwitchMove();
                    if (thing.name == "SamplePlayer") // TODO: Replace with actual name later
                        if (thing.GetComponent<Movement>().currentRoom == Rooms.GetComponent<RoomGen>().exit)
                            outside = true;
                }
                GetComponent<_WALK>().Walk();
            }
            if (timer >= 10)
            {
                // Signal that animations should have ended Debug
                //Debug.Log("UNGONG");
                GONG = false;
                timer = 0;
                second = 0;
            }
        }
	}
}
