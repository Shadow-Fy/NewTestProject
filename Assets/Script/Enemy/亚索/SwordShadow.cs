using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordShadow : MonoBehaviour
{
    public string _prefabname;
    private Transform _YasuoSword;
    private SpriteRenderer thisSprite;
    private SpriteRenderer _YasuoSwordSprite;
    private Color color = new Color(1, 1, 1);

    [Header("时间")]
    public float activeTime;    //显示时间
    public float activeStart;   //开始显示的时间

    [Header("不透明度")]
    private float alpha;
    public float alphaSet;
    public float alphaMultiplier;

    private void OnEnable()
    {
        _YasuoSword = GameObject.FindGameObjectWithTag(_prefabname).transform;
        thisSprite = GetComponent<SpriteRenderer>();
        _YasuoSwordSprite = _YasuoSword.GetComponent<SpriteRenderer>();

        alpha = alphaSet;

        thisSprite.sprite = _YasuoSwordSprite.sprite;

        transform.position = _YasuoSword.position;
        transform.localScale = _YasuoSword.localScale;
        transform.rotation = _YasuoSword.rotation;

        activeStart = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        alpha *= alphaMultiplier;
        color = new Color(1f, 1f, 1f, alpha);

        thisSprite.color = color;

        if (Time.time >= activeStart + activeTime)
        {
            ObjectPool.Instance.PushObject(gameObject);
        }
    }
}
