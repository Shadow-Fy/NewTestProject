using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YasuoSwordFire : MonoBehaviour
{

    private float lifetime = 3;


    // private Aim aim;

    void Start()
    {
    }
    private void Update()
    {
    }
    void FixedUpdate()
    {
        Move();
    }

    public void Move()//飞行
    {
        lifetime -= Time.deltaTime;

        transform.position +=  -transform.right * 10;
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
            other.GetComponent<IDamageable>().GetHit(6);
        }
    }
}
