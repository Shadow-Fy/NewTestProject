using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YasuoControl : MonoBehaviour, IDamageable
{
    public enum YasuoState_Enum
    {
        Idle, Attackstate1, Attackstate2, Attackstate3
    }
    private Dictionary<YasuoState_Enum, YasuoBaseState> states = new Dictionary<YasuoState_Enum, YasuoBaseState>();

    [Space]
    [Header("圣剑")]
    public GameObject Sword1;
    public GameObject Sword2;
    public GameObject Sword3;

    [Space]
    [Header("二阶段使用的物品")]
    public Transform flyPointLeftUp;   //飞天攻击最高点
    public Transform flyPointRightDown;   //飞天攻击最低点
    public Transform groundPoint;   //地刺攻击地面起点

    [Space]
    [Header("三阶段使用的物品")]
    public GameObject circle;//保护罩
    public GameObject levelfire;
    public GameObject groundattack1prefeb;
    public GameObject groundattack2prefeb;

    public GameObject wing;/* 翅膀 */
    public GameObject wingprefeb;
    [HideInInspector] public bool canmove = false;/* 状态3中开场动画控制 */

    [Space]
    [Header("基础属性道具")]
    public float groundattackcount = 0;
    public GameObject RushAttackCheck;
    private float windcd = 0f;
    private bool rlwind;
    public GameObject attacktips;
    public GameObject wind;
    public bool 可以接大;
    public GameObject 大招;
    public GameObject light_1;
    public GameObject light_2;
    public GameObject light_3;
    public GameObject fire;/* 远程攻击 */
    public GameObject fire_2;/* 远程攻击 */
    public GameObject swordattackprefeb1;
    public GameObject swordattackprefeb2;
    public GameObject floatpoint;
    public GameObject leftparticle;/* 获取从左发射的粒子特效 */
    public GameObject rightparticle;/* 获取从右发射的粒子特效 */
    public GameObject windspace;/* 亚索风场 */
    public GameObject windspaceright;/* 亚索风场右 */
    private Animator _anim;
    public Animator _Anim
    {
        get { return _anim; }
        set { _anim = value; }
    }
    private Rigidbody2D rb;
    public Rigidbody2D _Rb
    {
        get { return rb; }
        set { rb = GetComponent<Rigidbody2D>(); }
    }
    public Blood blood;
    [Range(0, 2000)] public float health;
    [HideInInspector] public float idleTime;
    private int swordattackcount;
    //private bool isdead;


    [Space]
    [Header("其他属性")]
    [HideInInspector] public bool canhurt = false;
    private Transform _playerTR;
    public Transform _PlayerTR
    {
        get { return _playerTR; }
        set { _playerTR = value; }
    }
    public YasuoBaseState currentstate;
    [HideInInspector] public int direction;
    [HideInInspector] public float _distance;
    // public int _attackcount1;

    public bool _first;
    public bool _second;
    public bool _third;
    void Start()
    {
        blood.curHP = (int)health;
        _PlayerTR = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _Anim = GetComponent<Animator>();
        _Rb = GetComponent<Rigidbody2D>();
        _Rb.bodyType = RigidbodyType2D.Dynamic;
        states.Add(YasuoState_Enum.Idle, new Idle(this));
        states.Add(YasuoState_Enum.Attackstate1, new Attackstate1(this));
        states.Add(YasuoState_Enum.Attackstate2, new Attackstate2(this));
        states.Add(YasuoState_Enum.Attackstate3, new Attackstate3(this));
        TransitionState(YasuoState_Enum.Idle);

        if (Sword1.activeInHierarchy == false)
            Sword1.SetActive(true);

    }

    private void Update()
    {
        currentstate.OnUpdate();
        FilpDirection();

        _distance = Vector2.Distance(transform.position, _playerTR.position);


        if (health > 1500 && !_first)
        {
            _first = true;
            TransitionState(YasuoState_Enum.Attackstate1);

        }
        else if (health <= 1500 && health >= 900 && !_second)
        {
            if (Sword2.activeInHierarchy == false)
                Sword2.SetActive(true);
            TransitionState(YasuoState_Enum.Attackstate2);
            _second = true;
        }
        else if (health < 900 && !_third)
        {
            if (Sword3.activeInHierarchy == false)
                Sword3.SetActive(true);
            TransitionState(YasuoState_Enum.Attackstate3);
            _third = true;
        }
    }

    public void TransitionState(YasuoState_Enum type)
    {
        currentstate = states[type];
        currentstate.EnterState();
    }

    public void MoveToTarget()/* 朝玩家移动 */
    {
        if (_Anim.GetCurrentAnimatorStateInfo(0).IsName("idle") || _Anim.GetCurrentAnimatorStateInfo(0).IsName("run"))
        {
            transform.position = Vector2.MoveTowards(transform.position, _PlayerTR.position, 10 * Time.deltaTime);
            _Anim.SetBool("run", true);
        }
    }

    public void IdleAction()
    {
        idleTime -= Time.deltaTime;
        if (idleTime <= 0)
        {
            SwitchStateByHealth();
        }
    }

    public void SwitchStateByHealth()
    {
        if (health > 1500)
        {
            TransitionState(YasuoState_Enum.Attackstate1);
        }
        else if (health < 1500 && health >= 900)
        {
            if (Sword2.activeInHierarchy == false)
                Sword2.SetActive(true);
            TransitionState(YasuoState_Enum.Attackstate2);
        }
        else if (health < 900)
        {
            if (Sword3.activeInHierarchy == false)
                Sword3.SetActive(true);
            TransitionState(YasuoState_Enum.Attackstate3);
        }
    }

    // void Move()
    // {
    //     if (_Anim.GetCurrentAnimatorStateInfo(0).IsName("idle") || _Anim.GetCurrentAnimatorStateInfo(0).IsName("run"))
    //         transform.position = Vector2.MoveTowards(transform.position, _PlayerTR.position, 10 * Time.deltaTime);
    // }

    public void FilpDirection()/* 改变方向 */
    {
        if (_Anim.GetCurrentAnimatorStateInfo(0).IsName("idle") || _Anim.GetCurrentAnimatorStateInfo(0).IsName("run"))
        {
            if (transform.position.x > _PlayerTR.position.x)/* 朝左 */
            {
                direction = 1;
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else/* 朝右 */
            {
                direction = -1;
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }

    public void Light_1()/* 攻击1的特效 用事件帧实现 */
    {
        GameObject light1 = ObjectPool.Instance.GetObject(light_1);
        light1.transform.position = new Vector2(transform.position.x - direction * 3.5f, transform.position.y - 0.5f);
        light1.transform.rotation = Quaternion.Euler(0, 90 + direction * 90, 0);
    }

    public void Light_2()/* 攻击2的特效 用事件帧实现 */
    {
        GameObject light2 = ObjectPool.Instance.GetObject(light_2);
        light2.transform.position = new Vector2(transform.position.x - direction * 2.5f, transform.position.y - 0.5f);
        light2.transform.rotation = Quaternion.Euler(0, 90 + direction * 90, 0);

    }

    public void Light_3()/* 攻击3的特效 用事件帧实现 */
    {
        GameObject light3 = ObjectPool.Instance.GetObject(light_3);
        light3.transform.position = new Vector2(transform.position.x - direction * 2.2f, transform.position.y + 0.2f);
        light3.transform.rotation = Quaternion.Euler(0, 90 + direction * 90, 0);
    }

    public void Wind()/* 攻击特效吹风 用事件帧实现 */
    {
        GameObject _wind = ObjectPool.Instance.GetObject(wind);
        _wind.transform.position = new Vector2(transform.position.x - direction * 3f, transform.position.y + 1.7f);
        _wind.transform.rotation = Quaternion.Euler(0, 90 + direction * 90, 0);
    }

    public void FireAttack()/* 远程攻击波 */
    {
        for (int i = -4; i < 5; i++)
        {
            GameObject fireattack = ObjectPool.Instance.GetObject(fire);
            fireattack.transform.position = transform.position;
            Vector2 direction = _PlayerTR.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;/* 计算角度 */
            fireattack.transform.rotation = Quaternion.AngleAxis(angle + i * 20 + 270, Vector3.forward);
        }
        StartCoroutine("FireAttak_2");
    }

    IEnumerator FireAttak_2()
    {
        yield return new WaitForSeconds(0.3f);
        for (int i = -4; i < 5; i++)
        {
            GameObject fireattack = ObjectPool.Instance.GetObject(fire);
            fireattack.transform.position = transform.position;
            Vector2 direction = _PlayerTR.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;/* 计算角度 */
            fireattack.transform.rotation = Quaternion.AngleAxis(angle + i * 20 + 270, Vector3.forward);
        }
        yield return new WaitForSeconds(0.3f);
        for (int i = -4; i < 5; i++)
        {
            GameObject fireattack = ObjectPool.Instance.GetObject(fire);
            fireattack.transform.position = transform.position;
            Vector2 direction = _PlayerTR.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;/* 计算角度 */
            fireattack.transform.rotation = Quaternion.AngleAxis(angle + i * 20 + 270, Vector3.forward);
        }
        yield return null;
    }

    void FireAttack2()/* 花里胡哨的远程攻击 */
    {
        for (int i = 0; i < 6; i++)
        {
            GameObject _fire_2 = ObjectPool.Instance.GetObject(fire_2);
            _fire_2.transform.position = transform.position;
            _fire_2.transform.up = Quaternion.AngleAxis(15 * (i - 4), Vector3.forward) * (_PlayerTR.position - new Vector3(transform.position.x, transform.position.y, transform.position.z)).normalized;
        }
    }

    public void SwordAttack()/* 三阶段左右6剑齐发 */
    {
        if (swordattackcount == 0)
        {
            for (int i = 0; i < 6; i++)
            {
                GameObject swordattack1 = ObjectPool.Instance.GetObject(swordattackprefeb1);
                swordattack1.transform.position = new Vector2(-33, (float)(-19.35 + i * 4));
                GameObject swordattack2 = ObjectPool.Instance.GetObject(swordattackprefeb2);
                swordattack2.transform.position = new Vector2(33, (float)(-17.35 + i * 4));
            }
            swordattackcount = 1;
        }
        else
        {
            for (int i = 0; i < 6; i++)
            {
                GameObject swordattack1 = ObjectPool.Instance.GetObject(swordattackprefeb1);
                swordattack1.transform.position = new Vector2(-33, (float)(-17.35 + i * 4));
                GameObject swordattack2 = ObjectPool.Instance.GetObject(swordattackprefeb2);
                swordattack2.transform.position = new Vector2(33, (float)(-19.35 + i * 4));
            }
            swordattackcount = 0;
        }
    }


    public void BigSkill()/* 大招特效 事件帧实现 */
    {
        GameObject _bigskill = ObjectPool.Instance.GetObject(大招);
        _bigskill.transform.position = transform.position;
    }

    public void WingOpen()
    {
        wing.SetActive(true);
    }

    public void State3CanMove()
    {
        canmove = true;
    }

    public void StartWind()/* 开启风场 */
    {
        windcd -= Time.deltaTime;
        if (windcd <= 0)
        {
            if (rlwind)
            {
                rlwind = false;
            }
            else if (!rlwind)
            {
                rlwind = true;
            }
            windcd = 10f;
            if (rlwind)
            {
                windspace.SetActive(true);
                windspaceright.SetActive(false);
            }

            if (!rlwind)
            {
                windspace.SetActive(false);
                windspaceright.SetActive(true);
            }
        }
    }

    public void GroundAttack()/* 地刺攻击 */
    {
        if (groundattackcount < 9)
        {

            groundattackcount++;
            StartCoroutine(Ground());
        }

    }

    IEnumerator Ground()
    {
        //白色
        GameObject groundattack1 = ObjectPool.Instance.GetObject(groundattack1prefeb);
        groundattack1.transform.position = new Vector2(transform.position.x - groundattackcount * 3.2f, groundPoint.position.y);
        GameObject groundattack1_2 = ObjectPool.Instance.GetObject(groundattack1prefeb);
        groundattack1_2.transform.position = new Vector2(transform.position.x + groundattackcount * 3.2f, groundPoint.position.y);
        yield return new WaitForSeconds(0.1f);
        //蓝色
        GameObject groundattack2 = ObjectPool.Instance.GetObject(groundattack2prefeb);
        groundattack2.transform.position = new Vector2(transform.position.x - groundattackcount * 3.2f, groundPoint.position.y);
        GameObject groundattack2_2 = ObjectPool.Instance.GetObject(groundattack2prefeb);
        groundattack2_2.transform.position = new Vector2(transform.position.x + groundattackcount * 3.2f, groundPoint.position.y);
        yield return new WaitForSeconds(0.2f);
        GroundAttack();
        yield return null;
    }

    public void AttackTips()/* 攻击提示 用动画事件帧实现 */
    {
        GameObject tips = ObjectPool.Instance.GetObject(attacktips);
        tips.transform.position = new Vector2(transform.position.x, transform.position.y + 2);
    }

    public void GetHit(float damage)
    {
        if (canhurt)
        {
            blood.GetDamage((int)damage);
            health -= (int)damage;
            GameObject point = ObjectPool.Instance.GetObject(floatpoint);
            point.transform.position = transform.position;
            point.transform.GetChild(0).GetComponent<TextMesh>().text = damage.ToString();
            if (health < 1)
            {
                // if (!dropsword)
                // {
                //     GameObject wingitem = ObjectPool.Instance.GetObject(wingprefeb);
                //     wingitem.transform.position = transform.position;
                //     dropsword = true;
                // }
                Destroy(gameObject);

                health = 0;
                //isdead = true;
            }
        }
    }

}
