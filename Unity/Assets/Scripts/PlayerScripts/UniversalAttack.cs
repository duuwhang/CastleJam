using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(EnemyMovement))]
public class UniversalAttack : MonoBehaviour
{
    enum AttackState { READY, WINDUP, ATTACKING, COOLDOWN }
    private AttackState attackState;

    private EnemyMovement playerMovement;

    [SerializeField] float windUpTime;
    [SerializeField] float attackDuration; 
    [SerializeField] float cooldownDuration;

    float currentattackDuration;
    float currentWindUpTime;
    float currentCooldownTime;

    public GameObject AttackArea;

    void Start()
    {
        playerMovement = GetComponent<EnemyMovement>();
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
                if(currentCooldownTime >= cooldownDuration) attackState = AttackState.READY;
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

    public void DoDamage(GameObject Player)
    {
        Debug.Log("Damage");

        Player.TryGetComponent<Health>(out Health healthValue);
        healthValue.Damage(1);
    }

}
