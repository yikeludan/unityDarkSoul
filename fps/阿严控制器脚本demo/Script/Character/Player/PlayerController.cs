using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;

    private Rigidbody rigidbody;

    public bool CanAirJump { get; set; }

    private PlayerGroundDecrition playerGroundDecrition;

    public bool isGrounded => playerGroundDecrition.isGround;

    public bool isFall => this.rigidbody.velocity.y < 0 && !isGrounded;
    private void Awake()
    {
        this.playerInput = this.GetComponent<PlayerInput>();
        this.playerGroundDecrition = this.GetComponentInChildren<PlayerGroundDecrition>();
        this.rigidbody = this.GetComponent<Rigidbody>();
    }

    private void Start()
    {
        this.playerInput.EnableGamePlayInputs();
    }

    private void Update()
    {
       // Debug.Log("isGrounded = "+isGrounded);
    }


    public  void SetVelocity(Vector3 vector3)
    {
        this.rigidbody.velocity = vector3;
    }

    public void Move(float speed)
    {
        if (this.playerInput.Move)
        {
            this.transform.localScale =  new Vector3(this.playerInput.AxisX, 1, 1);
        }

        this.SetVelocityX(speed * this.playerInput.AxisX);
    }


    public  void SetVelocityX(float x)
    {
        this.rigidbody.velocity = new Vector3(x,this.rigidbody.velocity.y);
    }


    public  void SetVelocityY(float y)//输入x 是为了保证跳起来有抛物线效果
    {
        this.rigidbody.velocity = new Vector3(this.rigidbody.velocity.x,y);
    }







    /*private Animator animator;

    private void Awake()
    {
        this.animator = this.GetComponentInChildren<Animator>();
    }



    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            
        }#1#
        if (Keyboard.current.aKey.isPressed || Keyboard.current.dKey.isPressed)
        {
            this.animator.Play("Run");
        }
        else
        {
            this.animator.Play("Idle");
        }

    }*/




}
