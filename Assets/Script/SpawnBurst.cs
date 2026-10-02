using Unity.Profiling;
using UnityEngine;

public class SpawnBurst : MonoBehaviour
{
    [SerializeField] private GameObject prefab;

    [SerializeField] private int amount = 500;

    private static readonly ProfilerMarker marker = new ProfilerMarker("Demo - Instantiate Burst");

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        using (marker.Auto())
        {
            for (int i = 0; i < amount; i++)
            {
                GameObject obj = Instantiate(prefab);

                obj.transform.position = Random.insideUnitSphere * 5f;

                Destroy(obj, 0.5f);
            }
        }
    }
}
