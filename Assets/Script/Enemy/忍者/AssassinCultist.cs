using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assassin_Cultist_State
{
    enum AssassinCultist_State
    {
        GetHit,
    }

    //自定义数据类
    public class AssassinCultistData
    {
        public float currentHealth = 0;
        public float maxHealth = 0;

        public AssassinCultistData(float currentHealth, float maxHealth)
        {
            this.currentHealth = currentHealth;
            this.maxHealth = maxHealth;
        }
    }


    public class AssassinCultist : Boss
    {
        bool isDush;  //判断是否在冲刺状态
        bool spriteRendererActive;  //用于更改SpriteRender开关状态
        public GameObject shadow;
        SpriteRenderer spriteRenderer;
    
        public override void Start()
        {
            base.Start();
            spriteRenderer = GetComponent<SpriteRenderer>();
            TransitionState(patrolParentState);
        }

        public override void Update()
        {
            base.Update();
            anim.SetBool("IsAttack", isAttack);
            if(isDush)
            {
                ShadowSprite shadowSprite = ObjectPool.Instance.GetObjectButNotActive(shadow).GetComponent<ShadowSprite>();
                shadowSprite.Init(transform, spriteRenderer);
                shadowSprite.gameObject.SetActive(true);
            }
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            OnDrawRay();
        }

        public override void Movement()
        {
            if(isDush) return;

            base.Movement();
        }

        public void OnDrawRay()    
        {
            RaycastHit2D leftCheck = Raycast(new Vector2(-1f, 0), Vector2.left, 0.2f, playerMask);
            RaycastHit2D rightCheck = Raycast(new Vector2(1f, 0), Vector2.right, 0.2f, playerMask);
            RaycastHit2D leftGroundCheck = Raycast(new Vector2(-1f, 0), Vector2.left, 0.2f, groundMask);
            RaycastHit2D rightGroundCheck = Raycast(new Vector2(1f, 0), Vector2.left, 0.2f, groundMask);

            if((leftCheck || rightCheck || leftGroundCheck || rightGroundCheck) && !isDead)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }

        RaycastHit2D Raycast(Vector2 startPoint, Vector2 rayDirection, float Lenth, LayerMask layerMask)    //射线
        {
            Vector2 pos = transform.position;
            RaycastHit2D hit = Physics2D.Raycast(pos + startPoint, rayDirection, Lenth, layerMask);
            Debug.DrawRay(pos + startPoint, rayDirection*Lenth);
        
            return hit;
        }

        #region Animation Event

        //Animation Event
        public void AttackDush()    //攻击冲刺
        {
            int dir = 0;
            dir = TurnAround(dir);

            if(!isDush)
            {
                speedMultiple = 5f;
                rb.velocity = new Vector2(dir * characterStats.CurrentSpeed * speedMultiple, rb.velocity.y);
            }
            else
            {
                speedMultiple = baseSpeedMultiple;
                rb.velocity = new Vector2(0, rb.velocity.y);
            }

            isDush = !isDush;
        }

        public void SetSpriteRender() //更改图片可见状态
        {
            spriteRenderer.enabled  = spriteRendererActive;
            spriteRendererActive = !spriteRendererActive;
        }

        public void SetSpriteRenderTrue()
        {
            spriteRenderer.enabled = true;
        }

        #endregion

        public override void GetHit(float damage)
        {
            base.GetHit(damage);
            if (!anim.GetCurrentAnimatorStateInfo(2).IsName("GetHit") && !isDead)
            {
                Debug.Log("Gethit");
                AssassinCultistData data = new AssassinCultistData(characterStats.characterData.currentHealth, characterStats.characterData.maxHealth);
                string json = JsonUtility.ToJson(data);
                EventManager.Instance?.InvokeEvent<AssassinCultist_State>(AssassinCultist_State.GetHit, json);
            }
        }
    }
}
