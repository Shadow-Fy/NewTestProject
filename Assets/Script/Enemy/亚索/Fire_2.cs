using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_2 : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool arrived = false;
    private Vector3 direction;
    private Vector3 targetpos;
    public float lerp;
    private GameObject player;

    private void OnEnable()
    {
        arrived = false;
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
    }
    void Update()
    {
        if (Vector2.Distance(transform.position, targetpos) > 10f)
            targetpos = player.transform.position;

    }

    private void FixedUpdate()
    {
        direction = (targetpos - transform.position).normalized;

        if (!arrived)
        {
            transform.up = Vector3.Slerp(transform.up, direction, lerp / Vector2.Distance(transform.position, targetpos));
            rb.velocity = transform.up * 350;
        }
        if (Vector2.Distance(transform.position, targetpos) < 1f && !arrived)
        {
            arrived = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Ground"))
        {
            ObjectPool.Instance.PushObject(gameObject);
        }

        if (other.CompareTag("Player"))
        {
            other.GetComponent<IDamageable>().GetHit(3f);
        }

    }
}
