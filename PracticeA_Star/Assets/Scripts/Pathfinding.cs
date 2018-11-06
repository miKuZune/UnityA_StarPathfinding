using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour {

    Grid grid;
    public Transform startPos;
    public Transform targetPos;

    private void Awake()
    {
        grid = GetComponent<Grid>();
    }

    private void Update()
    {
        FindPath(startPos.position, targetPos.position);
    }

    void FindPath(Vector3 a_StartPos, Vector3 a_TargetPos)
    {
        Node StartNode = grid.NodeFromWorldPosition(a_StartPos);
        Node TargetNode = grid.NodeFromWorldPosition(a_TargetPos);

        List<Node> openList = new List<Node>();
        HashSet<Node> ClosedList = new HashSet<Node>();

        openList.Add(StartNode);

        while(openList.Count > 0)
        {
            Node currNode = openList[0];
            for(int i = 1; i < openList.Count; i++)
            {
                if(openList[i].FCost <= currNode.FCost && openList[i].hCost < currNode.hCost)
                {
                    currNode = openList[i];
                }
            }
            openList.Remove(currNode);
            ClosedList.Add(currNode);

            if(currNode == TargetNode)
            {
                GetFinalPath(StartNode, TargetNode);
            }

            foreach(Node NeighborNode in grid.GetNeighboringNodes(currNode))
            {
                if(!NeighborNode.IsWall || ClosedList.Contains(NeighborNode))
                {
                    continue;
                }
                int moveCost = currNode.gCost + GetManhattenDistance(currNode, NeighborNode);

                if(moveCost < NeighborNode.gCost || !openList.Contains(NeighborNode))
                {
                    NeighborNode.gCost = moveCost;
                    NeighborNode.hCost = GetManhattenDistance(NeighborNode, TargetNode);
                    NeighborNode.parent = currNode;

                    if(!openList.Contains(NeighborNode))
                    {
                        openList.Add(NeighborNode);
                    }
                }
            }
        }
    }

    void GetFinalPath(Node startNode, Node targetNode)
    {
        List<Node> FInalPath = new List<Node>();
        Node currNode = targetNode;

        while(currNode != startNode)
        {
            FInalPath.Add(currNode);
            currNode = currNode.parent;
        }

        FInalPath.Reverse();

        grid.FinalPath = FInalPath;
    }

    int GetManhattenDistance(Node nodeA, Node nodeB)
    {
        int ix = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int iy = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        return ix + iy;
    }
}
