using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkeletonBaseState
{
    protected Skeleton skeleton;

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void EndState();
}
