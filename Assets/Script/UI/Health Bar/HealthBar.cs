using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour, IDamageable
{
    [SerializeField] public Image hpImg; // 血量显示的Image控件
    [SerializeField] public Image hpEffectImg; // 血量变化效果的Image控件

    private float maxHp = 100f; // 最大血量
    private float currentHp; // 当前血量
    public float buffTime = 0.5f; // 血条缓冲时间

    private Coroutine updateCoroutine;
    private CharacterStats characterStats;

    Quaternion myRotaion;
    
    void Start()
    {
        myRotaion = Quaternion.identity;
        characterStats = transform.parent.gameObject.GetComponent<CharacterStats>();

        UpdateHealthBar(); // 更新血条显示

        // if(characterStats != null){
        //     maxHp = characterStats.MaxHealth;
        //     currentHp = characterStats.CurrentHealth;
        // }
    }

    void Update() {
        transform.rotation = myRotaion;        
    }

    public void SetHealth(float health)
    {
        // 限制血量在0到最大血量之间
        currentHp = Mathf.Clamp(health, 0f, maxHp);

        // 更新血条显示
        UpdateHealthBar();

        // 当血量小于等于0时，触发死亡效果
        if (currentHp <= 0)
        {
            // Die();
        }
    }

    // 死亡函数
    private void Die()
    {
        // 在此处添加死亡相关的代码
    }

    // 在禁用对象时停止协程
    private void OnDisable()
    {
        if (updateCoroutine != null)
        {
            StopCoroutine(updateCoroutine);
        }
    }

    // 增加血量
    public void IncreaseHealth(float amount)
    {
        SetHealth(currentHp + amount);
    }

    // 减少血量
    public void DecreaseHealth(float amount)
    {
        SetHealth(currentHp - amount);
    }

    // 更新血条显示
    private void UpdateHealthBar()
    {
        // 根据当前血量与最大血量计算并更新血条显示
        hpImg.fillAmount = (float)characterStats.CurrentHealth / characterStats.MaxHealth;
        if (characterStats.CurrentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
        // 缓慢减少血量变化效果的填充值
        if (updateCoroutine != null)
        {
            StopCoroutine(updateCoroutine);
        }
        if (characterStats.CurrentHealth > 0)
            updateCoroutine = StartCoroutine(UpdateHpEffect());
    }

    // 协程，用于实现缓慢减少血量变化效果的填充值
    private IEnumerator UpdateHpEffect()
    {
        float effectLength = hpEffectImg.fillAmount - hpImg.fillAmount; // 计算效果长度
        float elapsedTime = 0f; // 已过去的时间

        while (elapsedTime < buffTime && effectLength != 0)
        {
            elapsedTime += Time.deltaTime; // 更新已过去的时间
            hpEffectImg.fillAmount = Mathf.Lerp(hpImg.fillAmount + effectLength, hpImg.fillAmount, elapsedTime / buffTime); // 使用插值函数更新效果填充值
            yield return null;
        }

        hpEffectImg.fillAmount = hpImg.fillAmount; // 确保填充值与血条填充值一致
    }

    public void GetHit(float damage)
    {
        UpdateHealthBar();
    }
}