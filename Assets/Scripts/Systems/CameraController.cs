using UnityEngine;

public class TopDownCameraController : MonoBehaviour
{
    [Header("Pan Settings")]
    public float panSpeed = 20f;
    public Vector2 panLimitX = new Vector2(-30f, 15f);
    public Vector2 panLimitZ = new Vector2(-50f, 20f);

    [Header("Zoom Settings")]
    public float zoomSpeed = 15f;
    public float minY = 5f;
    public float maxY = 20f;

    private Vector3 lastMousePosition;

    void Update()
    {
        HandlePan();
        HandleZoom();
        ClampPosition();
    }

    void HandlePan()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;
            Vector3 move = new Vector3(-delta.x, 0, -delta.y) * panSpeed * Time.deltaTime;

            transform.Translate(move, Space.World);
            lastMousePosition = Input.mousePosition;
        }
    }

 void HandleZoom()
{
    float scroll = Input.GetAxis("Mouse ScrollWheel");
    Camera cam = Camera.main;

    if (cam.orthographic)
    {
        cam.orthographicSize -= scroll * zoomSpeed;
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minY, maxY);
    }
    else
    {
        Vector3 position = transform.position;
        position.y -= scroll * zoomSpeed * 100 * Time.deltaTime;
        position.y = Mathf.Clamp(position.y, minY, maxY);
        transform.position = position;
    }
}


    void ClampPosition()
    {
        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, panLimitX.x, panLimitX.y);
        position.z = Mathf.Clamp(position.z, panLimitZ.x, panLimitZ.y);
        transform.position = position;
    }
}
