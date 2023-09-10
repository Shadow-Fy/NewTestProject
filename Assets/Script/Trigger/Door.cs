using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    bool isStay = false;
    [SerializeField] GameObject tipUI;

    void Update()
    {
        if (isStay)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                SceneControl.Instance.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
