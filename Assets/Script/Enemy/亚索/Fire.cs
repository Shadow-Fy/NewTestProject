using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    private float lifetime = 6;
    private bool direction;
    private bool _canattackself;
    // private Aim aim;

    private void OnEnable()
    {

    }
    void FixedUpdate()
    {
        Move();
    }

    public void Move()//飞行
    {
        transform.position += transform.up * 0.55f;
        if (lifetime <= 0)
        {
            ObjectPool.Instance.PushObject(gameObject);
            lifetime = 3;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<IDamageable>().GetHit(8);
            ObjectPool.Instance.PushObject(gameObject);
        }

        if (other.CompareTag("PlayerAttack"))
        {
            transform.Rotate(new Vector3(0, 0, Random.Range(175, 185)));
            _canattackself = true;
        }

        if (other.CompareTag("Enemy") && _canattackself)
        {
            other.GetComponent<IDamageable>().GetHit(15);
        }
    }
}
