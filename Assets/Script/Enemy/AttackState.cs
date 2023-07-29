using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : EnemyBaseState
{
    public AttackState(Enemy _object)
    {
        enemy = _object;
    }
    public override void EnterState()
    {

    }

    public override void OnUpdate()
    {
        if (Vector2.Distance(enemy.transform.position, enemy.targetpoint) > enemy.attackrate + 1)
        {
            if (!enemy.anim.GetCurrentAnimatorStateInfo(0).IsName("attack1"))
                enemy.MoveAction();
        }
        else
        {
            enemy.AttackAction();
        }
    }
}
