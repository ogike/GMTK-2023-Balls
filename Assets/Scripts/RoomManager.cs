using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    //Singleton
    //ezt a MonoBehaviourt bárhonnan el tudod érni a Sceneben a RoomManager.Instance hívással
    public static RoomManager Instance { get; private set; }

    public int roomWidth;
    public int roomHeight;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple GameManagers active in this scene, only one allowed!");
            return;
        }

        Instance = this;
    }
}
