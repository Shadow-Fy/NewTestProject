using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Health : MonoBehaviour
{
    public TMP_Text healthtext;
    public static float currenthealth;
    public static float maxhealth;
    private Image healthbar;
    public PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        
        healthbar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        // maxhealth = playerController.maxhealth;
        // currenthealth = playerController.currenthealth;
        // healthbar.fillAmount = (float)currenthealth / (float)maxhealth;
        // healthtext.text = currenthealth.ToString() + " / " + maxhealth.ToString();
    }

    public void UpdateHealthUI()
    {

    }    

    // void Start()
    // {
    //     EventControl.Instance.PlayerEvent += FlashScreen;
    //     defaultcolor = img.color;
    // }

    // private void OnDisable()
    // {
    //     EventControl.Instance.PlayerEvent -= FlashScreen;
    // }
}
