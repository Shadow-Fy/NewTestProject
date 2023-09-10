using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    static T instance;
    public static T Instance
    {
        get { return instance; }
    }
    public static bool isInit
    {
        get { return instance != null; }
    }

    protected virtual void Awake()
    {
        if (instance == null)
            instance = (T)this;
        else
            Destroy(gameObject);

    }

    protected virtual void OnDestroy()
    {
        if (instance == this)
            instance = null;
    }
}
