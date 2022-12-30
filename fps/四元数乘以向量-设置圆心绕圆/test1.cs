using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class camaerMove : MonoBehaviour
{
    // Start is called before the first frame update

    private float simMoveSpeed = 2f;

    public Transform target;

    public Transform player;

    public Transform mugun;

    
    public Vector3 mugunDis;

    private float mx = 0f;

    private float my = 0f;

    private float DampingV = 6.5f;

    private float DampingR = 2.5f;


    private bool isFlag = true;

    public Vector3 dis;

    public Vector3 rotDis;

    private float walkSpeed = 0f;

    private float walkDis = 0f;

    public float angelSpeed = 0f;

    private float angelDis = 0f;

    public float angel;

    private Animator _animator;


    private void Awake()
    {
        dis = new Vector3(0, 0.37f, -3.34f);
        this.rotDis = new Vector3(0, 0, -2);
        this.mugunDis = new Vector3(-2.57f, 0, 0);
        this.angelSpeed = 40f;
        this.angel = 45;
    }

    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
            mx += Input.GetAxis("Mouse X") * this.simMoveSpeed;
            my -= Input.GetAxis("Mouse Y") * this.simMoveSpeed;
            my = ClampAngle(my, 0, 60);
            if (Input.GetKey("w"))
            {
                this.walkSpeed = 1f;
            }
            else
            {
                this.walkSpeed = 0f;
            }
            this.walkDis = this.walkSpeed * Time.deltaTime;

    }


    private void demo1()
    {
        Quaternion targetQua  = Quaternion.Euler(my,mx,0);
        Vector3 vec3 = targetQua * dis + this.target.position;//目标点相当于圆心
        if (this.isFlag)
        {
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation,targetQua,Time.deltaTime * this.DampingR);
            this.transform.position = Vector3.Lerp(this.transform.position, vec3, Time.deltaTime * this.DampingR);
            // this.transform.position = vec3;
        }
        else
        {
            this.transform.rotation = targetQua;
            this.transform.position = vec3;

        }

        this.target.rotation = Quaternion.Euler(0,
            this.transform.localEulerAngles.y,
            0);
        this.target.Translate(0,0,this.walkDis,Space.Self);
    }


    private void demo2()
    {
        angelDis += this.angelSpeed * Time.deltaTime;
        Quaternion quaternion = Quaternion.Euler(0,angelDis,0);
        this.transform.position = this.target.position + quaternion * this.rotDis;
        this.transform.rotation = quaternion;
    }
    
    private void demo3()
    {
        angelDis += this.angelSpeed * Time.deltaTime;
        Quaternion quaternion = Quaternion.Euler(0,angelDis,0);
        this.mugun.position = this.target.position + quaternion * this.mugunDis;
        this.mugun.rotation = quaternion;
    }

    private void LateUpdate()
    {
        //this.demo1();
        this.demo3();
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
        {
            angle += 360;
        }

        if (angle > 360)
        {
            angle -= 360;
        }
        return Mathf.Clamp(angle, min, max);
    }
}
