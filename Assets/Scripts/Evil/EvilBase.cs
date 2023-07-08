using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilBase : MonoBehaviour
{
    // Start is called before the first frame update
    
    public float moveSpeed = 5f;
    public float range = 2f;
    public Rigidbody2D rb;
    public GameObject target;
    public PlayerController pc;

    //private Vector2 movement;
    public bool isPlayer = false;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    void SetAsPlayer()
    {
        if (true) //TODO: iterate over all evils to ensure only one is player
        {
            isPlayer = true;
            pc.isActive = true;
        }
    }
}
