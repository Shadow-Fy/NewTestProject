using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Clock;
using DG.Tweening;

public class EndController : MonoBehaviour
{
    public TMP_Text time;
    public TMP_Text currentTime;
    public PlayerController playerController;
    public TMP_Text playerHealth;
    public GameObject panel;
    public YasuoControl yaSuo;
    // Start is called before the first frame update
    void FixedUpdate()
    {
        currentTime.text = time.text;
        playerHealth.text = playerController.currenthealth.ToString();
    }

    private void Update()
    {
        if (yaSuo.health <= 0)
            PauseGame();
    }

    // Update is called once per frame
    public void PauseGame()
    {
        Time.timeScale = 0;
        panel.SetActive(true);
        DOTween.To(() => panel.GetComponent<RectTransform>().anchoredPosition,
            x => panel.GetComponent<RectTransform>().anchoredPosition = x,
            new Vector2(0, 0), 0.5f)
            .SetUpdate(true)
            .OnComplete(() =>
            {
                //Debug.Log("Pause");
            })
            .SetEase(Ease.OutElastic);
    }
}
