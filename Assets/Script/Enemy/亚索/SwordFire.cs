using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordFire : MonoBehaviour
{
    public bool canmove;

    private void OnEnable()
    {
        canmove = false;
    }
    void FixedUpdate()
    {
        if (canmove)
            Move();

    }

    public void canMove()
    {
        canmove = true;
    }




    public void Move()//飞行
    {

        transform.position += 12f * transform.right;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ground" || other.tag == "Player")
        {
            ObjectPool.Instance.PushObject(gameObject);
        }

        if (other.CompareTag("Player"))
        {
            other.GetComponent<IDamageable>().GetHit(10);
        }
    }
}
