using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    public Camera camera;
    public GameObject controlled;

    public EvilBase eBase;
    //public GameObject target;

    private Vector2 movement;
    // Start is called before the first frame update

    private static PlayerController _instance;

    public static PlayerController Instance()
    {
        return _instance;
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    void Start()
    {
        //eBase = gameObject.GetComponent<EvilBase>();
        rb = controlled.GetComponent<Rigidbody2D>();
        eBase = controlled.GetComponent<EvilBase>();
    }

    // Update is called once per frame
    void Update()
    {

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            //Debug.Log("E key was pressed.");
            (EvilBase,float) switchTargetTuple = EvilBase.FindNearestEvil(eBase);
            //Debug.Log(switchTargetTuple.Item2);
            if (switchTargetTuple.Item2 < 3.5f)
            {
                controlled = switchTargetTuple.Item1.gameObject;
                rb = controlled.GetComponent<Rigidbody2D>();
                eBase = controlled.GetComponent<EvilBase>();
            }
        }

        if (Input.GetMouseButtonDown(0) && !eBase.isCooldown)
        {
            
            StartCoroutine(eBase.Attack());
        }
        
        
    }

    private void FixedUpdate()
    {
        movement = movement.normalized;
        rb.MovePosition(rb.position + (Time.fixedDeltaTime * moveSpeed * movement ));
        camera.transform.position = new Vector3(eBase.transform.position.x, eBase.transform.position.y, -10);

    }
}
