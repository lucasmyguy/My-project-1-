using UnityEngine;

public class collectible : MonoBehaviour
{
    public Vector3 direction;
    public float moveSpeed;
    public float timer;
    public float fixedTimer = 3;
    public float raycastDistance = 3f;
    public LayerMask wallMask;
  
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            scoreManager.Instance.AddScore();
            Destroy(gameObject);
        }

        //if (other.CompareTag("Wall"))
        //{
        //    Vector3 wallNormal = other.transform.right;

        //    direction = Vector3.Reflect(direction, wallNormal);
            
        //}
    }

    

    void Start()
    {
        changeDirection();
    }

    void changeDirection()
    {
        direction = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        timer = fixedTimer;
    }

    void Update()
    {
        //transform.position = direction * moveSpeed * Time.deltaTime;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, direction, out hit, raycastDistance, wallMask))
        {
            direction = Vector3.Reflect(direction, hit.normal);
        }


        transform.Translate(direction * moveSpeed * Time.deltaTime);

        if (timer < 0)
        {
            changeDirection();  
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    void OnDrawGizmos()
    {
        RaycastHit hit;
        bool isHit = Physics.Raycast(transform.position, direction, out hit, raycastDistance);

        Gizmos.color = isHit ? Color.yellow : Color.green;
        Gizmos.DrawRay(transform.position, direction * (isHit ? hit.distance : raycastDistance));
    }
}