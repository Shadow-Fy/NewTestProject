using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assassin_Cultist_State
{
    public class AppearState : ChildStateMachine
    {
        public override void EnterState(Boss boss)
        {
            float randomX = Random.Range(boss.sceneLeftPoint.position.x, boss.sceneRightPoint.position.x);
            Vector2 destination = new Vector2(randomX, boss.transform.position.y);
            Collider2D aroundObject = Physics2D.OverlapCircle(destination, 10, boss.playerMask);

            //MMMMrD修改：修改前为 "!= null", 该写法导致栈溢出, 如果有其他bug再尝试其他写法
            if(aroundObject == null)
            {
                boss.currentParentState.TransitionChildState(boss.currentParentState.appearState, boss);
                return;
            }

            boss.transform.position = destination;
            boss.anim.SetTrigger("Appear");
            boss.TurnAround();
        }

        public override void UpdateState(Boss boss)
        {
            if(!boss.anim.GetCurrentAnimatorStateInfo(3).IsName("Appear"))
            {
                boss.currentParentState.TransitionChildState(boss.currentParentState.followTargetState, boss);
            }
        }

        public override void ExitState(Boss boss)
        {
            
        }
    }
    
}

