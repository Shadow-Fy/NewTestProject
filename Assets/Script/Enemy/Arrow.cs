using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : EnemyMissile
{

    // Update is called once per frame
    //public override void Update()
    //{
    //    base.Update();
    //}

    private void OnTriggerEnter2D(Collider2D other)
    {
        ObjectPool.Instance.PushObject(gameObject);
        if (other.CompareTag("Player"))
        {
            other.GetComponent<IDamageable>().GetHit(damage);
        }
    }
}
