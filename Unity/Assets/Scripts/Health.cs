using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public Action Died;

    public int MaxHealth;
    public int CurrentHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    void Damage(int amount)
    {
        CurrentHealth -= amount;
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;

            Died?.Invoke();
        }
    }

    void Heal(int amount)
    {
        CurrentHealth += amount;
        if (CurrentHealth > MaxHealth) CurrentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
