using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector2 dir;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = PlayerController.Instance().eBase.rb.position;
        var mousePos = PlayerController.Instance().camera.ScreenToWorldPoint(Input.mousePosition);
        dir = new Vector2(mousePos.x, mousePos.y) - PlayerController.Instance().eBase.rb.position;
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = dir.normalized * 10;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
