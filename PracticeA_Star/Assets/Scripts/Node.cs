using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {

    public int gridX;
    public int gridY;

    public bool IsWall;
    public Vector3 Position;

    public Node parent;

    public int gCost;
    public int hCost;

    public int FCost { get { return gCost + hCost; } }

    public Node(bool IsWall, Vector3 a_Pos, int gridX, int gridY)
    {
        this.IsWall = IsWall;
        this.Position = a_Pos;
        this.gridX = gridX;
        this.gridY = gridY;
    }
}
