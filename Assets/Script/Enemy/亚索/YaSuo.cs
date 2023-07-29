using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YaSuo : MonoBehaviour, IDamageable
{

    [Header("三把圣剑")]
    public GameObject Sword1;
    public GameObject Sword2;
    public GameObject Sword3;
    [Space]

    public GameObject line1, line2;
    public GameObject circle;
    public Blood blood;
    public GameObject floatpoint;
    public GameObject blueprefeb, purpleprefeb;
    public GameObject blue, purple;
    public GameObject levelfire;
    public Animator angel;
    public GameObject light_1;
    public GameObject light_2;
    public GameObject light_3;
    public GameObject fire;/* 远程攻击 */
    public GameObject fire_2;/* 远程攻击 */
    public GameObject groundattack1prefeb;
    public GameObject groundattack2prefeb;
    public Vector3 groundpoint;
    public int groundattackcount = 0;
    public GameObject swordattackprefeb1;
    public GameObject swordattackprefeb2;
    public int swordattackcount = 0;
    public GameObject bigskill;
    [HideInInspector] public bool b;/* 只执行一次的控制bool */
    [Range(0, 2000)] public float health;

    public State_1 state_1;
    public State_2 state_2;
    public State_3 state_3;
    public GameObject skill;
    public GameObject wind;/* 获取压缩的粒子特效龙卷风 */
    public GameObject attacktips;/* 获取敌人攻击提示头上的感叹号 */

    public GameObject leftparticle;/* 获取从左发射的粒子特效 */
    public GameObject rightparticle;/* 获取从右发射的粒子特效 */
    public GameObject wing;/* 翅膀 */
    public GameObject wingprefeb;
    public bool dropsword;
    [HideInInspector] public Transform player;
    private float cdtime;
    [HideInInspector] public int count_1 = 1;
    [HideInInspector] public int direction;/* 压缩的朝向 为1时朝左 为-1时朝右 */
    [HideInInspector] public Vector2 playerpoint;/* 用于执行一次获取player的坐标，并且保持y为亚索的y */

    public GameObject windspace;/* 亚索风场 */
    public GameObject windspaceright;/* 亚索风场右 */

    [HideInInspector] public float cd_3 = 10f;/* 压缩冲刺cd */
    [HideInInspector] public int choose = 1;/* 切换攻击方式 */


    [HideInInspector] public float dist;/* 判断和玩家的攻击距离 */

    [HideInInspector] public Animator anim;
    private bool isdead;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public YaSuoState currentstate;
    [HideInInspector] public bool canmove = false;/* 状态3中开场动画控制 */
    void Start()
    {
        if (Sword1.activeInHierarchy == false)
            Sword1.SetActive(true);
        blood.curHP = (int)health;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        state_1 = new State_1();
        state_2 = new State_2();
        state_3 = new State_3();
        TransitionState(state_1);
    }

    void Update()
    {
        if (isdead)
            return;
        dist = Vector3.Distance(player.position, transform.position);
        SwitchState();
        currentstate.OnUpdateState(this);
        FilpDirection();
    }




    public void TransitionState(YaSuoState state)
    {
        currentstate = state;
        currentstate.EnterState(this);
    }

    public void SwitchState()/* 根据生命值改变状态 */
    {
        if (health < 1500 && health >= 900)
        {
            if (Sword2.activeInHierarchy == false)
                Sword2.SetActive(true);
            TransitionState(state_2);
        }
        if (health < 900)
        {
            if (Sword3.activeInHierarchy == false)
                Sword3.SetActive(true);
            TransitionState(state_3);
        }

    }


    public void FilpDirection()/* 改变方向 */
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("idle") || anim.GetCurrentAnimatorStateInfo(0).IsName("run"))
        {
            if (transform.position.x > player.position.x)/* 朝左 */
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
        count_1++;
        GameObject light1 = ObjectPool.Instance.GetObject(light_1);
        light1.transform.position = new Vector2(transform.position.x - direction * 3.5f, transform.position.y - 0.5f);
        light1.transform.rotation = Quaternion.Euler(0, 90 + direction * 90, 0);
    }

    public void Light_2()/* 攻击2的特效 用事件帧实现 */
    {
        count_1 = 2;
        GameObject light2 = ObjectPool.Instance.GetObject(light_2);
        light2.transform.position = new Vector2(transform.position.x - direction * 2.5f, transform.position.y - 0.5f);
        light2.transform.rotation = Quaternion.Euler(0, 90 + direction * 90, 0);

    }

    public void Light_3()/* 攻击3的特效 用事件帧实现 */
    {
        count_1 = 3;
        GameObject light3 = ObjectPool.Instance.GetObject(light_3);
        light3.transform.position = new Vector2(transform.position.x - direction * 2.2f, transform.position.y + 0.2f);
        light3.transform.rotation = Quaternion.Euler(0, 90 + direction * 90, 0);
    }

    public void BigSkill()/* 大招特效 事件帧实现 */
    {
        GameObject _bigskill = ObjectPool.Instance.GetObject(bigskill);
        _bigskill.transform.position = player.position;
    }

    // public void Wind()/* 攻击特效吹风 用事件帧实现 */
    // {
    //     GameObject _wind = ObjectPool.Instance.GetObject(wind);
    //     if (direction == 1)
    //     {
    //         _wind.transform.position = new Vector2(transform.position.x - 3f, transform.position.y + 2.5f);
    //         _wind.transform.rotation = Quaternion.Euler(0, 180, 0);
    //         count_1++;
    //     }

    //     if (direction == -1)
    //     {
    //         _wind.transform.position = new Vector2(transform.position.x + 3f, transform.position.y + 2.5f);
    //         _wind.transform.rotation = Quaternion.Euler(0, 0, 0);
    //         count_1++;
    //     }
    //     Invoke("ResetCount", 1.5f);
    // }

    // void ResetCount()
    // {
    //     count_1 = 1;
    // }

    void FireAttack()/* 远程攻击波 */
    {
        GameObject fireattack = ObjectPool.Instance.GetObject(fire);
        fireattack.transform.position = transform.position;
        Vector2 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;/* 计算角度 */
        fireattack.transform.rotation = Quaternion.AngleAxis(Random.Range(260, 280) + angle, Vector3.forward);
    }

    void FireAttack2()/* 花里胡哨的远程攻击 */
    {
        for (int i = 0; i < 6; i++)
        {
            GameObject _fire_2 = ObjectPool.Instance.GetObject(fire_2);
            _fire_2.transform.position = transform.position;
            _fire_2.transform.up = Quaternion.AngleAxis(15 * (i - 4), Vector3.forward) * (player.position - new Vector3(transform.position.x, transform.position.y, transform.position.z)).normalized;
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

    public void GroundAttack()/* 三阶段地刺攻击 */
    {
        if (groundattackcount < 12)
        {
            GameObject _attacktip = ObjectPool.Instance.GetObject(attacktips);
            groundpoint = new Vector2(player.position.x, -19.75f);
            _attacktip.transform.position = groundpoint;
            groundattackcount++;
            StartCoroutine(Ground());
        }

    }

    IEnumerator Ground()
    {
        yield return new WaitForSeconds(0.3f);
        GameObject groundattack1 = ObjectPool.Instance.GetObject(groundattack1prefeb);
        groundattack1.transform.position = new Vector2(groundpoint.x, -9.4f);
        yield return new WaitForSeconds(0.1f);
        GameObject groundattack2 = ObjectPool.Instance.GetObject(groundattack2prefeb);
        groundattack2.transform.position = new Vector2(groundpoint.x, -9.4f);
        yield return new WaitForSeconds(0.12f);
        GroundAttack();
    }





    public void AttackTips()/* 攻击提示 用动画事件帧实现 */
    {
        GameObject tips = ObjectPool.Instance.GetObject(attacktips);
        tips.transform.position = new Vector2(transform.position.x, transform.position.y + 2);
    }

    void Rush_Move()/* 突进 */
    {
        OnceWork();
        if (dist > 5)
            transform.position = Vector2.MoveTowards(transform.position, playerpoint, 200 * Time.deltaTime);
        cd_3 = 10f;
        choose = 1;
    }

    public void OnceWork()/* 只执行一次 */
    {
        if (b)
        {
            playerpoint = player.transform.position;
            playerpoint.y = transform.position.y;
            b = false;
        }
    }

    public void GetHit(float damage)/* 受伤 */
    {
        blood.GetDamage((int)damage);
        health -= damage;
        GameObject point = ObjectPool.Instance.GetObject(floatpoint);
        point.transform.position = transform.position;
        point.transform.GetChild(0).GetComponent<TextMesh>().text = damage.ToString();
        if (health < 1)
        {
            if (!dropsword)
            {
                GameObject wingitem = ObjectPool.Instance.GetObject(wingprefeb);
                wingitem.transform.position = transform.position;
                dropsword = true;
            }
            Destroy(gameObject);

            health = 0;
            isdead = true;
        }
    }

    public void WingOpen()
    {
        wing.SetActive(true);
    }

    public void State3CanMove()
    {
        canmove = true;
    }
}
