using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Attackstate2 : YasuoBaseState
{
    private enum Attackstate2_Enum
    {
        Attack, FlyAttack, RushAttack
    }
    private Attackstate2_Enum _attackstate2 = Attackstate2_Enum.FlyAttack;
    private Vector3 _currentplayerposition;
    private float _flyattackcd = 2f;
    private float _flyattacktime = 0.5f;
    private int _flyattackcount2 = 1;


    private int _attackcount2;
    private float 大招蓄力 = 0.07f;
    private Vector3 movePos;
    private Vector3 _groundposition;

    //private int swordattackchoose = 1;
    //private float _swordtime = 6;
    private int _rushcount = 1;
    private float _rushtime = 1.5f;
    private Vector3 _rushpoint;
    private int _rushnum = 0;
    public Attackstate2(YasuoControl yasuo)
    {
        this.Yasuo = yasuo;
    }

    public override void EnterState()
    {
        _attackcount2 = 1;
        if (Yasuo.angel != null)
            Yasuo.angel.Play("out");
    }

    public override void OnUpdate()
    {
        //_swordtime -= Time.deltaTime;
        //if (_swordtime <= 0)
        //{
        //    if (swordattackchoose == 1)
        //    {
        //Yasuo.Sword1.GetComponent<YasuoSword>().StartAttack1();
        //Yasuo.Sword2.GetComponent<YasuoSword>().StartAttack1();
        //        swordattackchoose = 2;
        //    }
        //    else
        //    {
        //Yasuo.Sword1.GetComponent<YasuoSword>().StartAttack2();
        //Yasuo.Sword2.GetComponent<YasuoSword>().StartAttack2();
        //        swordattackchoose = 1;
        //    }
        //    _swordtime = 6;
        //}

        Yasuo.StartWind();
        switch (_attackstate2)
        {
            case Attackstate2_Enum.Attack:
                Attack();
                break;
            case Attackstate2_Enum.FlyAttack:
                FlyAttack();
                break;
            case Attackstate2_Enum.RushAttack:
                RushAttack();
                break;
        }
    }

    void Attack()
    {

        Yasuo._Anim.SetBool("run", false);
        switch (_attackcount2)
        {
            case 1:
                if (!Yasuo._Anim.GetCurrentAnimatorStateInfo(0).IsName("attack_2") && Yasuo._distance < 4.5f)
                    Yasuo._Anim.Play("attack_2");
                else
                {
                    Yasuo.MoveToTarget();
                }
                if (Yasuo._Anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && Yasuo._Anim.GetCurrentAnimatorStateInfo(0).IsName("attack_2"))
                {
                    _attackcount2 = 2;
                }
                break;
            case 2:
                if (!Yasuo._Anim.GetCurrentAnimatorStateInfo(0).IsName("吹风") && Yasuo._distance < 4.5f)
                    Yasuo._Anim.Play("吹风");
                else
                {
                    Yasuo.MoveToTarget();
                }

                if (Yasuo._Anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && Yasuo._Anim.GetCurrentAnimatorStateInfo(0).IsName("吹风"))
                    if (Yasuo.可以接大)
                    {
                        _attackcount2 = 3;
                        Yasuo.可以接大 = false;

                    }
                    else if (!Yasuo.可以接大)
                    {
                        _attackcount2 = 4;
                    }
                break;
            case 3:
                大招蓄力 -= Time.deltaTime;
                if (大招蓄力 <= 0)
                {
                    Yasuo._PlayerTR.position = _currentplayerposition;
                    Yasuo.transform.position = _currentplayerposition;
                    Yasuo._Anim.Play("大招");
                }
                else
                {
                    _currentplayerposition = Yasuo._PlayerTR.position;
                }

                if (Yasuo._Anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && Yasuo._Anim.GetCurrentAnimatorStateInfo(0).IsName("大招"))
                {
                    _attackcount2 = 4;
                }

                break;
            case 4:
                _attackcount2 = 1;
                大招蓄力 = 0.07f;
                _attackstate2 = Attackstate2_Enum.RushAttack;
                // Yasuo.TransitionState(YasuoControl.YasuoState_Enum.Idle);
                break;

        }
    }

    void FlyAttack()
    {
        switch (_flyattackcount2)
        {
            case 1:
                Yasuo.Sword1.GetComponent<YasuoSword>().StartAttack1();
                Yasuo.Sword2.GetComponent<YasuoSword>().StartAttack1();
                Yasuo.groundattackcount = 1;
                _groundposition = Yasuo.transform.position;
                movePos = new Vector3(Yasuo.transform.position.x, Random.Range(6, 15));
                Yasuo._Rb.gravityScale = 0;/* 初始重力为3 */
                Yasuo.wing.SetActive(true);
                _flyattackcount2 = 2;
                break;
            case 2:
                _flyattacktime -= Time.deltaTime;
                if (_flyattacktime <= 0)
                {
                    Yasuo.transform.position = Vector2.MoveTowards(Yasuo.transform.position, movePos, 150 * Time.deltaTime);
                    if (Vector3.Distance(Yasuo.transform.position, movePos) < 0.1f)
                    {
                        Yasuo.FireAttack();
                        _flyattackcount2 = 3;
                        _flyattacktime = _flyattackcd;
                        RandomRangeMovePos();
                    }
                }
                break;
            case 3:
                Yasuo.Sword1.GetComponent<YasuoSword>().StartAttack1();
                Yasuo.Sword2.GetComponent<YasuoSword>().StartAttack1();
                _flyattacktime -= Time.deltaTime;
                if (_flyattacktime <= 0)
                {
                    Yasuo.transform.position = Vector2.MoveTowards(Yasuo.transform.position, movePos, 100 * Time.deltaTime);
                    if (Vector3.Distance(Yasuo.transform.position, movePos) < 0.1f)
                    {
                        Yasuo.FireAttack();
                        _flyattackcount2 = 4;
                        _flyattacktime = _flyattackcd;
                        RandomRangeMovePos();
                    }
                }
                break;
            case 4:
                _flyattacktime -= Time.deltaTime;
                if (_flyattacktime <= 0)
                {
                    Yasuo.transform.position = Vector2.MoveTowards(Yasuo.transform.position, movePos, 100 * Time.deltaTime);
                    if (Vector3.Distance(Yasuo.transform.position, movePos) < 0.1f)
                    {
                        Yasuo.FireAttack();
                        _flyattackcount2 = 5;
                        _flyattacktime = _flyattackcd;
                    }
                }
                break;
            case 5:
                _flyattacktime -= Time.deltaTime;
                if (_flyattacktime <= 0)
                {
                    Yasuo.transform.position = Vector2.MoveTowards(Yasuo.transform.position, new Vector2(0, 15), 100 * Time.deltaTime);
                    if (Vector3.Distance(Yasuo.transform.position, new Vector2(0, 15)) < 0.1f)
                    {
                        _flyattackcount2 = 6;
                        _flyattacktime = 0.5f;
                        Yasuo.wing.SetActive(false);

                    }
                }
                break;
            case 6:
                Yasuo.transform.position = Vector2.MoveTowards(Yasuo.transform.position, new Vector2(0, _groundposition.y), 300 * Time.deltaTime);
                if (Vector3.Distance(Yasuo.transform.position, new Vector2(0, _groundposition.y)) < 0.1f)
                {
                    Camera.main.DOShakePosition(0.5f, new Vector3(0, -3, 0));
                    Yasuo.GroundAttack();
                    Yasuo._Rb.gravityScale = 3;
                    _flyattackcount2 = 1;
                    _flyattacktime = 0.5f;
                    Yasuo.TransitionState(YasuoControl.YasuoState_Enum.Idle);
                    _attackstate2 = Attackstate2_Enum.Attack;
                }
                break;
        }
    }

    void RushAttack()
    {
        switch (_rushcount)
        {
            case 1:
                if (_rushnum == 4)
                {
                    _rushcount = 3;
                }
                else
                {
                    Yasuo.Sword1.GetComponent<YasuoSword>().StartAttack2();
                    Yasuo.Sword2.GetComponent<YasuoSword>().StartAttack2();
                    _rushtime -= Time.deltaTime;
                    Yasuo._Anim.SetBool("run", false);
                    Yasuo._Anim.SetBool("idle", true);
                    Yasuo._Anim.Play("rush");
                    if (Yasuo.transform.position.x > 0)
                    {
                        _rushpoint = Yasuo.leftparticle.transform.position;
                        Yasuo.transform.position = Yasuo.rightparticle.transform.position + new Vector3(+1, 0, 0);
                        Yasuo.transform.rotation = Quaternion.Euler(0, 180, 0);
                        Yasuo.rightparticle.SetActive(true);
                    }
                    else
                    {
                        _rushpoint = Yasuo.rightparticle.transform.position;
                        Yasuo.transform.position = Yasuo.leftparticle.transform.position + new Vector3(-1, 0, 0);
                        Yasuo.transform.rotation = Quaternion.Euler(0, 0, 0);
                        Yasuo.leftparticle.SetActive(true);
                    }
                    if (_rushtime <= 0)
                    {
                        Yasuo.RushAttackCheck.SetActive(true);
                        _rushcount = 2;
                    }
                }
                break;
            case 2:
                Yasuo.transform.position = Vector2.MoveTowards(Yasuo.transform.position, _rushpoint, 180 * Time.deltaTime);
                if (Vector3.Distance(Yasuo.transform.position, _rushpoint) < 0.1f)
                {
                    _rushcount = 1;
                    _rushtime = 1.5f;
                    _rushnum++;
                    Yasuo.RushAttackCheck.SetActive(false);
                }
                break;
            case 3:
                Yasuo.TransitionState(YasuoControl.YasuoState_Enum.Idle);
                _rushnum = 0;
                _rushcount = 1;
                _rushtime = 1.5f;
                _attackstate2 = Attackstate2_Enum.FlyAttack;
                break;
        }
    }


    private void RandomRangeMovePos()
    {
        if (Yasuo.transform.position.x > 0)
        {
            movePos = new Vector3(Random.Range(-36, 0), Random.Range(5, 15));
        }
        else
        {
            movePos = new Vector3(Random.Range(0, 36), Random.Range(5, 15));
        }
    }


}
