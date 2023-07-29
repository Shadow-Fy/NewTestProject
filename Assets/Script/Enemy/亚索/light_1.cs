using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class light_1 : MonoBehaviour
{

    public void Push()
    {
        ObjectPool.Instance.PushObject(gameObject);
    }
}
