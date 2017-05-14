using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGen : MonoBehaviour {

    public bool pre = true;
    public Dictionary<string, bool> coordinates;

    // Use this for initialization
    void Start () {
        coordinates = new Dictionary<string, bool>();
        // Creates a predeterminted, manufactured map
        if(pre)
        {
            // Initialize all rooms as "enterable" (6x 5y map)
            for(int i = 0; i < 6; i++)
            {
                for(int j = 0; j < 5; j++)
                {
                    var coord = i.ToString() + j.ToString();
                    // Debug.Log(coord);
                    coordinates.Add(coord, true);
                }
            }
            // Remove the rooms based in Winston's notebook:
            coordinates.Remove("00");
            coordinates.Remove("02");
            coordinates.Remove("03");
            coordinates.Remove("12");
            coordinates.Remove("32");
            coordinates.Remove("40");
            coordinates.Remove("52");
            coordinates.Remove("53");

            // Debug function to see if correct rooms added then removed.
            foreach (KeyValuePair<string, bool> entry in coordinates)
            {
                //Debug.Log(entry.ToString());
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
