using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paowu1 : MonoBehaviour
{
     public float ShotSpeed = 10; // 抛出的速度
        private float time;          // A-B的时间
        public Transform pointA;     // 起点
        public Transform pointB;     // 终点
        public float g = -10;        // 重力加速度

        private Vector3 speed;       // 初速度向量
        private Vector3 Gravity;     // 重力向量
        private Vector3 currentAngle;// 当前角度
        private float dTime = 0;
        void Start()
        {

            // 时间=距离/速度
            time = Vector3.Distance(pointA.position, pointB.position)/ShotSpeed;

            // 设置起始点位置为A
            transform.position = pointA.position;

            // 通过一个式子计算初速度
            speed = new Vector3((pointB.position.x - pointA.position.x) / time,
                (pointB.position.y - pointA.position.y) / time - 0.5f * g * time,
                (pointB.position.z - pointA.position.z) / time);
            // 重力初始速度为0
            Gravity = Vector3.zero;
        }
        // Update is called once per frame
        void FixedUpdate()
        {
            // v=gt
            dTime += Time.fixedDeltaTime;
            Gravity.y = g * dTime;
            //类似物理中的合力
            Vector3 dirVector3 = speed + Gravity;
            //模拟位移
            transform.position += dirVector3 * Time.fixedDeltaTime;
            // 弧度转度：Mathf.Rad2Deg
            currentAngle.x = -Mathf.Atan((speed.y + Gravity.y) / speed.z) * Mathf.Rad2Deg;
            // 设置当前角度
            transform.eulerAngles = currentAngle;
        }
}
