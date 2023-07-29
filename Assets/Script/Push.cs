using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour
{
    public void PushPrefeb()
    {
        ObjectPool.Instance.PushObject(gameObject);
    }
}
