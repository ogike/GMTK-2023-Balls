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
    //public PlayerController pc;
    
    static List<EvilBase> _instances = new List<EvilBase>( );
    public static List<EvilBase> instances { get { return _instances; } }

    public float cooldownTime = 1;
    public bool isCooldown = false;

    public static (EvilBase,float) FindNearestEvil(EvilBase me)
    {
        EvilBase target = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = me.transform.position;
        foreach (EvilBase e in _instances)
        {
            if (e == me)
            {
                continue;
            }
            //Debug.Log("Hello");
            Transform t = e.transform;
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist)
            {
                minDist = dist;
                target = e;
            }
        }

        return (target, minDist);
    }
    private void Awake()
    {
        _instances.Add(this);
    }

    void Start()
    {
        FindNearestEvil(this);
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public virtual IEnumerator Attack()
    {
        Debug.Log("I attacked!!");
        yield return new WaitForSeconds(cooldownTime);
    }
    

    /*void SetAsPlayer()
    {
        if (true) //TODO: iterate over all evils to ensure only one is player
        {
            isPlayer = true;
            pc.isActive = true;
        }
    }*/
}
