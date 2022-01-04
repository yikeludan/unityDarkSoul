using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ff : MonoBehaviour
{


    private Rigidbody _rigidbody;

    private Vector2 dir;

    private float sideInput;
    private float forwardInput;

    public Transform muzzlePoint;

    public GameObject muzzle;

    private float thrust = 10f;

    private float moveSpeed = 1000f;



    private void Awake()
    {
        _rigidbody = this.GetComponent<Rigidbody>();

    }



    private void FixedUpdate()
    {
        if (forwardInput != 0)
        {
            _rigidbody.AddRelativeForce(Vector3.up * forwardInput * thrust,ForceMode.Force);

        }

     //如果按下鼠标左键，移动速度变快


        /*Vector2 dir =
            this.transform.up * forwardInput;
        _rigidbody2D.velocity = dir * moveSpeed * Time.fixedDeltaTime;*/

        /*_rigidbody.AddRelativeForce(Vector2.up * forwardInput * thrust);
        _rigidbody.AddRelativeForce(Vector2.right * sideInput * thrust);*/

        // _rigidbody2D.AddRelativeForce(Vector2.up * forwardInput * thrust);
        // _rigidbody2D.AddRelativeForce(Vector2.right * sideInput * thrust, ForceMode.Force);

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sideInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");


        Vector3 mouseDir = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dir = mouseDir - transform.position;
        dir = dir.normalized;
        transform.up = dir;

        if (Input.GetMouseButtonDown(0))
        {
            GameObject bullet = Instantiate(muzzle,
                muzzlePoint.transform.position,
                muzzlePoint.transform.rotation);


        }
    }
}
