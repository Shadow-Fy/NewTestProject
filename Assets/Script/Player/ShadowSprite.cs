using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowSprite : MonoBehaviour
{
    private Transform player;
    private SpriteRenderer thisSprite;
    private SpriteRenderer playerSprite;

    private Color color;

    //MMMMrD添加：對象池脚本需要 （不知道爲什麽是繁體將就看一下吧）
    [Header("位置參數")]
    private Transform userTransform;  //使用者位置

    [Header("时间控制参数")]
    public float activeTime;
    public float activeStart;

    [Header("不透明度控制")]
    public float alphaSet;
    public float alphaMultiplier;
    private float alpha;
    private SpriteRenderer userSpriteRenderer;  //使用者的SpriteRenderer

    protected void OnEnable()
    {
        //TODO：需要修改Player使用影子的邏輯
        // player = GameObject.FindGameObjectWithTag("Player").transform;
        thisSprite = GetComponent<SpriteRenderer>();
        // playerSprite = player.GetComponent<SpriteRenderer>();
        if (userSpriteRenderer != null)
        {
            alpha = alphaSet;
            thisSprite.sprite = userSpriteRenderer.sprite;
            transform.position = userTransform.position;
            transform.localScale = userTransform.localScale;
            transform.rotation = userTransform.rotation;
            activeStart = Time.time;
        }



    }

    protected void Update()
    {
        alpha *= alphaMultiplier;

        color = new Color(1, 1, 1, alpha);

        thisSprite.color = color;

        if (Time.time >= activeStart + activeTime)
        {
            ObjectPool.Instance.PushObject(this.gameObject);
        }
    }

    //TODO：往后可以优化ShadowSprite，使得所有角色都可以使用
    //MMMMrD添加：忍者脚本需要
    public void Init(Transform transform, SpriteRenderer spriteRenderer)
    {
        userTransform = transform;
        userSpriteRenderer = spriteRenderer;
        Debug.Log(userTransform);
        Debug.Log(userSpriteRenderer);
    }
}
