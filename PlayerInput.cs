using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float Dup;

    public float Dright;

    private float targetDup;

    private float targetDRight;

    private float vtargetDup;
    
    private float vtargetDRight;

    public float Jup;

    public float Jright;
    
    

    [HideInInspector] 
    public float mag;

    [HideInInspector] 
    public float magSpeed;

    [HideInInspector] 
    public float targetAnSpeed;

    
    [HideInInspector] 
    public Vector3 Dvec;
    
    public float walkSpeed;
    public bool inputEnable;

    private bool run;
    [HideInInspector]
    public bool jump;

    private bool lastJump;
    [HideInInspector]
    public bool attack;
    private bool lastAttack;
    void Start() 
    {
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");


        this.Jup = (Input.GetKey(KeyCode.UpArrow) ? 1.0f : 0f) - 
                   (Input.GetKey(KeyCode.DownArrow) ? 1.0f : 0);
        this.Jright = (Input.GetKey(KeyCode.RightArrow) ? 1.0f : 0f) - 
                   (Input.GetKey(KeyCode.LeftArrow) ? 1.0f : 0);
        /*this.Jup = y;
        this.Jright = x;*/
        
        
        this.run = Input.GetKey(KeyCode.LeftShift);
        this.targetDup = (Input.GetKey("w") ? 1.0f : 0) 
                         - (Input.GetKey("s") ? 1.0f : 0);
        
        this.targetDRight = (Input.GetKey("d") ? 1.0f : 0) 
                            - (Input.GetKey("a") ? 1.0f : 0);

        if (this.inputEnable == false)
        {
            this.targetDup = 0;
            this.targetDRight = 0;
        }

        this.Dup = Mathf.SmoothDamp(this.Dup, this.targetDup,ref this.vtargetDup, 0.1f);
        this.Dright = Mathf.SmoothDamp(this.Dright, this.targetDRight,ref this.vtargetDRight, 0.1f);

        Vector2 tempVec2 = this.SqaurToCircle(new Vector2(this.Dright, this.Dup));

        float Dup2 = tempVec2.y;
        float Dright2 = tempVec2.x;
        this.mag  = Mathf.Sqrt(Dup2 * Dup2 + Dright2* Dright2);
        this.magSpeed = this.run ? 2.0f : 1.0f;
        
        this.targetAnSpeed = this.run ? 2.0f : 1.0f;
        this.Dvec = Dright2 * this.transform.right + Dup2 * this.transform.forward;

        bool newJump = Input.GetKey(KeyCode.Space);
        if (newJump != this.lastJump && newJump == true)
        {
            this.jump = true;
        }
        else
        {
            this.jump = false;
        }
        this.lastJump = newJump;
        
        
        
        bool newAttack = Input.GetKey(KeyCode.C);
        if (newAttack != this.lastAttack && newAttack == true)
        {
            this.attack = true;
        }
        else
        {
            this.attack = false;
        }
        this.lastAttack = newAttack;
    }

  
    /**
     * 方形坐标转成圆形坐标公式 保证英雄斜向前进也不会跑起来
     */
    private Vector2 SqaurToCircle(Vector2 input)
    {
        Vector2 output = Vector2.zero;
        output.x = input.x * Mathf.Sqrt(1 - (input.y * input.y) / 2.0f);
        output.y = input.y * Mathf.Sqrt(1 - (input.x * input.x) / 2.0f);
        return output;
    }

   
}
