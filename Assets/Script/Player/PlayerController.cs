using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour, IDamageable
{
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected Animator anim;

    protected float horizontalmove_float;
    protected float horizontalmove_int;
    protected float verticalmove_int;

    public float maxhealth;
    public float currenthealth;
    public bool isdead;

    [Header("跳跃相关")]
    public bool isground;
    public bool isjump;
    public int jumpcount;
    public float jumpforce;
    public Transform groundcheck;
    public LayerMask groundlayer;
    public float checkradius;
    public bool canjump;
    public GameObject doubleJumpEffectPrefab;

    [Header("移动相关")]
    public float normalspeed = 10;
    public float speed = 10;
    public bool touchcheck = false;
    public float touchchecktime = 0;
    public float speedup = 6;
    private float releaseAtime;
    private float releaseDtime;
    private float pressAtime;
    private float pressDtime;
    protected bool isRun; //是否双击跑步

    [Header("冲刺相关")]
    protected float shadowcd = 0;
    public float dashTime;
    protected float dashTimeleft;
    protected float lastDash = -10f;
    public float dashCoolDown;
    public float dashspeed;
    protected bool isDash;
    public GameObject shadowPrefab;
    public GameObject dashEffectprefab;
    public GameObject dashDustPrefab;

    // Update is called once per frame
    public virtual void OnEnable()
    {
        currenthealth = maxhealth;
    }

    public virtual void Start(){
        //MMMMrD修改：向GameManager注册
        GameManager.Instance.RegisterPlayer(this);
    }

    public virtual void Update()
    {
        horizontalmove_float = Input.GetAxis("Horizontal");
        horizontalmove_int = Input.GetAxisRaw("Horizontal");
        verticalmove_int = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Jump") && jumpcount > 0)
        {
            canjump = true;
        }
        ReadyDash();
        DoubleTouch();
    }

    protected virtual void FixedUpdate()
    {
        Dash();
        if (isDash)
            return;
        Movement();
        Jump();
    }

    protected void DoubleTouch() // 双击跑步
    {
        if (Input.GetKeyUp(KeyCode.A))  // 第一次按下
        {
            releaseAtime = Time.time;
        }

        if (Input.GetKeyUp(KeyCode.D))  // 第一次按下
        {
            releaseDtime = Time.time;
        }



        if (Input.GetKey(KeyCode.A))
        {
            if (Input.GetKeyDown(KeyCode.A))
                pressAtime = Time.time;

            if (pressAtime - releaseAtime <= 0.08f)
            {
                isRun = true;
                speed = normalspeed + speedup;
                shadowcd += Time.deltaTime;
                if (shadowcd >= 0.1f)
                {
                    ObjectPool.Instance.GetObject(shadowPrefab);
                    shadowcd = 0;
                }
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (Input.GetKeyDown(KeyCode.D))
                pressDtime = Time.time;
            if (pressDtime - releaseDtime <= 0.08f)
            {
                isRun = true;
                speed = normalspeed + speedup;
                shadowcd += Time.deltaTime;
                if (shadowcd >= 0.1f)
                {
                    ObjectPool.Instance.GetObject(shadowPrefab);
                    shadowcd = 0;
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))  // 重置速度
        {
            isRun = false;
            speed = normalspeed;
            shadowcd = 0;
        }
    }



    public virtual void Movement()
    {
        if (rb.bodyType == RigidbodyType2D.Dynamic)
            rb.velocity = new Vector2(horizontalmove_float * speed, rb.velocity.y);

        if (horizontalmove_int != 0)
            transform.localScale = new Vector3(horizontalmove_int, 1, 1);
        if (horizontalmove_float != 0)
        {
            anim.SetBool("run", true);
        }
        else
        {
            anim.SetBool("run", false);
        }
    }

    protected void Jump()
    {
        isground = Physics2D.OverlapCircle(groundcheck.position, checkradius, groundlayer);

        if (canjump && rb.bodyType == RigidbodyType2D.Dynamic)
        {
            if (jumpcount == 1)
            {
                GameObject doubleJumpEffect = ObjectPool.Instance.GetObject(doubleJumpEffectPrefab);
                doubleJumpEffect.transform.position = transform.position + new Vector3(0, -1.2f, 0);


            }
            jumpcount--;

            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
            //rb.velocity = new Vector2(rb.velocity.x, 0);
            //rb.velocity += Vector2.up * jumpforce;
            canjump = false;
        }
        if (isground && rb.velocity.y == 0)
        {
            jumpcount = 2;
            anim.SetBool("fall", false);
            anim.SetBool("jump", false);
        }
        else if (rb.velocity.y > 0 && !isground)
        {
            anim.SetBool("jump", true);
            anim.SetBool("fall", false);
        }
        else if (rb.velocity.y < 0 && !isground)
        {
            anim.SetBool("fall", true);
            anim.SetBool("jump", false);
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireSphere(groundcheck.position, checkradius);
    //}

    private void ReadyDash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (Time.time >= (lastDash + dashCoolDown))
            {
                isDash = true;
                dashTimeleft = dashTime;
                lastDash = Time.time;
                if (!isground)
                {
                    GameObject dashEffect = ObjectPool.Instance.GetObject(dashEffectprefab);
                    dashEffect.transform.position = transform.position + new Vector3(horizontalmove_int * -0.5f, -0.35f, 0);
                    dashEffect.transform.localScale = new Vector3(horizontalmove_int, 1, 1);
                }
                else
                {
                    GameObject dashDust = ObjectPool.Instance.GetObject(dashDustPrefab);
                    dashDust.transform.position = transform.position + new Vector3(horizontalmove_int * 0.2f, -0.06f, 0);
                    dashDust.transform.localScale = new Vector3(horizontalmove_int, 1, 1);
                }
            }
        }

    }

    protected virtual void Dash()
    {
        if (isDash && rb.bodyType == RigidbodyType2D.Dynamic)
        {
            anim.SetBool("dash", true);
            anim.SetBool("run", false);
            anim.SetBool("fall", false);
            anim.SetBool("jump", false);
            if (dashTimeleft > 0)
            {
                shadowcd += Time.deltaTime;
                rb.velocity = new Vector2(dashspeed * horizontalmove_int, (dashspeed - 20) * verticalmove_int);
                dashTimeleft -= Time.deltaTime;
                if (shadowcd >= 0.02f)
                {
                    ObjectPool.Instance.GetObject(shadowPrefab);
                    shadowcd = 0;
                }
            }
            if (dashTimeleft <= 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                isDash = false;
                anim.SetBool("dash", false);
            }
        }
    }

    public void GetHit(float damage)
    {
        anim.SetTrigger("hit");
        currenthealth -= damage;
        if (currenthealth < 1)
        {
            currenthealth = 0;
            isdead = true;
        }
    }

    public virtual void Attack()
    {

    }
}
