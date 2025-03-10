using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float distance = 5f;
    [SerializeField]
    private float zoomSpeed = 2f;
    [SerializeField]
    private float minDistance = 2f, maxDistance = 12f;
    [SerializeField]
    private float rotationSpeed = 5f; 

    private float currentRotationX = 45f;
    private float currentRotationY = 0f;

    void LateUpdate()
    {
        if (target == null) return;

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        distance -= scroll * zoomSpeed;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            currentRotationY += mouseX * rotationSpeed;
            currentRotationX -= mouseY * rotationSpeed;
            currentRotationX = Mathf.Clamp(currentRotationX, 15f, 80f); 
        }

        Quaternion rotation = Quaternion.Euler(currentRotationX, currentRotationY, 0);
        Vector3 position = target.position - (rotation * Vector3.forward * distance);

        transform.position = position;
        transform.LookAt(target);
    }
}
