using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private bool arrived;
    public float lerp;
    public float speed;
    private Vector2 targetPos;
    private Vector3 direction;
    private float lifetime = 5f;
    public float damage;
    [SerializeField] private TrailRenderer trail;


    private void OnEnable()
    {
        speed = 50;
        lifetime = 5;
        arrived = false;
    }

    public void SetTarget(Vector2 _target)
    {
        targetPos = _target;
        speed = GetRandom(speed);
    }


    private void Update()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);//ȡ��α���

        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            ObjectPool.Instance.PushObject(gameObject);
            gameObject.GetComponent<TrailRenderer>().Clear();
        }

        direction = (targetPos - new Vector2(transform.position.x, transform.position.y)).normalized;

        if (!arrived)
        {
            transform.right = Vector3.Slerp(transform.right, direction, lerp / Vector2.Distance(transform.position, targetPos));
        }
        if (Vector2.Distance(transform.position, targetPos) < 1f && !arrived)
            arrived = true;

        transform.position += transform.right * GetRandom(speed) * Time.deltaTime;
    }

    public void SetColor(Color color)
    {
        trail.startColor = color;
        trail.endColor = new Color(color.r, color.g, color.b, 0);
    }

    private float GetRandom(float num)
    {
        return Random.Range(num - 15, num);
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
