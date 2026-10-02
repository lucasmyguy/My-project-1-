using UnityEngine;

public class spawnManager : MonoBehaviour
{
    public GameObject collectible;

    void Start()
    {
        Instantiate(collectible, new Vector3(collectible.transform.position.x + 1f, collectible.transform.position.y, 
                    collectible.transform.position.z), Quaternion.identity);
    }

}