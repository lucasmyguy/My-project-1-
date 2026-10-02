using System.Collections.Generic;
using UnityEngine;

public class MoveObstacles : MonoBehaviour
{
    public float moveSpeed;
    public List<Transform> pointList;

    private int targetPointIndex = 0;
    private Transform ownTransform;
    private Vector3 direction;
    void Start()
    {
        ownTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if ((pointList[targetPointIndex].position - ownTransform.position).magnitude < 0.01f)
        {
            targetPointIndex++;
            if (targetPointIndex >= pointList.Count)
            {
                targetPointIndex = 0;
            }
        }

        direction = pointList[targetPointIndex].position - ownTransform.position;
        ownTransform.position += direction.normalized * moveSpeed * Time.deltaTime;

    }
}
