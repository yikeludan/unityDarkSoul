using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gh : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform target;

    private float _radius_length = 10f;
    private float _angle_speed = 1f;


    private float temp_angle = 0f;


    private Vector3 _pos_new;


    private Vector3 center_pos;


//https://www.yuque.com/yikeludan/hvgb01/gekmv4

    private void Awake()
    {
        this.center_pos = this.transform.localPosition;
        //this.target.rotation = Quaternion.Euler(0,90,0);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        /*
        Vector3 dir = this.target.position - this.transform.position;



        Quaternion quaternion = Quaternion.LookRotation(this.target.forward);
        this.transform.rotation = quaternion;*/
       // this.transform.LookAt(this.target.position);


       this.temp_angle += this._angle_speed * Time.deltaTime; //

       this._pos_new.x = this.center_pos.x + Mathf.Cos(this.temp_angle) * this._radius_length;
       this._pos_new.y = this.center_pos.y + Mathf.Sin(this.temp_angle) * this._radius_length;
       this._pos_new.z = this.transform.localPosition.z;

       this.target.transform.localPosition = this._pos_new;

       float rr = Mathf.Atan2(this._pos_new.y, this._pos_new.x);
       float angel = this.temp_angle * Mathf.Rad2Deg;


       this.target.rotation = Quaternion.Euler(0,0,angel+90);

    }
}
