using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class MapGenerator : MonoBehaviour
{
    public List<Room> possibleRooms;

    private List<Room> _roomsSpawned;

    public int numOfRoomToSpawn = 5;
    public int maxNumOfTries = 20;

    public Vector3 roomsStartingPos;

    private int _roomWidth;
    private int _roomHeight;

    public Transform roomsParent;

    private System.Random _random;
    
    [Tooltip("The gameobject indicating where the hero will go next")]
    public GameObject directionIndicator;
    
    void Awake()
    {
        _random = new System.Random();
        
        _roomWidth = RoomManager.Instance.roomWidth;
        _roomHeight = RoomManager.Instance.roomHeight;

        SpawnRooms();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SpawnRooms();
        }
    }

    public void SpawnRooms()
    {
        bool succes = false;
        int curNumOfTries = 0;

        do
        {
            if (_roomsSpawned != null)
            {
                foreach (var room in _roomsSpawned)
                {
                    GameObject.Destroy(room.gameObject);
                }
            }

            succes = TrySpawningRooms();
            curNumOfTries++;

        } while (succes == false && curNumOfTries < maxNumOfTries);

        if (succes == false)
        {
            Debug.LogError("Ran out of possible tries without succesfully generating map. Size might be too much");
        }
        else
        {
            Debug.Log("Room spawning finished in " + curNumOfTries + "tries.");
        }
    }
    

    bool TrySpawningRooms()
    {
        _roomsSpawned = new List<Room>();
        Room curRoom = SpawnSingleRoom(0, 0, possibleRooms[_random.Next(possibleRooms.Count)]);

        
        
        for (int i = 1; i < numOfRoomToSpawn; i++)
        {
            List<DoorDirections> possibleDirs = new List<DoorDirections>();
            
            if (curRoom.doorDirections.HasFlag(DoorDirections.Up))    possibleDirs.Add(DoorDirections.Up);
            if (curRoom.doorDirections.HasFlag(DoorDirections.Right)) possibleDirs.Add(DoorDirections.Right);
            if (curRoom.doorDirections.HasFlag(DoorDirections.Down))  possibleDirs.Add(DoorDirections.Down);
            if (curRoom.doorDirections.HasFlag(DoorDirections.Left))  possibleDirs.Add(DoorDirections.Left);

            //negate the directions in which there already is a room next to us
            foreach (Room other in _roomsSpawned)
            {
                //up
                if (other.gridXPos == curRoom.gridXPos && other.gridYPos == curRoom.gridYPos + 1)
                {
                    possibleDirs.Remove(DoorDirections.Up);
                    continue;
                }
                
                //down
                if (other.gridXPos == curRoom.gridXPos && other.gridYPos == curRoom.gridYPos - 1)
                {
                    possibleDirs.Remove(DoorDirections.Down);
                    continue;
                }
                
                //left
                if (other.gridXPos == curRoom.gridXPos - 1 && other.gridYPos == curRoom.gridYPos)
                {
                    possibleDirs.Remove(DoorDirections.Left);
                    continue;
                }
                
                //right
                if (other.gridXPos == curRoom.gridXPos + 1 && other.gridYPos == curRoom.gridYPos)
                {
                    possibleDirs.Remove(DoorDirections.Right);
                    continue;
                }
            }

            if (possibleDirs.Count < 1)
            {
                Debug.LogWarning("No choosable directions from this room!");
                return false;
            }

            DoorDirections chosenDirection = possibleDirs[_random.Next(possibleDirs.Count)];

            List<Room> choosableRooms = possibleRooms.FindAll(room => room.OpenFrom(chosenDirection));

            if (choosableRooms.Count < 1)
            {
                Debug.LogWarning("No choosable rooms from this direction!");
                return false;
            }

            Room chosenRoom = choosableRooms[_random.Next(choosableRooms.Count)];

            int newGridPosX = curRoom.gridXPos;
            int newGridPosY = curRoom.gridYPos;
            AddDirectionToGridPos(chosenDirection, ref newGridPosX, ref newGridPosY);

            Room nextRoom = SpawnSingleRoom(newGridPosX, newGridPosY, chosenRoom);
            
            curRoom.SetAfterDirection(chosenDirection, nextRoom);
            SpawnNextRoomIndicator(curRoom.gridXPos, curRoom.gridYPos, chosenDirection, curRoom);

            curRoom = nextRoom;

        }

        return true;
    }

    void AddDirectionToGridPos(DoorDirections dir, ref int xPos, ref int yPos)
    {
        if (dir == DoorDirections.Down)
        {
            yPos--;
            return;
        }
        
        if (dir == DoorDirections.Up)
        {
            yPos++;
            return;
        }
        
        if (dir == DoorDirections.Left)
        {
            xPos--;
            return;
        }
        
        if (dir == DoorDirections.Right)
        {
            xPos++;
            return;
        }
        
        //if multiple dirs or none, dont do anything
        return;
    }


    Room SpawnSingleRoom(int x, int y, Room toSpawn)
    {
        //every room will have its origin at the lower left corner 
        Vector3 worldCoord = new Vector3(x * _roomWidth, y * _roomHeight, 0);
        
        GameObject spawned = GameObject.Instantiate(toSpawn.gameObject, worldCoord,
                                                    Quaternion.identity, roomsParent);

        Room spawnedRoom = spawned.GetComponent<Room>();
        spawnedRoom.SetSpawnData(x, y);
        
        _roomsSpawned.Add(spawnedRoom);
        return spawnedRoom;
    }

    void SpawnNextRoomIndicator(int gridX, int gridY, DoorDirections toDir, Room room)
    {
        //btm left coords
        float finalX = gridX * _roomWidth;
        float finalY = gridY * _roomHeight;
        Quaternion finalRotation = quaternion.identity;

        switch (toDir)
        {
            case DoorDirections.Down:
                finalX += _roomWidth / 2f;
                finalRotation = Quaternion.Euler(0f, 0f, 180f);
                break;
            
            case DoorDirections.Up:
                finalX += _roomWidth / 2f;
                finalY += _roomHeight;
                finalRotation = Quaternion.Euler(0f, 0f, 0f);
                break;
            
            case DoorDirections.Left:
                finalY += _roomHeight / 2f;
                finalRotation = Quaternion.Euler(0f, 0f, 90f);
                break;
            
            case DoorDirections.Right:
                finalX += _roomWidth;
                finalY += _roomHeight / 2f;
                finalRotation = Quaternion.Euler(0f, 0f, -90f);
                break;
        }

        GameObject.Instantiate(directionIndicator, 
            new Vector3(finalX, finalY, 0),
            finalRotation, 
            room.transform);
    }
    
}
