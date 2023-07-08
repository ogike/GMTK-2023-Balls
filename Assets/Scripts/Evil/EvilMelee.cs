using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilMelee : EvilBase
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void FixedUpdate()
    {
        if (!pc.isActive)
        {
            Vector2 dir = target.transform.position - gameObject.transform.position;
            
            if (dir.magnitude < range)
            {
                Attack();
            }
            else
            {
                Vector2 normDir = dir.normalized;
                rb.MovePosition(rb.position + (Time.fixedDeltaTime * moveSpeed * normDir ));
            }
            
        }
    }

    void Attack()
    {
        Debug.Log("I attacked!!");
    }
}
