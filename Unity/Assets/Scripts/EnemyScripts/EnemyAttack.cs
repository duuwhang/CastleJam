using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    bool DamageAreaActive;
    bool attackOnCooldown;
    bool inRange;
    float windUpTime;
    float currentWindUpTime;
    public GameObject AttackArea;
    

    void Start()
    {
        attackOnCooldown = true;
        inRange = false;
        currentWindUpTime = 0f;
        DamageAreaActive = false;
        AttackArea.SetActive(false);
    } 

    // Update is called once per frame
    void Update()
    {
        if (inRange)
        {
            currentWindUpTime++;
        }

        if (currentWindUpTime >= windUpTime)
        {
            DoAttack();
        }
    }

    public void AttackRangeDetection()
    {
        Debug.Log("We are ready to attack!");
        inRange = true;
    }

    public void DoAttack()
    {
        DamageAreaActive = true;
        AttackArea.SetActive(true);
        DamageAreaActive = false;
    }
}
