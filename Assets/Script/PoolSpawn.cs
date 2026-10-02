using System.Collections.Generic;
using System.ComponentModel.Design;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.Pool;

public class PoolSpawn : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int amount = 500;

    private ObjectPool<GameObject> pool;

    private readonly List<GameObject> activeObjects = new List<GameObject>();

    private static readonly ProfilerMarker marker = new ProfilerMarker("DEMO - Pool Get Burst");

    private void Awake()
    {
        pool = new ObjectPool<GameObject>(
            CreateObject,
            OnGet,
            OnRelease,
            OnDestroyObject,
            true,
            amount,
            amount * 2
        );

        Prewarm();
    }

    private void Prewarm()
    {
        List<GameObject> objects =
            new List<GameObject>();

        for (int i = 0; i < amount; i++)
        {
            objects.Add(pool.Get());
        }

        foreach (GameObject obj in objects)
        {
            pool.Release(obj);
        }

        Debug.Log("Pool ready: " + amount);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetBurst();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ReturnEverything();
        }
    }

    private void GetBurst()
    {
        using (marker.Auto())
        {
            for (int i = 0; i < amount; i++)
            {
                GameObject obj = pool.Get();

                obj.transform.position =
                    Random.insideUnitSphere * 5f;

                activeObjects.Add(obj);
            }
        }
    }

    private void ReturnEverything()
    {
        foreach (GameObject obj in activeObjects)
        {
            pool.Release(obj);
        }

        activeObjects.Clear();
    }

    private GameObject CreateObject()
    {
        return Instantiate(prefab);
    }

    private void OnGet(GameObject obj)
    {
        obj.SetActive(true);
    }

    private void OnRelease(GameObject obj)
    {
        obj.SetActive(false);
    }

    private void OnDestroyObject(GameObject obj)
    {
        Destroy(obj);
    }
}
