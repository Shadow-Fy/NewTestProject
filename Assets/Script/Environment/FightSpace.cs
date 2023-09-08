using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightSpace : MonoBehaviour
{
    [SerializeField]GameObject leftPoint;   //左边界
    [SerializeField]GameObject rightPoint;  //右边界
    [SerializeField]GameObject baseCamera;  //主摄像机
    [SerializeField]GameObject bossCamera;  //Boss摄像机

    private void Start() {
        //事件注册
        EventManager.Instance?.AddListener(EventType.BossFindTarget, OpenObject);
        EventManager.Instance?.AddListener(EventType.BossDie, CloseObject);
    }
    
    private void OpenObject(){
        leftPoint?.SetActive(true);
        rightPoint?.SetActive(true);
        leftPoint.GetComponent<Animator>().Play("LeftOpen");
        rightPoint.GetComponent<Animator>().Play("RightOpen");

        baseCamera?.SetActive(false);
        bossCamera?.SetActive(true);
    }

    private void CloseObject(){
        leftPoint?.GetComponent<Animator>().Play("LeftClose");
        rightPoint?.GetComponent<Animator>().Play("RightClose");

        baseCamera?.SetActive(true);
        bossCamera?.SetActive(false);
    }
}
