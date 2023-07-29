using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float speed;
    public float interval;//射击间隔时间
    public GameObject bulletPrefab;//子弹预制体
    public GameObject shellPrefab;//弹壳预制体
    protected Transform muzzlePos;//枪口位置
    protected Transform shellPos;//弹壳位置
    protected Vector2 mousePos;//鼠标位置
    protected Vector2 direction;//发射方向
    public Color color;//子弹颜色
    protected float timer;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        shellPos = muzzlePos = transform.GetComponentInParent<Transform>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Shoot();
    }

    protected virtual void Shoot()
    {
        direction = (mousePos - new Vector2(transform.position.x, transform.position.y)).normalized;
        //direction = (mousePos - new Vector2(transform.position.x, transform.position.y));

        if (timer != 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0) timer = 0;
        }

        if (Input.GetButton("Fire1"))
        {
            if (timer == 0)
            {
                timer = interval;
                Fire();
            }
        }
    }

    protected virtual void Fire()
    {
        GameObject bullet = ObjectPool.Instance.GetObject(bulletPrefab);
        bullet.transform.position = muzzlePos.position;

        float angel = Random.Range(-3f, 3f);
        bullet.GetComponent<Bullet>().SetSpeed(Quaternion.AngleAxis(angel, Vector3.forward) * direction, speed);
        bullet.GetComponent<Bullet>().SetColor(color);

        GameObject shell = ObjectPool.Instance.GetObject(shellPrefab);
        shell.transform.position = shellPos.position;
        shell.transform.rotation = shellPos.rotation;
    }

}
