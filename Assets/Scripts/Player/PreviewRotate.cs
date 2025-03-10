using UnityEngine;
using UnityEngine.EventSystems;

public class PreviewRotate : MonoBehaviour
{
    [SerializeField] private Transform previewModel;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private RectTransform previewArea; 

    private bool isDragging = false;

    void Update()
    {
        bool isMouseOverPreview = RectTransformUtility.RectangleContainsScreenPoint(previewArea, Input.mousePosition);

        if (isMouseOverPreview && Input.GetMouseButtonDown(0))
            isDragging = true;
        if (Input.GetMouseButtonUp(0))
            isDragging = false;

        if (isDragging)
        {
            float rotX = Input.GetAxis("Mouse X") * rotationSpeed;
            previewModel.Rotate(Vector3.up, -rotX);
        }
    }
}
