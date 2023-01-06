using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class demo3 : MonoBehaviour
{


    public Transform target;

    public float speed;

    public float angel;

    public float disRate;

    private void Awake()
    {
        this.speed = 10f;
        this.angel = 45f;
        this.disRate = 0.05f;
        Quaternion quaternion = Quaternion.Euler(0,0,45);
       // this.transform.up = quaternion * this.transform.up;
    }

    void Move()
    {
        Vector3 dir = this.target.position - this.transform.position;
        this.transform.up = dir.normalized;
        this.transform.rotation = this.transform.rotation * 
                                  Quaternion.Euler(0, 0, this.angel);
        this.transform.position += this.transform.up * this.speed * Time.deltaTime;
    }
    
    void Move1()
    {
        Vector3 dir = this.target.position - this.transform.position;
        this.transform.up = Vector3.Slerp(this.transform.up, dir,
            this.disRate / Vector2.Distance(this.transform.position, this.target.position));
        this.transform.position += this.transform.up * this.speed * Time.deltaTime;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.Move1();
    }
}
