using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperRifle : Gun
{
    protected override void Fire()
    {
        BulletFactory.Instance.CreatBullet("sniper",muzzlePos.position,direction,color,damage,direction,speed);
        // GameObject bullet = ObjectPool.Instance.GetObject(bulletPrefab);
        // bullet.transform.position = muzzlePos.position;
        // bullet.GetComponent<Bullet>().damage = damage;

        // bullet.GetComponent<Bullet>().SetSpeed(direction, speed);
        // bullet.GetComponent<Bullet>().SetColor(color);

        GameObject shell = ObjectPool.Instance.GetObject(shellPrefab);
        shell.transform.position = shellPos.position;
        shell.transform.rotation = shellPos.rotation;
    }
}
