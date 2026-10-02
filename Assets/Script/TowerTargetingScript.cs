using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class TowerTargetScript : MonoBehaviour
{
    public List<GameObject> targetList = new List<GameObject>();

    private Tower towerScript;
    void Start()
    {
        towerScript = GetComponentInParent<Tower>();
    }

    void Update()
    {
        targetList.RemoveAll(t => t == null);

        if (targetList.Count > 0)
        {
            GameObject closestTarget = null;
            float closestDistance = Mathf.Infinity;

            foreach (GameObject target in targetList)
            {
                float distance = Vector3.Distance(transform.position, target.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = target;
                }
            }

            towerScript.target = closestTarget;
            towerScript.hasTarget = true;
        }
        else if (towerScript.hasTarget)
        {
            towerScript.target = null;
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        List<int> enemyLayer = new List<int>();

        if (towerScript.isBlue)
        {
            enemyLayer.Add(10);
            enemyLayer.Add(15);
        }
        else
        {
            enemyLayer.Add(9);
            enemyLayer.Add(14);
        }

        for (int i = 0; i < enemyLayer.Count; i++)
        {
            if (other.gameObject.layer == enemyLayer[i] && !targetList.Contains(other.gameObject))
            {
                targetList.Add(other.gameObject);
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        targetList.Remove(other.gameObject);
    }
}
