using UnityEngine;

public class BorderManager : MonoBehaviour
{
    public float borderThickness = 0.1f; // Thickness of the border colliders
    public LayerMask playerLayer; // Layer mask to filter collision with the player

    void Start()
    {
        CreateBorders();
    }

    void CreateBorders()
    {
        // Find the ground object
        GameObject ground = GameObject.FindWithTag("Ground");
        if (ground == null)
        {
            Debug.LogError("No ground object found with the 'Ground' tag.");
            return;
        }

        // Get the mesh renderer and mesh filter of the ground object
        MeshRenderer groundRenderer = ground.GetComponent<MeshRenderer>();
        MeshFilter groundMeshFilter = ground.GetComponent<MeshFilter>();
        if (groundRenderer == null || groundMeshFilter == null)
        {
            Debug.LogError("Mesh renderer or mesh filter not found on the ground object.");
            return;
        }

        // Get the size of the ground mesh
        Mesh groundMesh = groundMeshFilter.mesh;
        Vector3 groundSize = groundMesh.bounds.size;

        // Adjust size based on border thickness
        groundSize.x += borderThickness * 2f;
        groundSize.z += borderThickness * 2f;

        // Create border colliders around the ground
        CreateBorder(Vector3.left * 0.5f * groundSize.x, new Vector3(borderThickness, groundSize.y, groundSize.z)); // Left border
        CreateBorder(Vector3.right * 0.5f * groundSize.x, new Vector3(borderThickness, groundSize.y, groundSize.z)); // Right border
        CreateBorder(Vector3.forward * 0.5f * groundSize.z, new Vector3(groundSize.x, groundSize.y, borderThickness)); // Front border
        CreateBorder(Vector3.back * 0.5f * groundSize.z, new Vector3(groundSize.x, groundSize.y, borderThickness)); // Back border
    }

    void CreateBorder(Vector3 position, Vector3 size)
    {
        // Create a new GameObject for the border collider
        GameObject border = new GameObject("Border");
        border.transform.position = transform.position + position;

        // Add a BoxCollider component to the border GameObject
        BoxCollider collider = border.AddComponent<BoxCollider>();
        collider.isTrigger = true; // Set the collider as a trigger to avoid physical interactions
        collider.size = size;

        // Ensure that the border only interacts with the player layer
        collider.gameObject.layer = playerLayer.value;
    }
}
