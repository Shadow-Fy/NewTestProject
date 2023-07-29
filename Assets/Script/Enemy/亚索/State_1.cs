using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_1 : YaSuoState
{
    private YaSuo Boss;
    private float cd_1 = 0.3f;/* 攻击前等待时间  前摇 */
    [HideInInspector] public float cd_2 = 1f;/* 攻击结束后等待时间  后摇 */
    [HideInInspector] public bool isattack;

    public override void EnterState(YaSuo yasuo)
    {
        Boss = yasuo;
    }

    public override void OnUpdateState(YaSuo yasuo)
    {

        Boss.cd_3 -= Time.deltaTime;
        if (Boss.cd_3 <= 0 && (Boss.anim.GetCurrentAnimatorStateInfo(0).IsName("idle") || Boss.anim.GetCurrentAnimatorStateInfo(0).IsName("run")))
        {
            Boss.choose = 2;
        }


        switch (Boss.choose)
        {
            case 1:
                Movement();
                break;
            case 2:
                Rush();
                Boss.b = true;
                break;
        }
    }

    void Rush()/* 瞬移到左右端朝玩家冲刺 */
    {

        Boss.anim.SetBool("run", false);
        Boss.anim.SetBool("idle", true);
        if (Boss.direction == 1)
        {

            Boss.transform.position = Boss.rightparticle.transform.position + new Vector3(+1, 0, 0);
            Boss.transform.rotation = Quaternion.Euler(0, 180, 0);
            Boss.rightparticle.SetActive(true);

        }
        if (Boss.direction == -1)
        {
            Boss.transform.position = Boss.leftparticle.transform.position + new Vector3(-1, 0, 0);
            Boss.transform.rotation = Quaternion.Euler(0, 0, 0);
            Boss.leftparticle.SetActive(true);
        }
        Boss.anim.Play("rush");
        Boss.Invoke("Rush_Move", 1.25f);
    }

    void  Movement()
    {

        if (Boss.dist > 3f && !isattack)
        {
            Move();
            Boss.anim.SetBool("run", true);
            Boss.anim.SetBool("idle", false);
            cd_1 = 0.3f;
            cd_2 = 0.1f;
        }

        if (Boss.dist <= 4f)
        {
            Boss.anim.SetBool("run", false);
            Boss.anim.SetBool("idle", true);
            Attack();
        }

        if (isattack == true)
            cd_2 -= Time.deltaTime;
        if (cd_2 <= 0)
        {
            isattack = false;
        }
    }

    void Move()
    {
        if (Boss.anim.GetCurrentAnimatorStateInfo(0).IsName("idle") || Boss.anim.GetCurrentAnimatorStateInfo(0).IsName("run"))
            Boss.transform.position = Vector2.MoveTowards(Boss.transform.position, Boss.player.position, 10 * Time.deltaTime);
    }



    void Attack()/* 普通攻击1段 */
    {

        cd_1 -= Time.deltaTime;
        if (cd_1 <= 0 && Boss.count_1 < 3)
        {
            isattack = true;
            Boss.anim.Play("attack_1");
            cd_1 = 1.2f;
        }
        else if (cd_1 <= 0 && Boss.count_1 == 3)
        {
            isattack = true;
            Boss.anim.Play("吹风");
        }

    }
}
