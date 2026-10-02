using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Tower : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    public bool isBlue;
    public GameObject target;
    public bool hasTarget;

    private Animator Animator;
    private StatsComponent stats;

    private string spawn = "isSpawned";


    private bool canAttack = true;
    private float timerSinceLastattacked;



    private void Awake()
    {
        stats = GetComponentInParent<StatsComponent>();
        timerSinceLastattacked = 1 / stats.attackSpd;

    }
    void Start()
    {
        Animator = GetComponent<Animator>();
        Animator.Play(spawn);
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
            return;
        }

        if (canAttack)
        {
            Attack();
            canAttack = false;
            ResetAtkSpdTimer();
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
            target.GetComponent<StatsComponent>().TakeDamage(stats.attackDamage);
        }
        else
        {
            GameObject p = Instantiate(projectilePrefab, transform.position + Vector3.up, Quaternion.identity);
            p.GetComponent<Projectile>().Init(target, stats.attackDamage);
        }
    }
}

