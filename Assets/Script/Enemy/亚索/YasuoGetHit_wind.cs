using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YasuoGetHit_wind : MonoBehaviour
{
    private YasuoControl boss;

    void Start()
    {
        boss = GameObject.Find("托儿所").GetComponent<YasuoControl>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(11111);
            other.GetComponent<IDamageable>().GetHit(10);
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1) * 15, ForceMode2D.Impulse);
            if (boss.health < 1500 && boss.health >= 900)/* 二阶段后击飞接R */
            {
                boss.可以接大 = true;
            }
        }


    }
}
