using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EvilMelee : EvilBase
{
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        
    }
    
    void FixedUpdate()
    {
        if (PlayerController.Instance().controlled != this && target != null)
        {
            Vector2 dir = target.transform.position - gameObject.transform.position;
            
            if (dir.magnitude < range && !isCooldown)
            {
                StartCoroutine(Attack());
                
            }
            else
            {
                Vector2 normDir = dir.normalized;
                rb.MovePosition(rb.position + (Time.fixedDeltaTime * moveSpeed * normDir ));
            }
            
        }
    }

    public override IEnumerator Attack()
    {
        isCooldown = true;
        Debug.Log("I attacked!!");
        yield return new WaitForSeconds(cooldownTime);
        isCooldown = false;
    }
}
