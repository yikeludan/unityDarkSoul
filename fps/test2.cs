using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test2 : MonoBehaviour
{
    public GameObject box;
 
    private Transform Head;
    private Transform LeftAirfoil;
    private Transform RightArifoil;
    private Transform LeftTailAirfoil;
    private Transform RightTailAirfoil;
    
    private float speed = 250.0f;
 
    private Rigidbody rb;
 
    void Start()
    {
        box = GameObject.Find("Plane");
        
 
        Head = transform.Find("Head");
        LeftAirfoil = transform.Find("LeftAirfoil");
        RightArifoil = transform.Find("RightAirfoil");
        LeftTailAirfoil = transform.Find("LeftTailAirfoil");
        RightTailAirfoil = transform.Find("RightTailAirfoil");
 
        rb = GetComponent<Rigidbody>();
    }
 
    void FixedUpdate()
    {
       
        transform.Translate(Vector3.right * Time.deltaTime);
        //俯冲
        if(Input .GetKey (KeyCode.W ))
        {
            rb.AddForceAtPosition(transform.up * 5.0f, LeftTailAirfoil.position);
            rb.AddForceAtPosition(transform.up * 5.0f, RightTailAirfoil.position);
        }
        //爬升
        else if(Input .GetKey (KeyCode.S ))
        {
            rb.AddForceAtPosition(transform.up * -5.0f, LeftTailAirfoil.position);
 
            rb.AddForceAtPosition(transform.up * -5.0f, RightTailAirfoil.position);
        }
        //左翻滚
        else if(Input .GetKey (KeyCode.A ))
        {
            rb.AddForceAtPosition(transform.up * -5.0f, LeftTailAirfoil.position);
 
            rb.AddForceAtPosition(transform.up * 5.0f, RightTailAirfoil.position);
        }
        //右翻滚
        else if(Input .GetKey (KeyCode.D ))
        {
            rb.AddForceAtPosition(transform.up * 5.0f, LeftTailAirfoil.position);
            rb.AddForceAtPosition(transform.up * -5.0f, RightTailAirfoil.position);
        }
    }

}
