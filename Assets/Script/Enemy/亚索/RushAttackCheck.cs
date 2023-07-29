using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushAttackCheck : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<IDamageable>().GetHit(20);
        }
    }
}
