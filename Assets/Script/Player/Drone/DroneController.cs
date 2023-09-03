using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DroneController : MonoBehaviour
{

    public GameObject[] guns;
    public GameObject[] gunsIcon;
    private int gunsNum;
    public Transform playerTR;
    public float speed = 40;
    private Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
        guns[0].SetActive(true);
        gunsIcon[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        DroneMove();
        SwitchGun();
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }


    private void DroneMove()
    {
        target = playerTR.position;
        float sqrLenght = (playerTR.position - transform.position).sqrMagnitude; // 比较向量大小时比较它们的平方数更快
        if (sqrLenght > 3 * 3)
            speed = 30;
        else
            speed = 24;

        if (transform.position.x > target.x)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (transform.position.x < target.x)
            transform.localScale = new Vector3(1, 1, 1);
    }

    void SwitchGun()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            guns[gunsNum].SetActive(false);
            gunsIcon[gunsNum].SetActive(false);
            if (--gunsNum < 0) gunsNum = guns.Length - 1;
            guns[gunsNum].SetActive(true);
            gunsIcon[gunsNum].SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            guns[gunsNum].SetActive(false);
            gunsIcon[gunsNum].SetActive(false);
            if (++gunsNum > guns.Length - 1) gunsNum = 0;
            guns[gunsNum].SetActive(true);
            gunsIcon[gunsNum].SetActive(true);
        }
    }
}
