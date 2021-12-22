using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hero : MonoBehaviour
{


    private Rigidbody _rigidbody;

    private float maxSpeed = 5;

    private Vector3 upV3;

    private Vector3 downV3;

    private float timer = 0;

    private void Awake()
    {
        this._rigidbody = this.GetComponent<Rigidbody>();
    }
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        //按W键 前进
        if (Input.GetKey(KeyCode.W))
        {

            Vector3 dir = transform.forward * 1 * maxSpeed;
            dir += this.transform.position;
            this.transform.position = Vector3.Lerp(this.transform.position, dir, Time.deltaTime);

        }
        //按S键 后退
        if (Input.GetKey(KeyCode.S))
        {
            Vector3 dir = this.transform.forward * -1 * maxSpeed;
            dir += this.transform.position;
            this.transform.position = Vector3.Lerp(this.transform.position, dir, Time.deltaTime);

        }
        //按A键 向左旋转
        if (Input.GetKey(KeyCode.A))
        {
            timer += Time.deltaTime;
            Quaternion q = Quaternion.Euler(0, -30, 0);
            float a = 0;
            a =  Mathf.Lerp(a, -30, Time.deltaTime * maxSpeed);
            this.transform.Rotate(this.transform.up,a);


        }
        //按D键 向右旋转
        if (Input.GetKey(KeyCode.D))
        {
            Quaternion q = Quaternion.Euler(0, 30, 0);

            float a = 0;
            a = Mathf.Lerp(a, 30, Time.deltaTime * maxSpeed);
            transform.Rotate(transform.up, a);

        }
    }

    
}
