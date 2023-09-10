using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{

    public TMP_Text text;
    private float colorChange = 1;
    private bool upDown = true;
    private bool isStart = false;


    void Update()
    {
        if (upDown)
        {
            colorChange -= Time.deltaTime * 0.5f;
            if (colorChange <= 0)
                upDown = false;
        }
        else
        {
            colorChange += Time.deltaTime * 0.5f;
            if (colorChange >= 1)
                upDown = true;
        }
        if (text != null)
            text.alpha = colorChange;

        if (Input.anyKeyDown && !isStart)
        {
            SceneControl.Instance.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            isStart = !isStart;
        }

    }


}
