using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class ObstacleManager2D : MonoBehaviour
{
    private HashSet<Vector2Int> obstacles = new HashSet<Vector2Int>();
    public LayerMask obstacleLayer; // Centang layer Wall dan Obstacle di Inspector

    private GridCoordinateSystem2D grid;

    private void Awake()
    {
        grid = GetComponent<GridCoordinateSystem2D>();
    }

    private void Start()
    {
        BakeObstaclesFromColliders();
    }

    [SerializeField] private float obstaclePadding = 1.6f; // Perbesar jika badan boss masih menyenggol sudut pilar

    [ContextMenu("Bake Obstacles")]
    public void BakeObstaclesFromColliders()
    {
        if (grid == null) grid = GetComponent<GridCoordinateSystem2D>();
        if (grid == null) return;

        Physics2D.SyncTransforms();
        obstacles.Clear();
        Vector2 boxSize = Vector2.one * (grid.CellSize * obstaclePadding);

        for (int x = 0; x < grid.subdivisions; x++)
        {
            for (int y = 0; y < grid.subdivisions; y++)
            {
                Vector2Int gridPos = new Vector2Int(x, y);
                Vector2 worldPos = grid.GridToWorld(gridPos);

                if (Physics2D.OverlapBox(worldPos, boxSize, 0f, obstacleLayer) != null)
                {
                    SetObstacle(gridPos, true);
                }
            }
        }
    }

    public void SetObstacle(Vector2Int position, bool isObstacle)
    {
        if (isObstacle) obstacles.Add(position);
        else obstacles.Remove(position);
    }

    public bool IsObstacle(Vector2Int position) => obstacles.Contains(position);
    public IEnumerable<Vector2Int> GetAllObstacles() => obstacles;

    public Vector2Int? FindNearestNonObstacle(Vector2Int targetGrid, int searchRadius, System.Func<Vector2Int, bool> isInBounds)
    {
        for (int radius = 1; radius <= searchRadius; radius++)
        {
            for (int x = -radius; x <= radius; x++)
            {
                for (int y = -radius; y <= radius; y++)
                {
                    Vector2Int candidate = targetGrid + new Vector2Int(x, y);
                    if (isInBounds(candidate) && !IsObstacle(candidate))
                    {
                        return candidate;
                    }
                }
            }
        }
        return null;
    }
}