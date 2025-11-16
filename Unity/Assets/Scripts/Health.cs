using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public Action Died;
    public int MaxHealth;
    public int CurrentHealth;

    public void Damage(int amount)
    {
        CurrentHealth -= amount;
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;

            Died?.Invoke();
        }
    }

    public void Heal(int amount)
    {
        CurrentHealth += amount;
        if (CurrentHealth > MaxHealth) CurrentHealth = MaxHealth;
    }

}
