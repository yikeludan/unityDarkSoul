using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class round : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform target;

    public Transform cameraTarget;

    private Transform roundPoint;

    private Vector3 v;

    private Quaternion q;

    private float speed = 1f;

    private Vector3 dir;

    public AnimationCurve animationCurve;
    
    
    private float timer = 0;


    private void Awake()
    {
        this.roundPoint = this.transform;
    }

    void Start()
    {
        v = new Vector3(0, 0, 10);
        
        this.cameraTarget.position = this.roundPoint.forward+v;
        dir =  this.roundPoint.position - this.cameraTarget.position;
        this.cameraTarget.LookAt(dir);
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        timer += Time.deltaTime;
        float y = animationCurve.Evaluate(timer);
        q = Quaternion.Euler(0,speed,0);
        Vector3 resV = q * dir.normalized *10;
        this.cameraTarget.position =  resV;
        
        //this.cameraTarget.rotation = Quaternion.Lerp(this.cameraTarget.rotation, q, Time.deltaTime * 5f);
        
        this.cameraTarget.rotation = q;
        speed += h *y* 50 * Time.deltaTime;
    }
}
