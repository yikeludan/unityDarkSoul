using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    //向后的距离
    public float backDistance = 2;

    //高度
    public float topDistance = 2;



    private float maxSpeed = 0.5f;

    private Vector3 initPos;

    private Vector3 kxPos;
    //振幅
    public float zhenFu = 0.3f;
    //频率
    public float HZ = 0.3f;

    private float x = 0;

    private float z = 0;



    private void Awake()
    {
        initPos = this.transform.position;
    }

    private void Update()
    {

        kxPos = initPos;
        //正弦函数 模拟漂浮效果
        kxPos.y = Mathf.Sin(Time.fixedTime * Mathf.PI * HZ) * (zhenFu/10) + 1;
    }

    //在LateUpdate中进行物理
    void LateUpdate()
    {

        //设置偏移量
        offset = -target.forward * backDistance + target.up * topDistance;
        x = this.target.position.x + offset.x;
        z = this.target.position.z + offset.z;
        Vector3 dir = new Vector3(x,
            kxPos.y,
            z);
        //使用插值，让宠物有一个平滑的移动
        transform.position = Vector3.Lerp(transform.position, dir,Time.deltaTime/maxSpeed);
        //宠物的旋转和玩家的旋转保持一致
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation,
            this.target.rotation,
            Time.deltaTime/maxSpeed);
        //transform.rotation = target.rotation;

    }
}
