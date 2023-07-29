using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundAttack : MonoBehaviour
{
    public float deathtime = 0.14f;
    public int speed;
    private void OnEnable()
    {
        deathtime = 0.15f;
        transform.position = new Vector2(transform.position.x, -34.3f);
    }

    private void FixedUpdate()
    {
        deathtime -= Time.fixedDeltaTime;
        transform.position = Vector3.MoveTowards(transform.position, new Vector2(transform.position.x, -9.44f), speed * Time.fixedDeltaTime);
        if (deathtime <= 0)
            ObjectPool.Instance.PushObject(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<IDamageable>().GetHit(15f);
        }

    }

}
