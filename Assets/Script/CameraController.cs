using UnityEngine;

public class MobaCameraController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 20f;
    public float edgeScrollThreshold = 15f; 
    public bool useEdgeScrolling = true;

    [Header("Zoom")]
    public Transform cameraTransform;
    public float zoomSpeed = 50f;
    public float minZoomDist = 15f;
    public float maxZoomDist = 35f;

    [Header("Boundaries")]
    public bool useBoundaries = true;
    public float minX = -50f;
    public float maxX = 50f;
    public float minZ = -50f;
    public float maxZ = 50f;


    [Header("Player")]
    public Transform playerTransform;

    private void Start()
    {
        if (cameraTransform == null && transform.childCount > 0)
        {
            cameraTransform = transform.GetChild(0);
        }
    }

    private void Update()
    {
        HandleMovement();
    }


    private void HandleMovement()
    {
        Vector3 screenInput = Vector3.zero;

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (horizontal != 0 || vertical != 0)
        {
            screenInput = new Vector3(horizontal, 0, vertical).normalized;
        }
        else if (useEdgeScrolling)
        {
            Vector3 mousePos = Input.mousePosition;

            if (mousePos.x >= Screen.width - edgeScrollThreshold) screenInput.x = 1;
            else if (mousePos.x <= edgeScrollThreshold) screenInput.x = -1;

            if (mousePos.y >= Screen.height - edgeScrollThreshold) screenInput.z = 1;
            else if (mousePos.y <= edgeScrollThreshold) screenInput.z = -1;

            screenInput.Normalize();
        }

        if (screenInput != Vector3.zero)
        {
            Vector3 camForward = cameraTransform.forward;
            Vector3 camRight = cameraTransform.right;

            camForward.y = 0;
            camRight.y = 0;

            camForward.Normalize();
            camRight.Normalize();

            Vector3 moveDirection = (camRight * screenInput.x) + (camForward * screenInput.z);
            moveDirection.Normalize();

            Vector3 newPosition = transform.position + (moveDirection * moveSpeed * Time.deltaTime);

            if (useBoundaries)
            {
                newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
                newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);
            }

            transform.position = newPosition;
        }
    }
}
