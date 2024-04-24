using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public int gridSize = 10; // Number of grid cells along each axis
    public float cellSize = 1f; // Size of each grid cell
    public Color gridColor = Color.white; // Color of the grid lines

    private void OnDrawGizmos()
    {
        DrawGrid();
    }

    void DrawGrid()
    {
        Vector3 bottomLeft = transform.position - new Vector3(gridSize * cellSize / 2f, 0, gridSize * cellSize / 2f);

        // Generate horizontal grid lines
        for (int i = 0; i <= gridSize; i++)
        {
            Vector3 startPos = bottomLeft + new Vector3(0, 0, i * cellSize);
            Vector3 endPos = startPos + new Vector3(gridSize * cellSize, 0, 0);
            Gizmos.color = gridColor;
            Gizmos.DrawLine(startPos, endPos);
        }

        // Generate vertical grid lines
        for (int i = 0; i <= gridSize; i++)
        {
            Vector3 startPos = bottomLeft + new Vector3(i * cellSize, 0, 0);
            Vector3 endPos = startPos + new Vector3(0, 0, gridSize * cellSize);
            Gizmos.color = gridColor;
            Gizmos.DrawLine(startPos, endPos);
        }
    }
}
