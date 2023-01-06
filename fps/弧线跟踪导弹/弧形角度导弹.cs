using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class demo3 : MonoBehaviour
{


    public Transform target;

    public Transform mid;

    public float speed;
    
    public float speed1;

    public float angel;

    public float disRate;

    public Vector3 p1;

    private Vector3 p0;

    private Vector3 p2;

    private void Awake()
    {
        this.speed = 10f;
        this.speed1 = 5f;
        this.angel = 45f;
        this.disRate = 0.05f;
        Quaternion quaternion = Quaternion.Euler(0,0,45);
        p0 = this.transform.position;
        p1 = this.mid.position;
        p2 = this.target.position;
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
    
    
    void Move2()
    {
       Vector3 p =  this.Bezier(p0, p1, p2,Time.time * this.speed1 * Time.fixedDeltaTime);
       Vector3 dir = p - transform.position;
       transform.up = dir;
       transform.position = Vector3.MoveTowards(transform.position, p, Time.deltaTime * this.speed1);
    }

    Vector3 Bezier(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        Vector3 result;
        Vector3 p0p1 = (1 - t) * p0 + t * p1;
        Vector3 p1p2 = (1 - t) * p1 + t * p2;
        Vector3 p2p3 = (1 - t) * p2 + t * p3;
        Vector3 p0p1p2 = (1 - t) * p0p1 + t * p1p2;
        Vector3 p1p2p3 = (1 - t) * p1p2 + t * p2p3;
        result = (1 - t) * p0p1p2 + t * p1p2p3;
        return result;
    }

    Vector3 Bezier(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        Vector3 p0p1 = (1 - t) * p0 + t * p1;
        Vector3 p1p2 = (1 - t) * p1 + t * p2;
        Vector3 result = (1 - t) * p0p1 + t * p1p2;
        return result;
    }




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.Move2();
        
    }
}
