using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 15f;
    private GameObject target;
    private float damage;

    public void Init(GameObject target, float damage)
    {
        this.target = target;
        this.damage = damage;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 targetPos = target.transform.position;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPos) < 0.1f)
        {
            StatsComponent s = target.GetComponentInParent<StatsComponent>();
            if (s != null) s.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}