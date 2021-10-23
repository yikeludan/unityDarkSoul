using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGroundSener : MonoBehaviour
{

    public CapsuleCollider capcal;

    private float radius;

    private Vector3 point1;

    private Vector3 point2;
    void Awake()
    {
        this.radius = this.capcal.radius;
        
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        this.point1 = this.transform.position + this.transform.up * this.radius;
        this.point2 = this.transform.position + this.transform.up * this.capcal.height
                      - this.transform.up * this.radius;
        Collider[] colliders =  Physics.OverlapCapsule(this.point1, this.point2,this.radius,
            LayerMask.GetMask("Ground"));
        if (colliders.Length > 0)
        {
            SendMessageUpwards("isGround");
        }
        else
        {
            SendMessageUpwards("isNotGround");
        }
        

    }
}
