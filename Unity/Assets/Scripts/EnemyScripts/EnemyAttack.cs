using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

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
            AttackArea.SetActive(false);

        }
    }

    public void AttackRangeDetection()
    {
        inRange = true;
    }

    public void DoAttack()
    {
        this.gameObject.TryGetComponent<EnemyMovement>(out EnemyMovement moveScript);
        moveScript.enabled = false;
        AttackArea.SetActive(true);
        moveScript.enabled = true;
    }

    public void DoDamage(GameObject Player)
    {
        Debug.Log("Damage");
        Player.TryGetComponent<Health>(out Health healthValue);
        healthValue.Damage(1);
    }
}
