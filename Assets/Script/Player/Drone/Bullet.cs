using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public float speed;
    [SerializeField] private TrailRenderer trail;
    private float lifetime = 5f;
    // Start is called before the first frame update
    private void OnEnable()
    {
        lifetime = 5;
    }

    void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            ObjectPool.Instance.PushObject(gameObject);
            gameObject.GetComponent<TrailRenderer>().Clear();
        }
        transform.position += transform.right * speed * Time.deltaTime;
    }
    public void SetColor(Color color)
    {
        trail.startColor = color;
        trail.endColor = new Color(color.r, color.g, color.b, 0);
    }

    public void SetSpeed(Vector3 direction, float _speed)
    {
        transform.right = direction;
        speed = _speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ObjectPool.Instance.PushObject(gameObject);
        gameObject.GetComponent<TrailRenderer>().Clear();
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<IDamageable>().GetHit(damage);
        }
    }
}
