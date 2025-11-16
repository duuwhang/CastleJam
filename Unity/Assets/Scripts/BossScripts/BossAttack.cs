using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(EnemyMovement))]
public class BossAttack : MonoBehaviour
{
    enum AttackState { READY, DECISION, WINDUP, ATTACKING, COOLDOWN }
    enum AttackTM { POISON, DIVE, RAM}
    private AttackTM attackTM;
    private AttackState attackState;

    private EnemyMovement playerMovement;

    [Header("Dive")]
    [SerializeField] float diveWindUpTime;
    [SerializeField] float diveAttackDuration; 
    [SerializeField] float diveCooldownDuration;

    [Header("Ram")]
    [SerializeField] float ramWindUpTime;
    [SerializeField] float ramAttackDuration; 
    [SerializeField] float ramCooldownDuration;

    [Header("Poison")]
    [SerializeField] float poisonWindUpTime;
    [SerializeField] float poisonAttackDuration; 
    [SerializeField] float poisonCooldownDuration;

    float windUpTime;
    float attackDuration;
    float cooldownDuration;

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
            case AttackState.DECISION:
                int randomNumber = UnityEngine.Random.Range(0, 100);
                var attackMove = randomNumber switch
                {
                    <= 30 => attackTM = AttackTM.DIVE,
                    <= 60 => attackTM = AttackTM.RAM,
                    _ => attackTM = AttackTM.DIVE
                };
                SetTimeValues();
                break;
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
    public void SetTimeValues()
    {
        switch (attackTM)
        {
            case AttackTM.DIVE:
                currentattackDuration = diveAttackDuration;
                currentWindUpTime = diveWindUpTime;
                currentCooldownTime = diveCooldownDuration;
                break;
            case AttackTM.RAM:
                currentattackDuration = ramAttackDuration;
                currentWindUpTime = ramWindUpTime;
                currentCooldownTime = ramCooldownDuration;
                break;
            case AttackTM.POISON:
                currentattackDuration = poisonAttackDuration;
                currentWindUpTime = poisonWindUpTime;
                currentCooldownTime = poisonCooldownDuration;
                break;
        }
    }
}
