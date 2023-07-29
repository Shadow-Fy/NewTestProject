using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShell : MonoBehaviour
{
    public float speed;//抛出速度
    public float stopTime = .5f;//停下时间
    public float fadeSpeed = .01f;//弹壳消失速度
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sprite;

    private void OnEnable()
    {
        float angel = Random.Range(-30f, 30f);
        rb.velocity = Quaternion.AngleAxis(angel, Vector3.forward) * Vector3.up * speed;
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1);
        rb.gravityScale = 3;
        StartCoroutine(Stop());
    }

    IEnumerator Stop()
    {
        yield return new WaitForSeconds(stopTime);

        while (sprite.color.a > 0)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a - fadeSpeed);
            yield return new WaitForFixedUpdate();
        }
        ObjectPool.Instance.PushObject(gameObject);
    }
}
