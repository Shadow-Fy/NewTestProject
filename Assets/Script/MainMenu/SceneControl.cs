using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneControl : Singleton<SceneControl>
{
    public float multiply;
    public Image backGround;
    public bool isLoading;
    private float alpha = 1;

    protected override void Awake()
    {
        base.Awake();
        alpha = 1;
        StartCoroutine(NewScene());
        //DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(int scene)
    {
        if(isLoading)
            return;
        StartCoroutine(loadlevel(scene));
    }

    IEnumerator NewScene()
    {
        isLoading = true;
        while (alpha >= 0)
        {
            backGround.color = new Color(1, 1, 1, alpha -= Time.deltaTime * multiply);
            yield return null;
        }

        isLoading = false;
    }

    IEnumerator loadlevel(int scene)
    {
        isLoading = true;
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
        operation.allowSceneActivation = false;
        ObjectPool.Instance.ClearObjectPool();
        while (!operation.isDone)
        {
            backGround.color = new Color(1, 1, 1, alpha += Time.deltaTime * multiply);
            if (alpha >= 1)
            {
                operation.allowSceneActivation = true;

            }

            yield return null;
        }

        //isLoading = false;
    }

}
