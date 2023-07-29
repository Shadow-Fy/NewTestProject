using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordRain : MonoBehaviour
{

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<IDamageable>().GetHit(10);
        }
    }

}
