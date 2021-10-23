using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Versioning;
using UnityEngine;

public class ActorController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;
    public GameObject player;
    private Vector3 Dvec;
    private Vector3 rbDir;
    private bool run;
    public float walkSpeed;
    private PlayerInput pi;
    private bool lockPlanrVec = false;

    
    private Vector3 jumpVec3;

    private float lerpTarget;
    void Start()
    {
        this.animator = this.player.GetComponent<Animator>();
        this.rb = this.GetComponent<Rigidbody>();
        this.pi = this.GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        float lerpForward = 
            Mathf.Lerp(this.animator.GetFloat("forward"), pi.targetAnSpeed, 0.5f);
        this.animator.SetFloat("forward", pi.mag * lerpForward);
        if (pi.jump)
        {
            this.animator.SetTrigger("jump");
        }
        
        if (pi.attack)
        {
            this.animator.SetTrigger("attack");
        }

        if (pi.mag > 0.1f)
        {
            //相当于方向
            Vector3 targetForward = Vector3.Slerp(this.player.transform.forward, pi.Dvec, 0.3f);
            this.player.transform.forward = targetForward;
        }

        if (this.lockPlanrVec == false)
        {
            //方向已定 直接走就行
            this.rbDir = pi.mag * this.player.transform.forward * pi.walkSpeed * pi.magSpeed;
        }

        
    }

    private bool CheckAnState(string stateName, string layerName = "Base Layer")
    {
        int layerIndex = this.animator.GetLayerIndex(layerName);
        return this.animator.GetCurrentAnimatorStateInfo(layerIndex).IsName(stateName);
    }


    private void FixedUpdate()
    {
        //this.rb.position += this.rbDir * Time.fixedDeltaTime;
        this.rb.velocity = new Vector3(this.rbDir.x, this.rb.velocity.y, this.rbDir.z) + this.jumpVec3;
        this.jumpVec3 = Vector3.zero; 
    }

    public void onJumpEnter()
    {
        this.pi.inputEnable = false;
        this.lockPlanrVec = true;
        this.jumpVec3 = new Vector3(0, 3, 0);
    }
    public void onJumpExit()
    {
        /*this.pi.inputEnable = true;
        this.lockPlanrVec = false;*/
    }


    public void isGround()
    {
        this.animator.SetBool("isGround",true);
    }
    
    public void isNotGround()
    {
        this.animator.SetBool("isGround",false);
    }

    public void onGroundEnter()
    {
        this.pi.inputEnable = true;
        this.lockPlanrVec = false;
    }

    public void onRollEnter()
    {
        this.pi.inputEnable = false;
        this.lockPlanrVec = true;
        this.jumpVec3 = this.player.transform.forward * 3f;
    }
    
    public void onJadEnter()
    {
        
        this.pi.inputEnable = false;
        this.lockPlanrVec = true;
        this.jumpVec3 = this.player.transform.forward * -40f;
    }

   

    public void OnAttackhAEnter()
    {
        this.pi.inputEnable = false;
        this.lerpTarget = 1.0f;
       

    }
    
    public void OnAttackhAUpdate()
    {
        float currentWeight =  
            this.animator.GetLayerWeight(this.animator.GetLayerIndex("Attack"));
        currentWeight = Mathf.Lerp(currentWeight, this.lerpTarget, 0.1f);
        this.animator.SetLayerWeight(this.animator.GetLayerIndex("Attack"),currentWeight);
    }
    
    
    public void OnAttackIdle()
    {
        this.pi.inputEnable = true;
        this.lerpTarget = 0f;
        //  this.animator.SetLayerWeight(this.animator.GetLayerIndex("Attack"),0);
    }
    
    public void OnAttackIdleUpdate()
    {
        float currentWeight =  
            this.animator.GetLayerWeight(this.animator.GetLayerIndex("Attack"));
        currentWeight = Mathf.Lerp(currentWeight, this.lerpTarget, 0.1f);
        this.animator.SetLayerWeight(this.animator.GetLayerIndex("Attack"),currentWeight);
    }


    
}
