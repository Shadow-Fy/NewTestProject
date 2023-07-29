using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YasuoCirc : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerAttack"))
        {
            ObjectPool.Instance.PushObject(other.gameObject);
        }
    }
}
