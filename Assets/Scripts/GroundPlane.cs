using UnityEngine;

public class GroundPlane : MonoBehaviour
{
    public float boundaryWidth { get; private set; } // Width of the play area (automatically detected)

    private void Start()
    {
        // Automatically detect boundary width based on collider size
        BoxCollider collider = GetComponent<BoxCollider>();
        if (collider != null)
        {
            // Get the size of the collider
            boundaryWidth = collider.size.x;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Your collision handling code here
    }
}
