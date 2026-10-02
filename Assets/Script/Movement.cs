using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            move += Vector3.forward;
        }

        if (Input.GetKey(KeyCode.S))
        {
            move += Vector3.back;
        }

        if (Input.GetKey(KeyCode.A))
        {
            move += Vector3.left;
        }

        if (Input.GetKey(KeyCode.D))
        {
            move += Vector3.right;
        }

        transform.position += move * moveSpeed * Time.deltaTime;
    }

}