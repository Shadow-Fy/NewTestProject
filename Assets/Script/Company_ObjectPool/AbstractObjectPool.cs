using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UserType{
    active, notActive
}

public class AbstractObjectPool
{
    public static AbstractObjectPool GetAbstractObjectPool(UserType userType)
    {
        switch (userType)
        {
            case UserType.active: return new ActiveObjectPool(); break;
            case UserType.notActive: return new DontActiveObjectPool(); break;
            default: return null;
        }
    }
    public virtual GameObject CreateGameObject(GameObject prefabe) {  return null; }
}
