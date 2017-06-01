using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour {

    public ROOM currentRoom;
    public RoomGen RoomCoords;
    public GameObject player;

    public float currentSpeed;

    string currentPosition;

    bool playedSound;
    bool moveFailed;

    int x = 0;
    int y = 0;

    float constant = 4.5f;

    string direction;

    public float toMove;

    // Use this for initialization
    void Start () {
        toMove = 0;
        currentPosition = currentRoom.coordinate;

        Int32.TryParse(currentPosition[0].ToString(), out x);
        Int32.TryParse(currentPosition[1].ToString(), out y);
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(currentRoom.transform.position.x, currentRoom.transform.position.y, 0);
	}

    public void SwitchMove()
    {
        toMove += 1;
        if (toMove > 1)
        {
            int monsterX = 0;
            int monsterY = 0;

            Int32.TryParse(currentPosition[0].ToString(), out monsterX);
            Int32.TryParse(currentPosition[1].ToString(), out monsterY);

            int playerX = 0;
            int playerY = 0;

            string playerPosition = player.GetComponent<Movement>().currentRoom.coordinate;

            Int32.TryParse(playerPosition[0].ToString(), out playerX);
            Int32.TryParse(playerPosition[1].ToString(), out playerY);

            bool updown = true;
            bool rando = false;

            float distanceToPlayer = Vector3.Distance(transform.position / constant, player.transform.position / constant) * 2;
            if (distanceToPlayer >= 0.75)
            {
                if (Math.Abs(playerX - monsterX) < Math.Abs(playerY - monsterY))
                    updown = true;
                else if (Math.Abs(playerX - monsterX) > Math.Abs(playerY - monsterY))
                    updown = false;
                else if (Math.Abs(playerX - monsterX) == Math.Abs(playerY - monsterY))
                {
                    rando = true;
                }

                if (rando)
                {
                    if (UnityEngine.Random.Range(0.0f, 1.0f) > 0.5)
                        updown = true;
                    else
                        updown = false;
                }

                if (updown) // Try closing the Y distance.
                {
                    if (playerY > monsterY)
                    {
                        MoveUpOrDown(monsterX, monsterY, 1);
                    }
                    else
                    {
                        MoveUpOrDown(monsterX, monsterY, -1);
                    }

                    while (moveFailed) // The move did not work. TRY AGAIN.
                    {
                        if (UnityEngine.Random.Range(0.0f, 1.0f) > 0.5)
                        {
                            MoveLeftOrRight(monsterX, monsterY, 1);
                        }
                        else
                        {
                            MoveLeftOrRight(monsterX, monsterY, -1);
                        }
                    }
                }
                else // Try closing the X distance.
                {
                    if (playerX > monsterX)
                    {
                        MoveLeftOrRight(monsterX, monsterY, 1);
                    }
                    else
                    {
                        MoveLeftOrRight(monsterX, monsterY, -1);
                    }

                    while (moveFailed) // The move did not work. TRY AGAIN.
                    {
                        if (UnityEngine.Random.Range(0.0f, 1.0f) > 0.5)
                        {
                            MoveUpOrDown(monsterX, monsterY, 1);
                        }
                        else
                        {
                            MoveUpOrDown(monsterX, monsterY, -1);
                        }
                    }
                }
            }
            toMove = 0;
        }
    }

    void MoveUpOrDown(int currentX, int currentY, int direction)
    {
        currentY += direction;
        string tryCoordinate = currentX.ToString() + currentY.ToString();

        if (RoomCoords.coordinates.ContainsKey(tryCoordinate))
        {
            currentRoom = RoomCoords.coordinates[tryCoordinate];
            currentPosition = currentRoom.coordinate;
            moveFailed = false;
        }
        else
            moveFailed = true;
    }

    void MoveLeftOrRight(int currentX, int currentY, int direction)
    {
        currentX += direction;
        string tryCoordinate = currentX.ToString() + currentY.ToString();

        if (RoomCoords.coordinates.ContainsKey(tryCoordinate))
        {
            currentRoom = RoomCoords.coordinates[tryCoordinate];
            currentPosition = currentRoom.coordinate;
            moveFailed = false;
        }
        else
            moveFailed = true;
    }
}
