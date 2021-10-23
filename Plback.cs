using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plback : MonoBehaviour
{
    public float Dup;

    public float Dright;

    private float targetDup;

    private float targetDRight;

    private float vtargetDup;
    
    private float vtargetDRight;

    private Animator animator;

    private Vector3 Dvec;

    private float walkSpeed = 1.4f;

    public GameObject player;

    private Rigidbody rb;

    private Vector3 rbDir;

    private bool run;
    void Start()
    {
        this.animator = this.player.GetComponent<Animator>();
        this.rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        this.run = Input.GetKey(KeyCode.Space);
        this.targetDup = (Input.GetKey("w") ? 1.0f : 0) 
                         - (Input.GetKey("s") ? 1.0f : 0);
        
        this.targetDRight = (Input.GetKey("d") ? 1.0f : 0) 
                            - (Input.GetKey("a") ? 1.0f : 0);

        this.Dup = Mathf.SmoothDamp(this.Dup, this.targetDup,ref this.vtargetDup, 0.1f);
        
        this.Dright = Mathf.SmoothDamp(this.Dright, this.targetDRight,ref this.vtargetDRight, 0.1f);

        float mag  = Mathf.Sqrt(this.Dup * this.Dup + this.Dright * this.Dright);
        float magSpeed = this.run ? 2.0f : 1.0f;

        
        float targetAnSpeed = this.run ? 2.0f : 1.0f;
        float lerpForward = 
            Mathf.Lerp(this.animator.GetFloat("forward"), targetAnSpeed, 0.5f);
        this.animator.SetFloat("forward",mag * lerpForward);
        this.Dvec = this.Dright * this.transform.right + this.Dup * this.transform.forward;
        if (mag > 0.1f)
        {
            //相当于方向
            Vector3 targetForward = Vector3.Slerp(this.player.transform.forward, this.Dvec, 0.3f);
            this.player.transform.forward = targetForward;
        }
        Debug.Log(mag);
        

        //方向已定 直接走就行
        this.rbDir = mag * this.player.transform.forward * this.walkSpeed * magSpeed;
    }

    private void FixedUpdate()
    {
        this.rb.position += this.rbDir * Time.fixedDeltaTime;
    }
}
