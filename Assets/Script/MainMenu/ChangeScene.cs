using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{

    public TMP_Text text;
    private float colorChange = 1;
    private bool upDown = true;
    public float multiply;


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

        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

}
