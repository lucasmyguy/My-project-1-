using UnityEngine;
using UnityEngine.AI;


public class NavMeshPlayer : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] UIManager uiManager;
    public StatsComponent stats;
    public Camera cam;
    public NavMeshAgent agent;
    public bool isBlue = true;

    private int blueMinionLayer = 9;
    private int redMinionLayer = 10;
    private int groundLayer = 11;
    private int blueTowerLayer = 12;
    private int redTowerLayer = 13;

    private Collider targetCollider;
    private bool hasTarget;
    private float attackTimer;

    private void Awake()
    {
        stats = GetComponentInParent<StatsComponent>();
        agent = GetComponentInParent<NavMeshAgent>();
        attackTimer = 1 / stats.attackSpd;
        agent.autoBraking = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            HandleRightClick();
        }

        HandleAttack();

        HpChanged();


    }

    bool IsEnemy(int layer)
    {
        if (isBlue) return layer == redMinionLayer || layer == redTowerLayer;
        return layer == blueMinionLayer || layer == blueTowerLayer;
    }

    void HandleRightClick()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);


        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, ~0, QueryTriggerInteraction.Ignore))
        {
            return;
        }

        int layer = hit.collider.gameObject.layer;

        if (IsEnemy(layer))
        {
            targetCollider = hit.collider;
            hasTarget = true;
            agent.stoppingDistance = stats.range;
        }
        else if (layer == groundLayer || layer == blueMinionLayer || layer == redMinionLayer)
        {
            hasTarget = false;
            agent.stoppingDistance = 0f;
            agent.SetDestination(hit.point);
        }
    }

    void HandleAttack()
    {
        attackTimer -= Time.deltaTime;

        if (!hasTarget) return;

        if (targetCollider == null)
        {
            hasTarget = false;
            agent.stoppingDistance = 0f;
            agent.ResetPath();
            return;
        }

        Vector3 closest = targetCollider.ClosestPoint(transform.position);
        float distance = Vector3.Distance(transform.position, closest);

        agent.SetDestination(closest);

        if (distance <= stats.range && attackTimer <= 0f)
        {
            Attack();
            attackTimer = 1f / stats.attackSpd;
        }
    }

    void Attack()
    {
        if (projectilePrefab == null)
        {
            targetCollider.GetComponent<StatsComponent>().TakeDamage(stats.attackDamage);
        }
        else
        {
            GameObject p = Instantiate(projectilePrefab, transform.position + Vector3.up, Quaternion.identity);
            p.GetComponent<Projectile>().Init(targetCollider.gameObject, stats.attackDamage);
        }
    }

    void HpChanged()
    {
        uiManager.UpdateHp(stats.health);
    }
}