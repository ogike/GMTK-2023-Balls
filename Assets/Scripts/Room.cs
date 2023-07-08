using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Flags]
public enum DoorDirections
{
    Up = 1,
    Right = 2,
    Down = 4,
    Left = 8
}

public class Room : MonoBehaviour
{

    public DoorDirections doorDirections;

    
    //where the hero comes from and where does it go, where did you come from Cotton-Eye Joe?
    // public DoorDirections BeforeDirection { get; set; }
    public DoorDirections AfterDirection { get; set; }

    public int gridXPos;
    public int gridYPos;

    public Room NextRoom { get; private set; }

    public void SetSpawnData(int gridX, int gridY)
    {
        gridXPos = gridX;
        gridYPos = gridY;
    }

    public void SetAfterDirection(DoorDirections toDir, Room nextRoom)
    {
        AfterDirection = toDir;
        NextRoom = nextRoom;
    }

    /// <summary>
    /// Checks if we are open towards the other room
    /// </summary>
    /// <param name="otherToUs">According to the other room, where are we to them?</param>
    /// <returns></returns>
    public bool OpenFrom(DoorDirections otherToUs)
    {
        if (otherToUs == DoorDirections.Left && doorDirections.HasFlag(DoorDirections.Right)) return true;
        
        if (otherToUs == DoorDirections.Right && doorDirections.HasFlag(DoorDirections.Left)) return true;
        
        if (otherToUs == DoorDirections.Down && doorDirections.HasFlag(DoorDirections.Up)) return true;
        
        if (otherToUs == DoorDirections.Up && doorDirections.HasFlag(DoorDirections.Down)) return true;

        return false;
    }
}
