using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(FredMovement))]
public class FredAttacks : MonoBehaviour
{
    enum AttackState { READY, DECISION, WINDUP, ATTACKING, COOLDOWN }
    enum AttackTM { POISON, DIVE, RAM}
    private AttackTM attackTM;
    private AttackState attackState;

    private FredMovement FredMovement;

    [Header("Dive Settings")]
    [SerializeField] float diveSpeed = 10f;
    [SerializeField] float groundY = -2f; // where the dive should end
    [SerializeField] GameObject diveHitbox; // assign in Inspector

    [Header("Dive")]
    [SerializeField] float diveWindUpTime = 1;
    [SerializeField] float diveAttackDuration = 1; 
    [SerializeField] float diveCooldownDuration = 1;

    [Header("Ram")]
    [SerializeField] float ramWindUpTime = 1;
    [SerializeField] float ramAttackDuration = 1; 
    [SerializeField] float ramCooldownDuration = 1;

    [Header("Poison")]
    [SerializeField] float poisonWindUpTime = 1;
    [SerializeField] float poisonAttackDuration = 1; 
    [SerializeField] float poisonCooldownDuration = 1;

    [Header("Scripts")]
    

    float windUpTime;
    float attackDuration;
    float cooldownDuration;

    float currentattackDuration;
    float currentWindUpTime;
    float currentCooldownTime;
    //public GameObject AttackArea;

    void Start()
    {
        Attack();
    }

    
    void Awake()
    {
        FredMovement = GetComponent<FredMovement>();
    }

    void Update()
    {
        switch (attackState)
        {
            case AttackState.DECISION:
                DecideOnAttack();
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
        //AttackArea.SetActive(false);
        Attack();
        FredMovement.enabled = true;
        attackState = AttackState.DECISION;
    }

    public void Attack()
    {
        //if (attackState != AttackState.READY) return;
        attackState = AttackState.WINDUP;
            currentWindUpTime = 0;
            
        FredMovement.enabled = false;
    }

    public void DoAttack()
    {
        attackState = AttackState.ATTACKING;
        // --- Das is für die eine Attacke ---
        //AttackArea.SetActive(true);

        // --- Wir machen einen switch und es werden die verschiedenen Attack Funktionen ausgeführt ---
        switch  (attackTM)
        {
            case AttackTM.DIVE:
                DoDiveAttack();
                break;
            case AttackTM.RAM:
                DoRamAttack();
                break;
            case AttackTM.POISON:
                DoPoisonAttack();
                break;
        }
        currentattackDuration = 0;
    }

    public void DoDamage(GameObject Player)
    {

        Player.TryGetComponent<Health>(out Health healthValue);
        healthValue.Damage(1);
    }
    public void DecideOnAttack()
    {
        int randomNumber = UnityEngine.Random.Range(0, 100);
        var attackMove = randomNumber switch
        {
            <= 30 => attackTM = AttackTM.DIVE,
            <= 60 => attackTM = AttackTM.RAM,
            _   => attackTM = AttackTM.DIVE
        };
        //Debug.Log(attackMove);
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
        attackState = AttackState.WINDUP;
    }

    public void DoDiveAttack()
    {
        Debug.Log("Dive Attack");
        // We need to be moving downwoards toward the ground in a sort of dash
        // we need a damage Area in front of us just slightly
        // We need to stay on the ground for a variable cooldown so only allow movement once the Cooldown has run out
    }
    public void DoRamAttack()
    {
        Debug.Log("Ram Attack");
    }
    public void DoPoisonAttack()
    {
        Debug.Log("Poison Attack");
    }
}
