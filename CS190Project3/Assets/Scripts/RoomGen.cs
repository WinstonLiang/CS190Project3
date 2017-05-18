using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGen : MonoBehaviour {

    // This script generates the coordinates for either the PROTOTYPE map or the FINAL map.
    // It is not designed for a "smart" read of a custom map; only the two for CS190 Project 3.

    public bool pre = true;
    public Dictionary<string, ROOM> coordinates;

    // This cannot be automagically added!
    // Each ROOM object must be manually added per scene!
    public List<ROOM> rooms;

    // Use this for initialization
    void Start () {
        coordinates = new Dictionary<string, ROOM>();

        // Initialize all rooms (6x 5y map)
        foreach (ROOM room in rooms)
            coordinates.Add(room.coordinate, room);

        //// Creates a predeterminted, manufactured map (FINAL MAP)
        //if (pre)
        //{

        //    // Initialize all rooms as "enterable" (6x 5y map)
        //    for (int i = 0; i < 6; i++)
        //    {
        //        for (int j = 0; j < 5; j++)
        //        {
        //            var coord = i.ToString() + j.ToString();
        //            // Debug.Log(coord);
        //            coordinates.Add(coord, true);
        //        }
        //    }
        //    // Remove the rooms based in Winston's notebook:
        //    coordinates["00"] = false;
        //    coordinates["02"] = false;
        //    coordinates["03"] = false;
        //    coordinates["12"] = false;
        //    coordinates["32"] = false;
        //    coordinates["40"] = false;
        //    coordinates["52"] = false;
        //    coordinates["53"] = false;
        //}

        //// Creates the rough map for M3 (PROTOTYPE MAP)
        //else
        //{
        //    // Initialize all rooms as "enterable" (3x 2y map)
        //    for(int i = 0; i < 3; i++)
        //    {
        //        for(int j = 0; j < 2; j++)
        //        {
        //            var coord = i.ToString() + j.ToString();
        //            coordinates.Add(coord, true);
        //        }
        //    }
        //    // Remove the rooms based on the prototype map:
        //    coordinates["01"] = false;
        //    coordinates["21"] = false;
        //}

        // Debug function to see if correct rooms added then removed.
        foreach (KeyValuePair<string, ROOM> entry in coordinates)
        {
            //Debug.Log(entry.ToString());
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
