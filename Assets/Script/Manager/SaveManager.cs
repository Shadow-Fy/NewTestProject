using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    private Vector2 resetPoint;

    public void SetResetPoint(Vector2 point){
        resetPoint = point;
    }

    public void ResetPoint(GameObject gameObject){
        if(resetPoint != null)
            gameObject.transform.position = resetPoint + new Vector2(0, 1f);
    }
}
