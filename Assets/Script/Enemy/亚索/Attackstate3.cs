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
        Yasuo.transform.position = new Vector3(58.910552f, 0);
        Yasuo.wing.SetActive(true);
        GameObject _wing = ObjectPool.Instance.GetObject(Yasuo.wingprefeb);
        _wing.transform.position = Yasuo.transform.position;
        Yasuo.Sword1.GetComponent<YasuoSword>().StartAttack3();
        Yasuo.Sword2.GetComponent<YasuoSword>().StartAttack3();
        Yasuo.Sword3.GetComponent<YasuoSword>().StartAttack3();

    }

    public override void OnUpdate()
    {
        Yasuo.StartWind();
    }

    public void SixStarAttack()
    {

    }
}
