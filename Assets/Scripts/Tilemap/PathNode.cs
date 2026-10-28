using UnityEngine;

public class PathNode
{
    public int x;
    public int y;
    public bool isWalkable;
    public int gCost, hCost, fCost;
    public PathNode cameFromNode;

    // constructor to initialize the PathNode
    public PathNode(int x, int y)
    {
        this.x = x;
        this.y = y;
        this.isWalkable = true;
    }

    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }
}