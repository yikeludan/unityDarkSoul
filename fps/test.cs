using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
       private Transform self;
   
       private CharacterController cc;
       
       public Transform target;
       public Vector3 offset;

       public float index;
       void Start()
       {
           this.self = this.transform;
           this.cc = this.GetComponent<CharacterController>();
       }
   
       private void LateUpdate()
       {
           if (index == 0)
           {
               return;
               
           }
           float rad  = Random.Range(0f, 3f);
           //设置偏移量
           offset = target.forward * (-2f) + target.up * rad;      
           //改变宠物的位置，让宠物移动
           transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * 0.2f);
           //让宠物和主人的旋转保持一致
           this.self.rotation = Quaternion.Lerp(this.self.rotation, this.target.rotation, Time.deltaTime * 2f);

       }
   
       // Update is called once per frame
       void Update()
       {
           if (index == 1)
           {
               return;
               
           }

           float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            if (h == 0 && v == 0)
            {
                return;
            }

            Debug.Log(h+","+v);
           
            Vector3 dir = new Vector3(h, 0, v);
            Vector3 worldDir = this.self.TransformDirection(dir);
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            this.self.rotation = Quaternion.Lerp(this.self.rotation, targetRotation, Time.deltaTime * 10f);
            this.cc.Move(this.self.forward * 10f * Time.deltaTime);
       }
}
