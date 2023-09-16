using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFlash : MonoBehaviour
{
    public Image img;
    public float time;
    public Color flashcolor;

    private Color defaultcolor;

    // Start is called before the first frame update

    void Start()
    {
        EventControl.Instance.PlayerHurtEvent += FlashScreen;
        defaultcolor = img.color;
    }

    private void OnDisable()
    {
        EventControl.Instance.PlayerHurtEvent -= FlashScreen;
    }


    public void FlashScreen(float arg1, float arg2)
    {
        StartCoroutine(Flash());
    }

    IEnumerator Flash()
    {
        img.enabled = true;
        img.color = flashcolor;
        yield return new WaitForSeconds(time);
        img.color = defaultcolor;
        img.enabled = false;
    }


}
