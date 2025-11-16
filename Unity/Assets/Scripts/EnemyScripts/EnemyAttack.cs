using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerAttack : MonoBehaviour
{
    enum AttackState { READY, WINDUP, ATTACKING, COOLDOWN }
    private AttackState attackState;

    private PlayerMovement playerMovement;

    [SerializeField] float windUpTime = 1;
    [SerializeField] float attackDuration = 1;
    [SerializeField] float cooldownDuration = 1;

    float currentattackDuration;
    float currentWindUpTime;
    float currentCooldownTime;

    public GameObject AttackArea;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        switch (attackState)
        {
            case AttackState.WINDUP:
                currentWindUpTime += Time.deltaTime;
                if (currentWindUpTime >= windUpTime) DoAttack();
                break;
            case AttackState.ATTACKING:
                currentattackDuration += Time.deltaTime;
                if (currentattackDuration >= attackDuration) EndAttack();
                break;
            case AttackState.COOLDOWN:
                currentCooldownTime += Time.deltaTime;
                if (currentCooldownTime >= cooldownDuration) attackState = AttackState.READY;
                break;
        }
    }

    private void EndAttack()
    {
        attackState = AttackState.COOLDOWN;
        AttackArea.SetActive(false);

        playerMovement.enabled = true;
    }

    public void Attack()
    {
        if (attackState != AttackState.READY) return;

        attackState = AttackState.WINDUP;
        currentWindUpTime = 0;

        playerMovement.enabled = false;
    }

    public void DoAttack()
    {
        attackState = AttackState.ATTACKING;

        Debug.Log("Attack wird ausgeführt");

        AttackArea.SetActive(true);
        currentattackDuration = 0;
    }

    public void DoDamage(GameObject Enemy)
    {
        Debug.Log("Damage");

        Enemy.TryGetComponent<Health>(out Health healthValue);
        healthValue.Damage(1);
    }

}
