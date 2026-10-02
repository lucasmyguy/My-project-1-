using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class GemManager : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask gemLayer;
    [SerializeField] private Gem gemPrefab;

    private IObjectPool<Gem> pool;

    private void Awake()
    {
        pool = new ObjectPool<Gem>(CreateItem, OnGet, OnRelease);
    }

    void Start()
    {

    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            pool.Get();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            bool targetHit = Physics.Raycast(ray, out hit, 55f, gemLayer);
            if (targetHit)
            {
                hit.collider.GetComponent<Gem>().Collect();
            }
        }
    }

    private Gem CreateItem()
    {
        Gem item = Instantiate(gemPrefab);
        item.SetPool(pool);
        return item;
    }

    private void OnGet(Gem item)
    {
        item.gameObject.SetActive(true);
    }

    private void OnRelease(Gem item)
    {
        item.gameObject.SetActive(false);
    }
}
