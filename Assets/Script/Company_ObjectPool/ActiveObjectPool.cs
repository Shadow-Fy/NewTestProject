using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ActiveObjectPool : AbstractObjectPool
{
    public override GameObject CreateGameObject(GameObject prefabe)
    {
        GameObject gameObject = ObjectPool.Instance.GetObjectButNotActive(prefabe);
        gameObject.SetActive(true);
        return gameObject;
    }
}
