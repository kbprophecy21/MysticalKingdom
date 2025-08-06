using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform cameraTransform;

    // Boundaries based on 60x60 map
    public float minX = 0f;
    public float maxX = 60f;
    public float minZ = 0f;
    public float maxZ = 60f;

    // Zoom limits
    public float minZoom = 20f;
    public float maxZoom = 100f;
    public float zoomSpeed = 10f;

    // Pan speed
    public float panSpeed = 1f;

    private Vector3 lastMousePosition;

    void Update()
    {
        HandlePan();
        HandleZoom();
        ClampCameraPosition();
    }

    void HandlePan()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;
            Vector3 move = new Vector3(-delta.x * panSpeed * Time.deltaTime, 0, -delta.y * panSpeed * Time.deltaTime);

            cameraTransform.Translate(move, Space.World);
            lastMousePosition = Input.mousePosition;
        }
    }

    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 pos = cameraTransform.position;
        pos.y -= scroll * zoomSpeed;
        pos.y = Mathf.Clamp(pos.y, minZoom, maxZoom);
        cameraTransform.position = pos;
    }

    void ClampCameraPosition()
    {
        Vector3 pos = cameraTransform.position;

        // Optional: clamp based on current zoom to avoid seeing outside map
        float buffer = 5f;
        pos.x = Mathf.Clamp(pos.x, minX + buffer, maxX - buffer);
        pos.z = Mathf.Clamp(pos.z, minZ + buffer, maxZ - buffer);

        cameraTransform.position = pos;
    }
}
