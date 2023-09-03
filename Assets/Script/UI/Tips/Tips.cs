using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tips : MonoBehaviour
{
    [SerializeField]TMP_Text text;
    [SerializeField]Canvas tipsCanvas;
    [TextArea][SerializeField]private string textInfo;

    void Start(){
        text.SetText(textInfo);
        tipsCanvas.gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            Debug.Log("玩家进入");
            SaveManager.Instance?.SetResetPoint(transform.position);
            tipsCanvas.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Player"){
            Debug.Log("玩家退出");
            tipsCanvas.gameObject.SetActive(false);
        }
    }
}
