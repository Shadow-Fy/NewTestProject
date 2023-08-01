using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (CompareTag("Player"))
        {
            collision.gameObject.GetComponent<IDamageable>().GetHit(7);
            //collision.gameObject.GetComponent<Rigidbody2D>().AddForce()
        }
    }
}
