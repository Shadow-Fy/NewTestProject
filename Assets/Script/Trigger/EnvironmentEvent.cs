using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentEvent : MonoBehaviour
{
    
    [SerializeField]EventType eventType;
    bool isActive = false;

    //当玩家进入时，启动对应事件
    private void OnTriggerEnter2D(Collider2D other) {
        if(!isActive && other.gameObject.tag == "Player"){
            isActive = true;
            EventManager.Instance?.InvokeEvent(eventType);
        }
    }
}
