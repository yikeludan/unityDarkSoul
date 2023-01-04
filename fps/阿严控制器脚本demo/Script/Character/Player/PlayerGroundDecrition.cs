using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundDecrition : MonoBehaviour
{
    [SerializeField]float radius = 0.1f;

    [SerializeField] LayerMask groundLayer;

    private Collider[] colliders = new Collider[1];

    public bool isGround => Physics.OverlapSphereNonAlloc(
        this.transform.position,
        radius,colliders, groundLayer)!=0;


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position,radius);
    }

    private void Update()
    {
        Debug.Log("colliders = "+colliders[0].name);
    }
}
