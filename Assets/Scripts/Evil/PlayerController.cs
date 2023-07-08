using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Camera camera;
    //public GameObject target;

    private Vector2 movement;
    public bool isActive = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }
    }

    private void FixedUpdate()
    {
        if (isActive)
        {
            movement = movement.normalized;
            rb.MovePosition(rb.position + (Time.fixedDeltaTime * moveSpeed * movement ));
            camera.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -10);
        }
    }
}
