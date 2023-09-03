using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attackstate3 : YasuoBaseState
{

    private float _flyattackcd = 3f;
    private float _flyattacktime = 0.9f;
    private int _flyattackcount2 = 1;

    private Vector3 movePos;

    public Attackstate3(YasuoControl yasuo)
    {
        this.Yasuo = yasuo;
    }

    public override void EnterState()
    {
        Yasuo._Anim.Play("idle");
        Yasuo.wing.SetActive(true);
        GameObject _wing = ObjectPool.Instance.GetObject(Yasuo.wingprefeb);
        _wing.transform.position = Yasuo.transform.position;


    }

    public override void OnUpdate()
    {
        if (!Yasuo.Sword3.GetComponent<YasuoSword2>().lineAttackBool && !Yasuo.Sword3.GetComponent<YasuoSword2>().rotateAttackBool)
        {
            Yasuo.canhurt = true;
            Yasuo.magicCircle.SetActive(false);
            for (int i = 0; i < Yasuo.cultist.Length; i++)
            {
                Yasuo.cultist[i].SetActive(true);
            }
            FlyAttack();
        }


    }

    void FlyAttack()
    {
        Vector2 mid = new Vector2((Yasuo.flyPointLeftUp.position.x + Yasuo.flyPointRightDown.position.x) / 2, Yasuo.flyPointLeftUp.position.y);
        switch (_flyattackcount2)
        {
            case 1: //找点
                Yasuo._Anim.SetBool("run", false);
                Yasuo._Anim.SetBool("idle", true);
                Yasuo.Sword1.GetComponent<YasuoSword>().StartAttack1();
                Yasuo.Sword2.GetComponent<YasuoSword>().StartAttack1();
                Yasuo.groundattackcount = 1;
                movePos = new Vector3(Yasuo.transform.position.x, Random.Range(Yasuo.flyPointRightDown.position.y, Yasuo.flyPointLeftUp.position.y));
                Yasuo._Rb.gravityScale = 0;/* 初始重力为3 */
                Yasuo.wing.SetActive(true);
                _flyattackcount2 = 2;
                break;
            case 2: //起飞，找下一个点
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
            case 3: //移动到下一个点攻击，找下一个点
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
            case 4: //移动到下一个点攻击
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
            case 5: //飞到中间
                _flyattacktime -= Time.deltaTime;
                if (_flyattacktime <= 0)
                {
                    Yasuo.transform.position = Vector2.MoveTowards(Yasuo.transform.position, mid, 100 * Time.deltaTime);
                    if (Vector3.Distance(Yasuo.transform.position, mid) < 0.1f)
                    {
                        _flyattackcount2 = 1;
                        _flyattacktime = 2f;
                        Yasuo.wing.SetActive(false);
                        Yasuo.Sword1.GetComponent<YasuoSword>().StartAttack2();
                        Yasuo.Sword2.GetComponent<YasuoSword>().StartAttack2();
                    }
                }
                break;
        }
    }

    private void RandomRangeMovePos()
    {
        float mid = (Yasuo.flyPointLeftUp.position.x + Yasuo.flyPointRightDown.position.x) / 2.0f;
        if (Yasuo.transform.position.x > mid)
        {
            movePos = new Vector3(Random.Range(Yasuo.flyPointLeftUp.position.x, mid), Random.Range(Yasuo.flyPointLeftUp.position.y, Yasuo.flyPointRightDown.position.y));
        }
        else
        {
            movePos = new Vector3(Random.Range(mid, Yasuo.flyPointRightDown.position.x), Random.Range(Yasuo.flyPointLeftUp.position.y, Yasuo.flyPointRightDown.position.y));
        }
    }

}
