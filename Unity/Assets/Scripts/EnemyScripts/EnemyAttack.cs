using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    bool DamageAreaActive;
    bool attackOnCooldown;
    bool inRange;
    [SerializeField] float windUpTime;
    float currentWindUpTime;
    public GameObject AttackArea;

    
    

    void Start()
    {
        attackOnCooldown = true;
        inRange = false;
        currentWindUpTime = 1f;
        DamageAreaActive = false;
        AttackArea.SetActive(false);
    } 

    // Update is called once per frame
    void Update()
    {
        if (inRange)
        {
            currentWindUpTime += currentWindUpTime * Time.deltaTime;
            Debug.Log(currentWindUpTime);
        }

        if (currentWindUpTime >= windUpTime)
        {
            DoAttack();
            currentWindUpTime = 1f;
        }
    }

    public void AttackRangeDetection()
    {
        inRange = true;
    }

    public void DoAttack()
    {
        DamageAreaActive = true;
        AttackArea.SetActive(true); 
        DamageAreaActive = false;
    }

    public void DoDamage(GameObject Player)
    {
        Debug.Log("Damage");
        Player.TryGetComponent<Health>(out Health healthValue);
        healthValue.Damage(1);
    }
}
