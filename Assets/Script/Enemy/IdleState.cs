using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : EnemyBaseState
{
    public IdleState(Enemy _object)
    {
        enemy = _object;
    }
    public override void EnterState()
    {
        enemy.anim.SetBool("run", false);
        enemy.idletime = 2f;
    }

    public override void OnUpdate()
    {
        enemy.IdleAction();
    }
}
