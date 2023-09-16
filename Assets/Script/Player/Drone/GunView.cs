using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunView : MonoBehaviour
{
    [SerializeField] private GameObject[] gunsIcon;

    private void Start()
    {
        EventControl.Instance.PlayerGunEvent += UpdateGunUI;
    }

    private void OnDisable()
    {
        EventControl.Instance.PlayerGunEvent -= UpdateGunUI;
    }

    public void UpdateGunUI(int lastcount, int count)
    {
        gunsIcon[lastcount].SetActive(false);
        gunsIcon[count].SetActive(true);
    }
}
