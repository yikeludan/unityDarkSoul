using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Chacter
{

 
    private Rigidbody2D _rigidbody2D;

    public StatsBarHUD statsBarHUD;

    [SerializeField]
    public InputSys input;

    private Vector2 vec2;

    private float accTime = 4f;

    private float decTime = 4f;

    private Coroutine acCor;

    private Coroutine decCor;

    public GameObject bullet;
    
    public GameObject bullet1;
    
    public GameObject bullet2;


    public Transform muzzle;
    
    public Transform muzzleTop;
    
    public Transform muzzleBottom;

    private WaitForSeconds waitForSeconds;

    private Collider2D _collider2D;

    private float maxRoll = 360f;

    public float currentRoll;

    private float rollSpeed = 360f;

    private bool isRoll = false;

    private Vector3 dogeScale = new Vector3(0.2f, 0.2f, 0.2f);

    private float dogeDuration;

    private float t1 = 0;
    private float t2 = 0;

    
    [Header("---health-----")]
    public bool isReHealth = true;
    public float reHealthTime;
    public float reHealthPrec;
    private WaitForSeconds waitHealthReTime;

    private Coroutine reHealthTempCor;

    public AnimationCurve animationCurve;
    
    private float timer = 0;


    [Range(0,2)]
    public int weaponPower = 0;

    protected  override void OnEnable()
    {
        base.OnEnable();
        this.input.onMove += Move;
        this.input.onStopMove += StopMove;
        this.input.onFire += onFire;
        this.input.stopFire += stopFire;
        this.input.onDoge += onDoge;

    }

    private void OnDisable()
    {
        
        this.input.onMove -= Move;
        this.input.onStopMove -= StopMove;
        this.input.onFire -= onFire;
        this.input.stopFire -= stopFire;
        this.input.onDoge -= onDoge;

    }

    private void Awake()
    {
        this._rigidbody2D = this.GetComponent<Rigidbody2D>();
        this._collider2D = this.GetComponent<Collider2D>();
        this.dogeDuration = this.maxRoll / rollSpeed;
    }

    private void Start()
    {
        this._rigidbody2D.gravityScale = 0f;
        this.input.EnableGamePlayInput();
        this.waitForSeconds = new WaitForSeconds(0.12f);
        this.waitHealthReTime = new WaitForSeconds(this.reHealthTime);
        this.statsBarHUD.Initialazle(this.health,this.maxHeath);
    }

    public override void RestHealth(float resHealth)
    {
        base.RestHealth(resHealth); 
        this.statsBarHUD.updateStatus(health,maxHeath);
    }

    public override void Die()
    {
        this.statsBarHUD.updateStatus(0,maxHeath);
        base.Die();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        this.statsBarHUD.updateStatus(health,maxHeath);
        if (gameObject.activeSelf)
        {
            if (this.isReHealth)
            {
                if (this.reHealthTempCor != null)
                {
                    StopCoroutine(this.reHealthTempCor);
                }

                this.reHealthTempCor = StartCoroutine(this.DamageOverTime(this.waitHealthReTime, this.reHealthPrec));
            }
        }
    }


    private void FixedUpdate()
    {
        /*float x = Input.GetAxis("Horizontal");

        float y = Input.GetAxis("Vertical");
        if (x == 0 && y == 0)
        {
            return;
        }
        this.vec2 = new Vector2(x,y);
        print(x+","+y);

        

        this._rigidbody2D.velocity = this.vec2 * 280 * Time.fixedDeltaTime;*/
    }

     void Move(Vector2 vec2)
     {
        // this._rigidbody2D.velocity = vec2 * 6;
        if (acCor != null)
        {
            StopCoroutine(this.acCor);
        }

        Quaternion qu = Quaternion.AngleAxis(28 * vec2.y,Vector3.right);
        this.acCor = StartCoroutine(acMove(this.accTime,vec2.normalized * 6,qu));
        StartCoroutine(MoveLimit());
        
     }
   
   void StopMove()
   {
       
        //this._rigidbody2D.velocity = Vector2.zero;
        if (acCor != null)
        {
            StopCoroutine(this.acCor);
        }
        this.acCor = StartCoroutine(acMove(this.decTime,Vector2.zero,Quaternion.identity));
        StopCoroutine(MoveLimit());
   }

   void onFire()
   {
       

       StartCoroutine("buildBullet");
   }
   
   void onDoge()
   {
       if (this.isRoll|| !PlayerEnery.instance.isEnougth(25))
       {
           return;
       }
       StartCoroutine(nameof(onDogeCoroutine));
   }

   IEnumerator onDogeCoroutine()
   {
       this.isRoll = true;
       PlayerEnery.instance.Use(25);
       this._collider2D.isTrigger = true;
       this.currentRoll = 0f;
       var scale = this.transform.localScale;
       float value = 0;
       while (this.currentRoll<this.maxRoll)
       {
           if (value >= 1)
           {
               this.timer = 0;
           }
           this.timer += Time.deltaTime;
          

          value  = this.animationCurve.Evaluate(this.timer);
          Vector3 dirScale = new Vector3(value, value, value);
          print("value = "+value); 
           
           this.currentRoll += this.rollSpeed * Time.deltaTime;
           this.transform.rotation = Quaternion.AngleAxis(this.currentRoll,Vector3.right);
           this.transform.localScale = Vector3.Lerp(this.transform.localScale,dirScale,this.timer);
           /*this.transform.localScale = BezierCurve.QuadraticPoint(Vector3.one, Vector3.one, this.dogeScale,
               this.currentRoll / this.maxRoll);*/
           
           
           /*
           if (this.currentRoll < this.maxRoll / 2f)
           {
               scale.x = Mathf.Clamp(scale.x - Time.deltaTime / this.dogeDuration, this.dogeScale.x,1f);
               scale.y = Mathf.Clamp(scale.y - Time.deltaTime / this.dogeDuration, this.dogeScale.y,1f);
               scale.z = Mathf.Clamp(scale.z - Time.deltaTime / this.dogeDuration, this.dogeScale.z,1f);
               
              
               
               /*this.t1 += Time.deltaTime / this.dogeDuration;
               this.transform.localScale = Vector3.Lerp(this.transform.localScale, this.dogeScale, t1);
               #1#
               


               //scale -= (Time.deltaTime / this.dogeDuration) * Vector3.one;
           }
           else
           {
             
               scale.x = Mathf.Clamp(scale.x + Time.deltaTime / this.dogeDuration, this.dogeScale.x,1f);
               scale.y = Mathf.Clamp(scale.y + Time.deltaTime / this.dogeDuration, this.dogeScale.y,1f);
               scale.z = Mathf.Clamp(scale.z + Time.deltaTime / this.dogeDuration, this.dogeScale.z,1f);
              
              
               /*
               this.t2 += Time.deltaTime / this.dogeDuration;
               this.transform.localScale = Vector3.Lerp(this.transform.localScale, Vector3.one, t2);
               #1#


              // scale += (Time.deltaTime / this.dogeDuration) * Vector3.one;
           }
           this.transform.localScale = scale;*/

           yield return null;
       }
       
       
       this._collider2D.isTrigger = false;
       this.isRoll = false;
   }

   void stopFire()
   {
       StopCoroutine("buildBullet");
   }

   IEnumerator buildBullet()
   {
       while (true)
       {
           /*switch (this.weaponPower)
           {
               case 0:
                   Instantiate(this.bullet, this.muzzle.transform.position, Quaternion.identity);
                   break;
               case 1:
                   Instantiate(this.bullet1, this.muzzleTop.transform.position, Quaternion.identity);
                   Instantiate(this.bullet2, this.muzzleBottom.transform.position, Quaternion.identity);
                   break;
               case 2:
                   Instantiate(this.bullet, this.muzzle.transform.position, Quaternion.identity);
                   Instantiate(this.bullet1, this.muzzleTop.transform.position, Quaternion.identity);
                   Instantiate(this.bullet2, this.muzzleBottom.transform.position, Quaternion.identity);
                   break;
               case 3:
                   break;
               case 4:
                   break;
           }*/
           
           
           switch (this.weaponPower)
           {
               case 0:
                   PoolManager.Release(this.bullet, this.muzzle.transform.position,this.transform.rotation);
                   break;
               case 1:
                   PoolManager.Release(this.bullet1, this.muzzleTop.transform.position,this.transform.rotation);
                   PoolManager.Release(this.bullet2, this.muzzleBottom.transform.position,this.transform.rotation);

                   break;
               case 2:
                   PoolManager.Release(this.bullet, this.muzzle.transform.position,this.transform.rotation);
                   PoolManager.Release(this.bullet1, this.muzzleTop.transform.position,this.transform.rotation);
                   PoolManager.Release(this.bullet2, this.muzzleBottom.transform.position,this.transform.rotation);

                   break;
               case 3:
                   break;
               case 4:
                   break;
           }
           yield return this.waitForSeconds;
       }
   }

   IEnumerator MoveLimit()
   {
       while (true)
       {
           this.transform.position = 
               ViewPort.instance.PlayMovePostion(this.transform.position);
           yield return null;
       }
   }


   IEnumerator acMove(float time,Vector2 vector2,Quaternion moveRoation)
   {
       float t = 0;
       while (t < 1f)
       {
           t += Time.fixedDeltaTime / time;
           this._rigidbody2D.velocity = Vector2.Lerp(this._rigidbody2D.velocity,vector2,t);
           this.transform.rotation = 
               Quaternion.Lerp(this.transform.rotation, moveRoation, t);
           yield return null;
       }
       
   }
   
   IEnumerator decMove(Vector2 vector2)
   {
       float t = 0;
       while (t < this.decTime)
       {
           t += Time.fixedDeltaTime / this.decTime;
           this._rigidbody2D.velocity = Vector2.Lerp(this._rigidbody2D.velocity,vector2,t/decTime);
           yield return null;
       }
   }
   
   
   
}
