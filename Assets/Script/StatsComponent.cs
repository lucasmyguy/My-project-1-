using UnityEngine;

public class StatsComponent : MonoBehaviour
{
    public float maxHealth = 100;
    public float attackSpd = 1.0f;
    public float attackDamage = 30;
    public float armor = 20;
    public float range = 1.25f;

    public float health;

    private void Awake()
    {
        health = maxHealth;
    }

    public void TakeDamage(float rawDamage)
    {
        float damage = rawDamage * 100f / (100f + armor);
        health -= damage;

        if (health < 0)
        {
            Destroy(gameObject);
        }
    }

}
