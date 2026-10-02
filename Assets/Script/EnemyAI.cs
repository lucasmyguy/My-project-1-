using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Material blue;
    [SerializeField] private Material red;
    [SerializeField] private GameObject projectilePrefab;
    public bool isBlue;

    private Vector3 blueSpawnLocation = new Vector3(-50, 1, 0);
    private Vector3 redSpawnLocation = new Vector3(50, 1, 0);

    private Renderer rend;
    private NavMeshAgent agent;
    private StatsComponent stats;

    public GameObject target;
    public bool hasTarget = false;
    
    private float timerSinceLastattacked;
    private bool canAttack = true;


    private void Awake()
    {
        agent = this.GetComponent<NavMeshAgent>();
        rend = this.GetComponentInChildren<Renderer>();
        stats = this.GetComponentInParent<StatsComponent>();
        ResetAtkSpdTimer();
    }
    public void setup(bool isBlue)
    {
        this.isBlue = isBlue;
        rend.material = isBlue ? blue : red;
        gameObject.layer = isBlue ? 9 : 10;
        MoveToEnemyBase();
    }

    public void MoveToEnemyBase()
    {
        agent.SetDestination(isBlue ? redSpawnLocation : blueSpawnLocation);
    }

    void Update()
    {
        if (!canAttack)
        {
            timerSinceLastattacked -= Time.deltaTime;
            if (timerSinceLastattacked <= 0)
            {
                canAttack = true;
            }
        }

        if (!hasTarget) return;

        if (target == null)
        {
            hasTarget = false;
            MoveToEnemyBase();
            return;
        }

        float distance = Vector3.Distance(transform.position, target.transform.position);

        if (distance > stats.range)
        {
            agent.SetDestination(target.transform.position);
        }
        else
        {
            agent.ResetPath();

            if (canAttack)
            {
                Attack();
                canAttack = false;
                ResetAtkSpdTimer();
            }
        }
    }

    void ResetAtkSpdTimer()
    {
        timerSinceLastattacked = 1 / stats.attackSpd;
    }
    
    void Attack()
    {
        if (projectilePrefab == null)
        {
            StatsComponent targetStats = target.GetComponentInParent<StatsComponent>();
            if (targetStats != null)
            {
                targetStats.TakeDamage(stats.attackDamage);
            }
        }
        else
        {
            GameObject p = Instantiate(projectilePrefab, transform.position + Vector3.up, Quaternion.identity);
            p.GetComponent<Projectile>().Init(target, stats.attackDamage);
        }
    }
}