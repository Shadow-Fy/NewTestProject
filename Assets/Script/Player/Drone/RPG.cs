using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPG : Gun
{
    public int roketNum;
    public float rocketAngel;

    protected override void Fire()
    {
        int median = roketNum / 2;

        for (int i = 0; i < roketNum; i++)
        {
            GameObject bullet = ObjectPool.Instance.GetObject(bulletPrefab);
            bullet.transform.position = muzzlePos.position;

            if (roketNum % 2 == 1)
            {
                bullet.transform.right = Quaternion.AngleAxis(rocketAngel * (i - median), Vector3.forward) * direction;
            }
            else
            {
                bullet.transform.right = Quaternion.AngleAxis(rocketAngel * (i - median) + rocketAngel / 2, Vector3.forward) * direction;
            }
            bullet.GetComponent<Rocket>().SetTarget(mousePos);
        }
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
