using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class DontActiveObjectPool : AbstractObjectPool
{
    public override GameObject CreateGameObject(GameObject prefabe)
    {
        GameObject gameObject = ObjectPool.Instance.GetObjectButNotActive(prefabe);
        return gameObject;
    }
}
