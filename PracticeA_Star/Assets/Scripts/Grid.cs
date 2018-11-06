using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    public Transform StartPos;
    public LayerMask WallMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    public float Distance;

    Node[,] grid;
    public List<Node> FinalPath;

    float nodeDiameter;
    int gridSizeX, gridSizeY;

    private void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        Debug.Log(nodeDiameter);
        CreateGrid();
    }

    public List<Node> GetNeighboringNodes(Node node)
    {
        List<Node> NeighboringNodes = new List<Node>();
        int xCheck;
        int yCheck;

        //Right side
        xCheck = node.gridX + 1;
        yCheck = node.gridY;

        if(xCheck >= 0 && xCheck < gridSizeX)
        {
            if(yCheck >= 0 && yCheck < gridSizeY)
            {
                NeighboringNodes.Add(grid[xCheck, yCheck]);
            }
        }
        //Left side
        xCheck = node.gridX - 1;
        yCheck = node.gridY;

        if (xCheck >= 0 && xCheck < gridSizeX)
        {
            if (yCheck >= 0 && yCheck < gridSizeY)
            {
                NeighboringNodes.Add(grid[xCheck, yCheck]);
            }
        }
        //Up side
        xCheck = node.gridX;
        yCheck = node.gridY + 1;

        if (xCheck >= 0 && xCheck < gridSizeX)
        {
            if (yCheck >= 0 && yCheck < gridSizeY)
            {
                NeighboringNodes.Add(grid[xCheck, yCheck]);
            }
        }
        //Down side
        xCheck = node.gridX;
        yCheck = node.gridY - 1;

        if (xCheck >= 0 && xCheck < gridSizeX)
        {
            if (yCheck >= 0 && yCheck < gridSizeY)
            {
                NeighboringNodes.Add(grid[xCheck, yCheck]);
            }
        }

        return NeighboringNodes;
    }

    public Node NodeFromWorldPosition(Vector3 a_WorldPos)
    {
        float xPos = ((a_WorldPos.x + a_WorldPos.x / 2) / gridWorldSize.x);
        float yPos = ((a_WorldPos.x + a_WorldPos.z / 2) / gridWorldSize.y);

        xPos = Mathf.Clamp01(xPos);
        yPos = Mathf.Clamp01(yPos);

        int x = Mathf.RoundToInt((gridSizeX - 1) * xPos);
        int y = Mathf.RoundToInt((gridSizeY - 1) * yPos);

        return grid[x, y];
    }

    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 bottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

        for(int y = 0; y < gridSizeX; y++)
        {
            for(int x = 0; x < gridSizeX; x++)
            {
                Vector3 worldPoint = bottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                bool Wall = true;

                if(Physics.CheckSphere(worldPoint, nodeRadius, WallMask))
                {
                    Wall = false;
                }

                grid[x, y] = new Node(Wall, worldPoint, x, y);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));

        if(grid != null)
        {
            foreach(Node node in grid)
            {
                if(node.IsWall)
                {
                    Gizmos.color = Color.white;
                }
                else
                {
                    Gizmos.color = Color.blue;
                }

                if(FinalPath != null)
                {
                    if(FinalPath.Contains(node))
                    {
                        Gizmos.color = Color.red;
                    }
                }

                Gizmos.DrawCube(node.Position, Vector3.one * (nodeDiameter - Distance));
            }
        }
    }
}
