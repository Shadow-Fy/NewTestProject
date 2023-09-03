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
        defaultcolor = img.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FlashScreen()
    {
        StartCoroutine(Flash());
    }

    IEnumerator Flash()
    {
        img.color = flashcolor;
        yield return new WaitForSeconds(time);
        img.color = defaultcolor;
        img.gameObject.SetActive(false);
    }


}
