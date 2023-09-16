using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel
{
    public float CurrentHealth { get; set; }
    public float MaxHealth { get; set; } = 10000;
    public float Speed { get; set; }
    public float JumpForce { get; set; }

    public void InitPlayerHealth()
    {
        CurrentHealth = MaxHealth;
        // Debug.Log("初始化生命值成功 当前生命值为：" + CurrentHealth);
    }

    public void ChangePlayerHealth(float damage)
    {
        CurrentHealth -= damage;
    }

    public bool IsDead()
    {
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            return true;
        }
        else
        {
            return false;
        }
    }

}
