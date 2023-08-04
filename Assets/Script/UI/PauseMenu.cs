using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuInPause;
    private bool pause=false;
    
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pause)
            {
                Resume();
            }
            else
            {
                PauseGame();
            }
        }
    }
    
    //恢复继续游戏
    public void Resume()
    {
        pause = false;
        DOTween.To(() => menuInPause.GetComponent<RectTransform>().anchoredPosition,
                x => menuInPause.GetComponent<RectTransform>().anchoredPosition = x,
                new Vector2(0, 650), 0.2f)
            .SetUpdate(true)
            .OnComplete(() =>
            {
                Time.timeScale = 1;
                menuInPause.SetActive(false);
                Debug.Log("Resume");
            })
            .SetEase(Ease.Linear);
    }

    //玩家按下暂停
    public void PauseGame()
    {
        pause = true;
        Time.timeScale = 0;
        menuInPause.SetActive(true);
        DOTween.To(() => menuInPause.GetComponent<RectTransform>().anchoredPosition,
            x => menuInPause.GetComponent<RectTransform>().anchoredPosition = x,
            new Vector2(0, 0), 0.5f)
            .SetUpdate(true)
            .OnComplete(() =>
            {
                Debug.Log("Pause");
            })
            .SetEase(Ease.OutElastic);
    }
    
}
