using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeScene : MonoBehaviour
{

    public TMP_Text text;
    private float colorChange = 255;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        colorChange -= Time.deltaTime;
        text.color = new Color(255, 255, 255, colorChange);
    }
}
