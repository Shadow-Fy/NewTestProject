using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissile : MonoBehaviour
{
    public float damage;
    public float speed;
    public float originlifetime;
    public float lifetime;
    // Start is called before the first frame update
    private void OnEnable()
    {
        lifetime = originlifetime;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime < 0)
            ObjectPool.Instance.PushObject(gameObject);
        Move();
    }

    public virtual void Move()
    {
        transform.position += transform.right * speed * Time.deltaTime;
    }
}
