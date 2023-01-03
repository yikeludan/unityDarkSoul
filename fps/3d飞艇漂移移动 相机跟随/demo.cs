using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class demo : MonoBehaviour
{
    private Rigidbody rigidbody;

    private Vector3 dir;

    public float speed;

    public float t;

    public Vector3 cameraDec;


    public Transform camera;

    public Transform rotCenter;

    public Transform rotCenterV2;

    public Transform testRot;

    public float rotSpeed;


    public float raduisSpeed;


    private Vector3 rotOffsetVec3 = new Vector3(20, 0, 0);

    public float angel;

    public bool flag;

    private void Awake()
    {
        this.rigidbody = this.GetComponent<Rigidbody>();
        this.speed = 350f;
        this.rigidbody.drag = 2.5f;
        this.cameraDec =  new Vector3(0, 45, -60);
        this.raduisSpeed = 150f;
        flag = false;

    }

    // Update is called once per frame
    void Update()
    {
        //this.Move();
        this.Dir();
        this.RotateByRotCenterOffsetVec3();
       // this.dirV2();

    }

    private void FixedUpdate()
    {
       this.Move1();
    }

    private void LateUpdate()
    {
       // this.CameraFollowMove();
    }

    void CameraFollowMove()
    {
        this.camera.position = this.transform.position + this.cameraDec ;
        Vector3 dir = this.transform.position - this.camera.position;
        Quaternion quaternion = Quaternion.LookRotation(dir);
        this.camera.rotation = quaternion;
    }


    void CameraFollowMoveV1()
    {


        this.camera.position = Vector3.Lerp(this.camera.position,
            this.transform.position + new Vector3(0,20,0)+ this.transform.forward * -40,
            5f * Time.fixedDeltaTime);
        Vector3 dir = this.transform.position - this.camera.position;
        Quaternion quaternion = Quaternion.LookRotation(dir);
        this.camera.rotation = quaternion;

    }


    /**
     *
     *
     * 圆心距离飞船的位置
     *
     */
    void RotateByRotCenterOffsetVec3()
    {
        this.rotCenter.position = this.transform.position - this.rotOffsetVec3 * 2;
        this.rotCenterV2.position = this.transform.position + this.rotOffsetVec3 * 2;

    }

    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");//获取水平偏移量（x轴）
        float vertical = Input.GetAxis("Vertical");    //获取垂直偏移量（z轴）
        //将水平偏移量与垂直偏移量组合为一个方向向量
        Vector3 direction = new Vector3(horizontal, 0, vertical);
        //判断是否有水平偏移量与垂直偏移量产生
        if (direction != Vector3.zero)
        {
            //将游戏对象的z轴转向对应的方向向量
            //            transform.rotation = Quaternion.LookRotation(direction);
            //对上一行代码进行插值运算则可以将转向表现得较平滑
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.3f);
            //将游戏对象进行移动变换方法则可以实现简单的物体移动
            this.transform.Translate(Vector3.forward * 5 * Time.deltaTime,Space.Self);
        }
    }
    
    void Move1()
    {
       
        if (this.dir != Vector3.zero)
        {
            this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation,
                Quaternion.LookRotation(this.dir), Time.deltaTime * this.raduisSpeed);
            this.speed = Mathf.Lerp(1, 550, Time.time * this.rotSpeed);
            this.rigidbody.AddForce(this.transform.forward * this.speed,ForceMode.Force);
            // this.rigidbody.velocity = this.dir * this.speed;

        }
        this.CameraFollowMoveV1();

    }
    
    
    void Dir()
    {
        float horizontal = Input.GetAxis("Horizontal");//获取水平偏移量（x轴）
        float vertical = Input.GetAxis("Vertical");    //获取垂直偏移量（z轴）
        //将水平偏移量与垂直偏移量组合为一个方向向量
        Vector3 direction = new Vector3(horizontal, 0, vertical);
        this.dir = direction;



    }

    void dirV2()
    {
        this.angel += 55 * Time.deltaTime;

        Quaternion quaternion = Quaternion.Euler(0, this.angel, 0);
        this.transform.position = this.rotCenterV2.position +
                                  quaternion * -this.rotOffsetVec3;
        this.transform.rotation = quaternion;
    }
}
