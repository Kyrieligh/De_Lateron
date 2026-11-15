using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class GridPathfinding : MonoBehaviour
{
    public GridCoordinateSystem2D grid;
    private ObstacleManager2D obstacleManager;


    private void Awake()
    {
        if (grid != null)
        {
            obstacleManager = grid.GetComponent<ObstacleManager2D>();
            if (obstacleManager == null)
            {
                Debug.LogWarning("ObstacleManager2D not found on the GridCoordinateSystem2D. Pathfinding may not work correctly.");
            }
        }
    }

    public List<Vector2> FindPathWorld(Vector2 startWorld, Vector2 targetWorld)
    {
        Vector2Int startGrid = grid.WorldToGrid(startWorld);
        Vector2Int endGrid = grid.WorldToGrid(targetWorld);

        return FindPathInternal(startGrid, endGrid);
    }

    public List<Vector2> FindPathGrid(Vector2Int startGrid, Vector2Int endGrid)
    {
        return FindPathInternal(startGrid, endGrid);
    }

    private List<Vector2> FindPathInternal(Vector2Int startGrid, Vector2Int endGrid)
    {
        if (!IsInBounds(startGrid) || !IsInBounds(endGrid)) return null;

        if (obstacleManager != null && obstacleManager.IsObstacle(startGrid))
        {
            startGrid = FindNearestNonObstacle(startGrid);
        }

        // If the endGrid is an obstacle, find the nearest non-obstacle position
        if (obstacleManager != null && obstacleManager.IsObstacle(endGrid))
        {
            endGrid = FindNearestNonObstacle(endGrid);
            if (endGrid == null) return null; // No valid position found
        }

        GridNode startNode = new GridNode(startGrid, grid.GridToWorld(startGrid));
        GridNode endNode = new GridNode(endGrid, grid.GridToWorld(endGrid));

        List<GridNode> openSet = new List<GridNode> { startNode };
        HashSet<Vector2Int> closedSet = new HashSet<Vector2Int>();

        Dictionary<Vector2Int, GridNode> allNodes = new Dictionary<Vector2Int, GridNode>
        {
            [startGrid] = startNode
        };

        startNode.GCost = 0;
        startNode.HCost = Vector2.Distance(startNode.WorldPosition, endNode.WorldPosition);

        while (openSet.Count > 0)
        {
            GridNode currentNode = GetLowestFCostNode(openSet);

            if (currentNode.GridPosition == endGrid)
                return ReconstructPath(currentNode);

            openSet.Remove(currentNode);
            closedSet.Add(currentNode.GridPosition);

            foreach (Vector2Int offset in GetNeighbours(currentNode.GridPosition))
            {
                Vector2Int neighborGrid = currentNode.GridPosition + offset;
                if (closedSet.Contains(neighborGrid) || !IsInBounds(neighborGrid)) continue;
                if (obstacleManager != null && obstacleManager.IsObstacle(neighborGrid)) continue;

                Vector2 worldPos = grid.GridToWorld(neighborGrid);
                float tentativeG = currentNode.GCost + Vector2.Distance(currentNode.WorldPosition, worldPos);

                if (!allNodes.TryGetValue(neighborGrid, out var neighborNode))
                {
                    neighborNode = new GridNode(neighborGrid, worldPos);
                    allNodes[neighborGrid] = neighborNode;
                }

                if (tentativeG < neighborNode.GCost || !openSet.Contains(neighborNode))
                {
                    neighborNode.GCost = tentativeG;
                    neighborNode.HCost = Vector2.Distance(worldPos, endNode.WorldPosition);
                    neighborNode.Parent = currentNode;

                    if (!openSet.Contains(neighborNode))
                        openSet.Add(neighborNode);
                }
            }
        }

        return null;
    }

    private bool IsInBounds(Vector2Int pos)
    {
        return pos.x >= 0 && pos.y >= 0 && pos.x < grid.subdivisions && pos.y < grid.subdivisions;
    }

    private GridNode GetLowestFCostNode(List<GridNode> nodes)
    {
        GridNode best = nodes[0];
        foreach (var node in nodes)
        {
            if (node.FCost < best.FCost || (node.FCost == best.FCost && node.HCost < best.HCost))
                best = node;
        }
        return best;
    }

    private List<Vector2Int> GetNeighbours(Vector2Int currentPos)
    {
        List<Vector2Int> neighbours = new List<Vector2Int>();

        Vector2Int[] straightDirs = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };
        foreach (var dir in straightDirs)
        {
            neighbours.Add(dir);
        }

        // Diagonal hanya diizinkan jika kedua sel lurus di sampingnya tidak terhalang tembok/tabung
        Vector2Int[] diagonalDirs =
        {
        new Vector2Int(1, 1),
        new Vector2Int(-1, 1),
        new Vector2Int(1, -1),
        new Vector2Int(-1, -1)
    };

        foreach (var diag in diagonalDirs)
        {
            Vector2Int checkHorizontal = currentPos + new Vector2Int(diag.x, 0);
            Vector2Int checkVertical = currentPos + new Vector2Int(0, diag.y);

            bool horizontalBlocked = obstacleManager != null && obstacleManager.IsObstacle(checkHorizontal);
            bool verticalBlocked = obstacleManager != null && obstacleManager.IsObstacle(checkVertical);

            if (!horizontalBlocked && !verticalBlocked)
            {
                neighbours.Add(diag);
            }
        }

        return neighbours;
    }

    private List<Vector2> ReconstructPath(GridNode endNode)
    {
        List<Vector2> path = new List<Vector2>();
        GridNode current = endNode;
        while (current != null)
        {
            path.Add(current.WorldPosition);
            current = current.Parent;
        }
        path.Reverse();
        return path;
    }

    private Vector2Int FindNearestNonObstacle(Vector2Int targetGrid)
    {
        int searchRadius = 5; // Adjust as needed
        if (obstacleManager == null) return targetGrid;

        Vector2Int? nearest = obstacleManager.FindNearestNonObstacle(targetGrid, searchRadius, IsInBounds);
        return nearest ?? targetGrid; // Return the original position if no non-obstacle is found
    }
}
