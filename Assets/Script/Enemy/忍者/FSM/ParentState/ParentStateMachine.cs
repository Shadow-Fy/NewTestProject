using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assassin_Cultist_State
{
    public abstract class ParentStateMachine
    {
        [Header("Assassin Cultist State")]
        public ChildStateMachine currentChildState;
        public IdleState idleState = new IdleState();
        public RunState runState = new RunState();
        public HideState hideState = new HideState();
        public AppearState appearState = new AppearState();
        public AppearAttackState appearAttackState = new AppearAttackState();
        public FollowTargetState followTargetState = new FollowTargetState();
        public AttackTargetState attackTargetState = new AttackTargetState();
        public SkillAttackState skillAttackState = new SkillAttackState();

        public abstract void EnterState(Boss boss);
        public abstract void UpdateState(Boss boss);
        public abstract void ExitState(Boss boss);
        
        public void TransitionChildState(ChildStateMachine state, Boss boss)
        {
            currentChildState = state;
            currentChildState.EnterState(boss);
        }
    }

}
