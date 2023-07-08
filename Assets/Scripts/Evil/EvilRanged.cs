using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilRanged : EvilBase
{
    // Start is called before the first frame update
    public GameObject projectile;

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public override IEnumerator Attack()
    {
        isCooldown = true; 
        Instantiate(projectile);
        Debug.Log("I attacked!!");
        yield return new WaitForSeconds(cooldownTime);
        isCooldown = false;
    }
}
