using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Gun
{
    protected override void Fire()
    {
        // bullet.transform.position = muzzlePos.position;
        // bullet.GetComponent<Bullet>().damage = damage;

        float angel = Random.Range(-3f, 3f);
        // bullet.GetComponent<Bullet>().SetSpeed(Quaternion.AngleAxis(angel, Vector3.forward) * direction, speed);
        // bullet.GetComponent<Bullet>().SetColor(color);
        BulletFactory.Instance.CreatBullet("rifle", muzzlePos.position, 
        Quaternion.AngleAxis(angel, Vector3.forward) * direction, color, damage, direction, speed);

        GameObject shell = ObjectPool.Instance.GetObject(shellPrefab);
        shell.transform.position = shellPos.position;
        shell.transform.rotation = shellPos.rotation;
    }
}
