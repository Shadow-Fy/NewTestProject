using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YasuoGetHit_1 : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            other.GetComponent<IDamageable>().GetHit(8);
        }

    }
}
