using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour, IDamageable
{
    public enum SkeletonEnum
    {
        Call, Idle, Attack1, Patrol
    }

    private Dictionary<SkeletonEnum, SkeletonBaseState> states = new Dictionary<SkeletonEnum, SkeletonBaseState>();
    private SkeletonBaseState currentState;


    private void Awake()
    {
        //states.Add()
    }
    // Start is called before the first frame update
    void Start()
    {

    }


    void Update()
    {
        //currentState.UpdateState();
    }

    /// <summary>
    /// 切换状态为目标状态
    /// </summary>
    /// <param name="type">需要切换的状态枚举名</param>
    public void SwitchState(SkeletonEnum type)
    {
        currentState = states[type];
        currentState.EnterState();
    }




    public void GetHit(float damage)
    {
    }
}
