using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Health : MonoBehaviour
{
    public Action Died;

    [SerializeField] public int MaxHealth;

    [SerializeField] private InputActionReference heal;
    [SerializeField] private InputActionReference damage;
    public int CurrentHealth;

    int TestHeal = 5;
    const int TestDamage = 5;

    void Start()
    {
        CurrentHealth = MaxHealth;
        heal.action.performed += Heal;

    }

    public void Damage(int amount = TestDamage)
    {
        Debug.Log("Damage");
        CurrentHealth -= amount;
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;

            Died?.Invoke();
        }
    }

    private void Heal(InputAction.CallbackContext context)
    {
        CurrentHealth += TestHeal;
        if (CurrentHealth > MaxHealth) CurrentHealth = MaxHealth;
    }

    

    // Update is called once per frame
    void Update()
    {

    }
}
