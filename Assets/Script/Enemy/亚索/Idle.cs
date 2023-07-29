using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : YasuoBaseState
{
    public Idle(YasuoControl yasuo)
    {
        this.Yasuo = yasuo;
    }

    public override void EnterState()
    {
        Yasuo._Anim.SetBool("run", false);
        Yasuo.idleTime = 2.5f;
    }

    public override void OnUpdate()
    {
        Yasuo.IdleAction();
    }
}
