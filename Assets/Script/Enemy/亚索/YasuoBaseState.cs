using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class YasuoBaseState
{
    protected YasuoControl Yasuo;
    public abstract void EnterState();
    public abstract void OnUpdate();
}
