using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    bool isStay = false;
    [SerializeField] GameObject tipUI;
    public ChangeScene changeScene;

    void Update()
    {
        if (isStay)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneControl.Instance.LoadScene();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isStay = true;
            tipUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isStay = false;
            tipUI.SetActive(false);
        }
    }
}
