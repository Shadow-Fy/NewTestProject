using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : EnemyBaseState
{
    public PatrolState(Enemy _object)
    {
        enemy = _object;
    }
    public override void EnterState()
    {
        enemy.ChangePatrolTarget();
    }

    public override void OnUpdate()
    {
        enemy.MoveAction();
        if (Mathf.Abs(enemy.transform.position.x - enemy.targetpoint.x) < 0.1f)
        {
            enemy.anim.SetBool("run", false);
            enemy.TransitionToState(Enemy.StateEnum.idle);
            enemy.ChangePatrolTarget();
        }
    }
}
