using UnityEngine;



public class BuildingSystem : MonoBehaviour
{
    public GameObject buildingPrefab; // Reference to the building prefab
    public LayerMask groundLayer; // Layer mask for the ground.

    private GameObject previewObject; // Object used for previewing the building placement
    private bool isPlacing = false; // Flag to check if we are currently placing a building

    void Update()
    {
        if (isPlacing)
        {
            GhostBuilding(); // Call the method to show the ghost building
            PlaceBuilding(); // Call the method to place the building
        }

        if (Input.GetKeyDown(KeyCode.B)) // Press 'B' to start placing a building
        {
            //StartPlacingTower();
        }
    }

    private void GhostBuilding()
    {
        if (previewObject == null)
        {
            previewObject = Instantiate(buildingPrefab);
            previewObject.GetComponent<Collider>().enabled = false; // Disable the collider for the preview object
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f, groundLayer))
        {
            Vector3 position = hit.point;
            position.y += 0.5f; // Adjust the height of the building

            previewObject.transform.position = position;
            previewObject.SetActive(true);
        }
    }

    private void PlaceBuilding()
    {
        if (Input.GetMouseButtonDown(0) && previewObject != null)
        {
            // Place the building at the preview object's position
            GameObject building = Instantiate(buildingPrefab, previewObject.transform.position, Quaternion.identity);
            building.GetComponent<Collider>().enabled = true; // Enable the collider for the placed building
            Destroy(previewObject); // Destroy the preview object
            isPlacing = false; // Reset the placing flag
        }
    }

    public void StartPlacingTower(GameObject ghostTowerPrefab)
    {
        isPlacing = true;
        if (previewObject == null)
        {
            previewObject = Instantiate(buildingPrefab);
            previewObject.GetComponent<Collider>().enabled = false;
        }
        previewObject.SetActive(true);
    }



}