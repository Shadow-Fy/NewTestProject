using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attackstate1 : YasuoBaseState
{
    private enum Attackstate1_Enum
    {
        Rush, Attack
    }
    private Attackstate1_Enum _attackstate1;
    private enum State1
    {
        Attack1, Attack2, 吹风, Rsuh
    }
    private State1 _currentstate;
    private int _attackchoose;
    private Vector3 _PlayerPoint;
    private int _rushcount;
    private float _swordtime = 6;
    private float _rushtime = 1.5f;
    public Attackstate1(YasuoControl yasuo)
    {
        this.Yasuo = yasuo;
    }

    public override void EnterState()
    {

        _rushcount = 1;


        if (Yasuo._distance <= 20)
            _attackstate1 = Attackstate1_Enum.Attack;
        else
        {
            _attackstate1 = Attackstate1_Enum.Rush;
        }
    }

    public override void OnUpdate()
    {
        _swordtime -= Time.deltaTime;
        if (_swordtime <= 0)
        {
            Yasuo.Sword1.GetComponent<YasuoSword>().StartAttack1();
            _swordtime = 6;
        }


        switch (_attackstate1)
        {
            case Attackstate1_Enum.Attack:
                Attack();
                break;
            case Attackstate1_Enum.Rush:
                RushAttack();
                break;
        }

    }

    void Attack()
    {
        Yasuo._Anim.SetBool("run", false);
        switch (_currentstate)
        {

            case State1.Attack1:
                if (!Yasuo._Anim.GetCurrentAnimatorStateInfo(0).IsName("attack_1") && Yasuo._distance < 4.5f)
                    Yasuo._Anim.Play("attack_1");
                else
                {
                    Yasuo.MoveToTarget();
                }
                if (Yasuo._Anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && Yasuo._Anim.GetCurrentAnimatorStateInfo(0).IsName("attack_1"))
                {
                    _currentstate = State1.Attack2;
                }
                break;
            case State1.Attack2:
                if (!Yasuo._Anim.GetCurrentAnimatorStateInfo(0).IsName("attack_2") && Yasuo._distance < 4.5f)
                    Yasuo._Anim.Play("attack_2");
                else
                {
                    Yasuo.MoveToTarget();
                }
                if (Yasuo._Anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && Yasuo._Anim.GetCurrentAnimatorStateInfo(0).IsName("attack_2"))
                {
                    _currentstate = State1.吹风;
                }
                break;
            case State1.吹风:
                if (!Yasuo._Anim.GetCurrentAnimatorStateInfo(0).IsName("吹风") && Yasuo._distance < 4.5f)
                    Yasuo._Anim.Play("吹风");
                else
                {
                    Yasuo.MoveToTarget();
                }
                if (Yasuo._Anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && Yasuo._Anim.GetCurrentAnimatorStateInfo(0).IsName("吹风"))
                {
                    _currentstate = State1.Attack1;
                    Yasuo.TransitionState(YasuoControl.YasuoState_Enum.Idle);
                }
                break;
        }

    }

    void RushAttack()
    {
        switch (_rushcount)
        {
            case 1:
                _rushtime -= Time.deltaTime;
                Yasuo._Anim.SetBool("run", false);
                Yasuo._Anim.SetBool("idle", true);
                if (Yasuo.direction == 1)
                {

                    Yasuo.transform.position = Yasuo.rightparticle.transform.position + new Vector3(+1, 0, 0);
                    Yasuo.transform.rotation = Quaternion.Euler(0, 180, 0);
                    Yasuo.rightparticle.SetActive(true);

                }
                if (Yasuo.direction == -1)
                {
                    Yasuo.transform.position = Yasuo.leftparticle.transform.position + new Vector3(-1, 0, 0);
                    Yasuo.transform.rotation = Quaternion.Euler(0, 0, 0);
                    Yasuo.leftparticle.SetActive(true);
                }
                _PlayerPoint = Yasuo._PlayerTR.position;
                _PlayerPoint.y = Yasuo.transform.position.y;
                Yasuo._Anim.Play("rush");
                if (_rushtime <= 0)
                {
                    _rushcount = 2;
                }
                break;
            case 2:
                Yasuo.transform.position = Vector2.MoveTowards(Yasuo.transform.position, _PlayerPoint, 200 * Time.deltaTime);
                if (Vector3.Distance(Yasuo.transform.position, _PlayerPoint) < 0.1f)
                {
                    Yasuo._Anim.Play("attack_3");
                }
                if (Yasuo._Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.85f && Yasuo._Anim.GetCurrentAnimatorStateInfo(0).IsName("attack_3"))
                {
                    Yasuo.TransitionState(YasuoControl.YasuoState_Enum.Idle);
                    _rushcount = 3;
                    _rushtime = 1.5f;
                }
                break;
        }
    }
}
