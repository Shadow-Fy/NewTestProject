using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceSide : MonoBehaviour, AnimationEvent
{
    [SerializeField]GameObject target;
    public void AnimationEvent()
    {
        target?.SetActive(false);
    }
}
