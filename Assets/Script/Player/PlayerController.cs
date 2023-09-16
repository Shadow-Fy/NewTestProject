using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour, IDamageable
{
    [Header("�����M��")]
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected Animator anim;
    [SerializeField] protected SpriteRenderer spriteRenderer;

    protected float horizontalmove_float;
    protected float horizontalmove_int;
    protected float verticalmove_int;

    public PlayerModel playerModel = new PlayerModel();
    public bool isdead;
    public float hurtCD = 0.4f;
    private float hurtTime;

    [Header("��Ծ���")]
    public bool isground;
    public bool isjump;
    public int jumpcount;
    public float jumpforce;
    public Transform groundcheck;
    public LayerMask groundlayer;
    public float checkradius;
    public bool canjump;
    public GameObject doubleJumpEffectPrefab;
    public float fallMultiplier = 4f;
    public float lowJumpMultiplier = 7f;

    [Header("�ƶ����")]
    public float normalspeed = 10;
    public float speed = 10;
    public bool touchcheck = false;
    public float touchchecktime = 0;
    public float speedup = 6;
    private float releaseAtime;
    private float releaseDtime;
    private float pressAtime;
    private float pressDtime;
    protected bool isRun; //�Ƿ�˫���ܲ�

    [Header("������")]
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
    // private ScreenFlash sf;
    // public GameObject sfc;


    // Update is called once per frame
    public virtual void OnEnable()
    {

    }

    public virtual void Start()
    {
        hurtTime = hurtCD;

        spriteRenderer = GetComponent<SpriteRenderer>();

        GameManager.Instance.RegisterPlayer(this);

        playerModel.InitPlayerHealth();
        EventControl.Instance.InitEvent(playerModel.CurrentHealth, playerModel.MaxHealth);
    }

    public virtual void Update()
    {
        hurtTime -= Time.deltaTime;
        horizontalmove_float = Input.GetAxis("Horizontal");
        horizontalmove_int = Input.GetAxisRaw("Horizontal");
        verticalmove_int = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Jump") && jumpcount > 0)
        {
            canjump = true;
        }
        ReadyDash();
        DoubleTouch();

        if (!isDash)
            if (rb.velocity.y < 0)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
            else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }
    }

    protected virtual void FixedUpdate()
    {
        Dash();
        if (isDash)
            return;
        Movement();
        Jump();
    }

    protected void DoubleTouch() // ˫���ܲ�
    {
        if (Input.GetKeyUp(KeyCode.A))  // ��һ�ΰ���
        {
            releaseAtime = Time.time;
        }

        if (Input.GetKeyUp(KeyCode.D))  // ��һ�ΰ���
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
                    //MMMMrD�޸ģ��������ʹ��Shadow�ķ�ʽ
                    GameObject shadowSprite = ObjectPool.Instance.GetObjectButNotActive(shadowPrefab);
                    shadowSprite.GetComponent<ShadowSprite>().Init(transform, spriteRenderer);
                    shadowSprite.SetActive(true);
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
                    GameObject shadowSprite = ObjectPool.Instance.GetObjectButNotActive(shadowPrefab);
                    shadowSprite.GetComponent<ShadowSprite>().Init(transform, spriteRenderer);
                    shadowSprite.SetActive(true);

                    shadowcd = 0;
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))  // �����ٶ�
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
            // Debug.Log(rb.velocity);
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
                    GameObject shadow = ObjectPool.Instance.GetObjectButNotActive(shadowPrefab);
                    shadow.GetComponent<ShadowSprite>().Init(transform, spriteRenderer);
                    shadow.SetActive(true);
                    shadowcd = 0;
                }
            }
            if (dashTimeleft <= 0)
            {
                isDash = false;
                rb.velocity = new Vector2(rb.velocity.x, 0);
                anim.SetBool("dash", false);
            }
        }
    }

    public void GetHit(float damage)
    {
        if (hurtTime <= 0)
        {
            playerModel.ChangePlayerHealth(damage);
            EventControl.Instance.GetHitEvent(playerModel.CurrentHealth, playerModel.MaxHealth);
            // currenthealth -= damage;
            hurtTime = hurtCD;
            if (playerModel.IsDead())
            {
                isdead = true;
            }
        }
    }

    public virtual void Attack()
    {

    }
}
