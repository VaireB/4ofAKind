using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    public GameObject waypointPrefab; // Prefab for the waypoint object
    public int maxWaypoints = 4; // Maximum number of waypoints allowed
    public float gridSize = 1f; // Size of the grid cells
    public string gridTag = "Grid"; // Tag for objects representing the grid
    public Transform player; // Reference to the player object
    public float playerMoveSpeed = 5f; // Speed at which the player moves
    public float waypointRadius = 0.5f; // Radius within which the player considers a waypoint reached
    public LineRenderer playerPathRenderer; // Line renderer to visualize the path between player and waypoints

    private List<GameObject> waypoints = new List<GameObject>(); // List to store placed waypoints
    private bool canMove = false; // Flag to indicate if the player can move
    private int currentWaypointIndex = 0; // Index of the current waypoint
    private Vector3 prefabCenterOffset; // Offset to snap to the center of the prefab

    void Start()
    {
        // Calculate the center offset of the prefab
        prefabCenterOffset = waypointPrefab.transform.position - waypointPrefab.GetComponent<Renderer>().bounds.center;

        // Initialize the player path renderer
        playerPathRenderer.positionCount = 2; // Start and end points only
        playerPathRenderer.startWidth = 0.1f; // Set the width of the line
        playerPathRenderer.endWidth = 0.1f;
        playerPathRenderer.enabled = false; // Initially disable the renderer
    }

    void Update()
    {
        // Check if the maximum number of waypoints has been reached
        if (waypoints.Count >= maxWaypoints)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Enable player movement
                canMove = true;
                currentWaypointIndex = 0; // Start from the first waypoint

                // Enable the player path renderer
                playerPathRenderer.enabled = true;
            }
        }
        else
        {
            // Check for left mouse button click to place a waypoint
            if (Input.GetMouseButtonDown(0))
            {
                // Raycast from the mouse cursor to the ground
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    // Check if the hit object has the specified tag
                    if (hit.collider.CompareTag(gridTag))
                    {
                        // Snap the hit position to the nearest grid cell center
                        Vector3 snappedPosition = SnapToGrid(hit.point);

                        // Place a waypoint at the snapped position
                        GameObject newWaypoint = Instantiate(waypointPrefab, snappedPosition, Quaternion.identity);
                        waypoints.Add(newWaypoint);

                        // Update the player path renderer
                        UpdatePlayerPath();
                    }
                }
            }

            // Check for right mouse button click to remove the last placed waypoint
            if (Input.GetMouseButtonDown(1))
            {
                if (waypoints.Count > 0)
                {
                    // Remove the last placed waypoint
                    GameObject lastWaypoint = waypoints[waypoints.Count - 1];
                    Destroy(lastWaypoint);
                    waypoints.RemoveAt(waypoints.Count - 1);

                    // Update the player path renderer
                    UpdatePlayerPath();
                }
            }
        }

        // Move the player if canMove is true
        if (canMove)
        {
            MovePlayerTowardsWaypoint();
        }
    }

    private void MovePlayerTowardsWaypoint()
    {
        if (currentWaypointIndex < waypoints.Count)
        {
            // Calculate the direction towards the current waypoint
            Vector3 direction = waypoints[currentWaypointIndex].transform.position - player.position;

            // Check if the player has reached the current waypoint
            if (direction.magnitude <= waypointRadius)
            {
                // Remove the reached waypoint
                GameObject reachedWaypoint = waypoints[currentWaypointIndex];
                waypoints.RemoveAt(currentWaypointIndex);
                Destroy(reachedWaypoint);

                // Update the player path renderer
                UpdatePlayerPath();

                // Move to the next waypoint
                // Note: We don't increment currentWaypointIndex here because removing the reached waypoint automatically shifts the index to the next one
            }
            else
            {
                // Normalize the direction and move the player
                direction.Normalize();
                player.Translate(direction * playerMoveSpeed * Time.deltaTime);
            }
        }
        else
        {
            // Reset movement when all waypoints have been reached
            canMove = false;
            currentWaypointIndex = 0;

            // Disable the player path renderer
            playerPathRenderer.enabled = false;
        }
    }

    private Vector3 SnapToGrid(Vector3 position)
    {
        // Round the position to the nearest grid cell
        float x = Mathf.Round(position.x / gridSize) * gridSize;
        float y = Mathf.Round(position.y / gridSize) * gridSize;
        float z = Mathf.Round(position.z / gridSize) * gridSize;

        // Offset the snapped position to the center of the prefab
        return new Vector3(x, y, z) + prefabCenterOffset;
    }

    private void UpdatePlayerPath()
    {
        // Set the positions of the LineRenderer vertices for the player path
        playerPathRenderer.SetPosition(0, player.position); // Start point is the player position
        playerPathRenderer.SetPosition(1, waypoints[currentWaypointIndex].transform.position); // End point is the position of the current waypoint
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider is a trigger and if it has the Coin tag
        if (other.CompareTag("Coin"))
        {
            // Remove the coin from the scene
            Destroy(other.gameObject);
        }
    }
}
