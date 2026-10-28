using UnityEngine;

public class PathFindingGrid : MonoBehaviour
{
    [Header("Grid Settings")]
    public int width = 20;
    public int height = 20;
    public float cellSize = 1f;
    public LayerMask obstacleLayer; // obtacle layer

    private PathNode[,] gridNodes;

    private void Awake()
    {
        CreateGrid();
    }

    private void CreateGrid()
    {
        gridNodes = new PathNode[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                gridNodes[x, y] = new PathNode(x, y);

                // Cek fisika di koordinat dunia
                Vector2 worldPos = new Vector2(x, y);
                if (Physics2D.OverlapCircle(worldPos, 0.4f, obstacleLayer))
                {
                    gridNodes[x, y].isWalkable = false; // if there have wall or obstacle, set to false
                }
            }
        }

        Debug.Log("Grid A* Physics berhasil dibuat!");
    }

    public PathNode GetNode(int x, int y)
    {
        if (x >= 0 && x < width && y >= 0 && y < height)
        {
            return gridNodes[x, y];
        }
        return null;
    }

    private void OnDrawGizmos()
    {
        if (gridNodes == null) return;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Gizmos.color = gridNodes[x, y].isWalkable ? new Color(0, 1, 0, 0.3f) : new Color(1, 0, 0, 0.5f);
                Gizmos.DrawCube(new Vector3(x * cellSize + cellSize / 2f, y * cellSize + cellSize / 2f, 0), Vector3.one * (cellSize * 0.9f));
            }
        }
    }
}