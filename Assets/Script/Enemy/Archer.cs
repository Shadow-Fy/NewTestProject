using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Enemy, IDamageable
{
    public GameObject arrow;
    public void GetHit(float damage)
    {
        currenthealth -= Time.deltaTime;
        if (currenthealth < 1)
        {
            currenthealth = 0;
            isdead = true;
        }
        anim.SetTrigger("hit");
    }

    public void FireArrow()
    {
        FlipDirection();
        Vector2 direction = (new Vector2(targetpoint.x, transform.position.y) - new Vector2(transform.position.x, transform.position.y)).normalized;
        GameObject arrowprefab = ObjectPool.Instance.GetObject(arrow);
        arrowprefab.transform.right = direction;
        arrowprefab.transform.position = new Vector3(transform.position.x + direction.x * 0.7f, transform.position.y - 1.24f);
    }
}
