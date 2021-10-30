using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private GameObject PlayHandle;

    private GameObject CameraHandle;

    private PlayerInput pi;

    private float tempEulerX;

    private GameObject player;
    
    [HideInInspector]
    public GameObject camera;
    void Awake()
    {
        this.CameraHandle = this.transform.parent.gameObject;
        this.PlayHandle = this.CameraHandle.transform.parent.gameObject;
        this.pi = this.PlayHandle.GetComponent<PlayerInput>();
        this.tempEulerX = 20;
        this.player  = this.PlayHandle.GetComponent<ActorController>().player;
        this.camera = Camera.main.gameObject;
    }
    

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 tempPlayerEuler = this.player.transform.eulerAngles;
        
        this.PlayHandle.transform.Rotate(Vector3.up,this.pi.Jright *100f* Time.fixedDeltaTime);
        //this.CameraHandle.transform.Rotate(Vector3.right,this.pi.Jup * 80f * Time.deltaTime);
        //this.tempEulerX = this.CameraHandle.transform.eulerAngles.x;
        this.tempEulerX -= this.pi.Jup * -80f * Time.fixedDeltaTime;
        this.tempEulerX = Mathf.Clamp(this.tempEulerX, -40, 30);
        this.CameraHandle.transform.localEulerAngles = new Vector3(this.tempEulerX, 0, 0);

        this.player.transform.eulerAngles = tempPlayerEuler;

        this.camera.transform.position = Vector3.Lerp(this.camera.transform.position, this.transform.position, 0.3f);
        //this.camera.transform.rotation = this.transform.rotation;
        this.camera.transform.LookAt(this.CameraHandle.transform);
    }
}
