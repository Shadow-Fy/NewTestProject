using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GetHit_Skill : MonoBehaviour
{
    private YaSuo boss;
    void Start()
    {
        boss = GameObject.Find("托儿所").GetComponent<YaSuo>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Transform>().position = transform.position + new Vector3(0, 2, 0);
            other.GetComponent<IDamageable>().GetHit(6);
        }

    }
    public void Push()
    {
        ObjectPool.Instance.PushObject(gameObject);
    }

    public void CameraMove()
    {
        Camera.main.DOShakePosition(0.5f, new Vector3(0, -2, 0));

    }
}
