using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletFactory : Singleton<BulletFactory>
{
    [Header("子弹预制体")]
    public GameObject rifleBulletPrefab;
    public GameObject sniperBulletPrefab;
    public GameObject RPGBulletPrefab;
    public GameObject shotGunBulletPrefab;

    [Space]
    public GameObject ShellPrefab;


    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }


    /// <summary>
    /// 通过子弹工厂创造预制体
    /// </summary>
    /// <param name="objectName">子弹所属武器名称</param>
    /// <param name="position">子弹实例化的位置</param>
    /// <param name="direction">子弹实例化后的方向</param>
    /// <param name="color">子弹拖尾颜色</param>
    /// <param name="damage">子弹攻击伤害</param>
    /// <param name="target">鼠标位置</param>
    /// <param name="speed"></param>
    /// <returns>子弹预制体</returns>
    public GameObject CreatBullet(string objectName, Vector3 position,
    Vector3 direction, Color color, float damage, Vector2 target, float speed)
    {
        GameObject bullet = ChooseBulletType(objectName);
        bullet.transform.position = position;
        bullet.transform.right = direction;
        bullet.GetComponent<Bullet>().SetColor(color).SetDamage(damage).SetTarget(target).SetSpeed(direction,speed);


        return bullet;
    }

    private GameObject ChooseBulletType(String objectName)
    {
        GameObject bullet;
        switch (objectName)
        {
            case "rifle":
                bullet = ObjectPool.Instance.GetObject(rifleBulletPrefab);
                break;
            case "sniper":
                bullet = ObjectPool.Instance.GetObject(sniperBulletPrefab);
                break;
            case "RPG":
                bullet = ObjectPool.Instance.GetObject(RPGBulletPrefab);
                break;
            case "shotGun":
                bullet = ObjectPool.Instance.GetObject(shotGunBulletPrefab);
                break;
            default:
                bullet = null;
                Debug.LogWarning("没有符合类型的子弹预制体");
                break;
        }

        return bullet;
    }

}
