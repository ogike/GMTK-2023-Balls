using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilRanged : EvilBase
{
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        
    }
    
    public override IEnumerator Attack()
    {
        Debug.Log("I attacked!!");
        yield return new WaitForSeconds(cooldownTime);
    }
}
