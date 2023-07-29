using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class YaSuoState
{
    public abstract void EnterState(YaSuo yasuo);
    public abstract void OnUpdateState(YaSuo yasuo);
}
