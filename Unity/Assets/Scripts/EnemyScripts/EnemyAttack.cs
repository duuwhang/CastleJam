using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    bool attackOnCooldown;
    bool inRange;
    float windUpTime;
    float currentWindUpTime;

    void Start()
    {
        attackOnCooldown = true;
        inRange = false;
        currentWindUpTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange)
        {
            currentWindUpTime++;
        }

    }

    public void AttackRangeDetection()
    {
        Debug.Log("We are ready to attack!");
        inRange = true;
    }
}
