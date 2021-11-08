using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBullet : Bullet
{
    private TrailRenderer trailRenderer;

   
    private void Awake()
    {
        this.trailRenderer = this.GetComponentInChildren<TrailRenderer>();
        if (vector2 != Vector2.right)
        {
            this.transform.GetChild(0).rotation = 
                Quaternion.FromToRotation(Vector2.right, vector2);
        }
    }
    
    private void OnDisable()
    {
        this.trailRenderer.Clear();
    }

    protected override void OnCollisionEnter2D(Collision2D collision2D)
    {
        base.OnCollisionEnter2D(collision2D);
        PlayerEnery.instance.Obtain(PlayerEnery.instance.percent *10);
    }
}
