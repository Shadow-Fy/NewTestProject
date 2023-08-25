using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attackstate3 : YasuoBaseState
{
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
            Yasuo.magicCircle.SetActive(false);
    }

    public void SixStarAttack()
    {

    }
}
