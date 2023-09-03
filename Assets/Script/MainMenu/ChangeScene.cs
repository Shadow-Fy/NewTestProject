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
    public float multiply;
    public Image backGround;
    private float alpha = 0;


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
    }

    public void LoadScene()
    {
        StartCoroutine(loadlevel());
    }




    IEnumerator loadlevel()
    //设置协程类型方法loadlevel
    {
        //backGround.gameObject.SetActive(true);

        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        ////加载本场景数值 +1的场景SampleScene
        operation.allowSceneActivation = false;
        ////先不加载下一场景
        while (!operation.isDone)
        {
            backGround.color = new Color(1, 1, 1, alpha += Time.deltaTime * 4);
            if (alpha >= 1)
            {
                operation.allowSceneActivation = true;
            }
            //    //slider.value = operation.progress;
            //    //operation.progress本质上就是数值
            //    //text.text = operation.progress * 100 + "%";

            //    //if (operation.progress >= 0.9f)
            //    //由于该方法本身的问题所以需要手动把数值改为100%
            //    //{
            //    //slider.value = 1;
            //    //text.text = "Press any key";
            //    //    if (Input.anyKeyDown)
            //    //    {
            //    //        operation.allowSceneActivation = true;
            //    //        //按下任意按钮开始加载下一场景
            //    //    }
            yield return null;
        }


    }


}
