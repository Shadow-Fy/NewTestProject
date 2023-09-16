using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour
{
    public TMP_Text healthtext;
    public Image healthbar;
    void Start()
    {
        EventControl.Instance.PLayerInitEvent += UpdateHealthUI;
        EventControl.Instance.PlayerHurtEvent += UpdateHealthUI;
    }

    private void OnDisable()
    {
        EventControl.Instance.PLayerInitEvent -= UpdateHealthUI;
        EventControl.Instance.PlayerHurtEvent -= UpdateHealthUI;
    }


    public void UpdateHealthUI(float currenthealth, float maxhealth)
    {
        Debug.Log("更新UI");
        healthbar.fillAmount = (float)currenthealth / (float)maxhealth;
        healthtext.text = currenthealth.ToString() + " / " + maxhealth.ToString();
    }


}
