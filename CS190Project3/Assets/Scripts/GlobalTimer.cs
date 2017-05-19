using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalTimer : MonoBehaviour {

    public float timer = 0;
    public List<GameObject> moving;

    bool GONG;

	// Use this for initialization
	void Start () {
        timer = 0;
        GONG = false;
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;

        if(timer >= 5 && !GONG)
        {
            // Signal for all to move Debug
            Debug.Log("GONG");
            GONG = true;

            foreach(GameObject thing in moving)
            {
                thing.GetComponent<Movement>().SwitchMove();
            }
            this.GetComponent<_WALK>().Walk();
        }
        if(timer >= 10)
        {
            // Signal that animations should have ended Debug
            Debug.Log("UNGONG");
            GONG = false;
            timer = 0;
        }
	}
}
