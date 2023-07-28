using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour {
    private const int STRAIGHTCOST = 10;
    GridGenerator gridGenerator;
    private List<PathNode> openList;
    private List<PathNode> closedList;

    private void Start() {
        gridGenerator = GetComponent<GridGenerator>();
    }

    public int CalculateDistanceCost(PathNode start, PathNode end) {
        int xDistance = Mathf.Abs(start.x - end.x);
        int zDistance = Mathf.Abs(start.z - end.z);
        int remaining = Mathf.Abs(xDistance - zDistance);
        return STRAIGHTCOST * remaining;
    }

    private PathNode GetLowestFCostNode(List<PathNode> nodes) {
        PathNode lowestFCostNode = nodes[0];
        for (int i = 0; i < nodes.Count; i++) {
            if (nodes[i].fCost < lowestFCostNode.fCost)
                lowestFCostNode = nodes[i];
        }
        return lowestFCostNode;
    }

    private List<PathNode> GetNeighbourList(PathNode currentNode) {
        List<PathNode> neighbourList = new List<PathNode>();
        if (currentNode.x - 1 >= 0&&GetNode(currentNode.x-1,currentNode.z).isWalkable)
            neighbourList.Add(GetNode(currentNode.x - 1, currentNode.z));
        if (currentNode.x + 1 < gridGenerator.width&& GetNode(currentNode.x +1 , currentNode.z).isWalkable)
            neighbourList.Add(GetNode(currentNode.x + 1, currentNode.z));
        if (currentNode.z - 1 >= 0&&GetNode(currentNode.x, currentNode.z-1).isWalkable)
            neighbourList.Add(GetNode(currentNode.x, currentNode.z - 1));
        if (currentNode.z + 1 < gridGenerator.height&& GetNode(currentNode.x, currentNode.z + 1).isWalkable)
            neighbourList.Add(GetNode(currentNode.x, currentNode.z + 1));
        return neighbourList;
    }

    public PathNode GetNode(int x, int z) {
        PathNode node = null;
        for (int i = 0; i < gridGenerator.pathNodes.Count; i++) {
            if (gridGenerator.pathNodes[i].x == x && gridGenerator.pathNodes[i].z == z) {
                node = gridGenerator.pathNodes[i];
                break;
            }
        }
        return node;
    }

    private List<PathNode> CalculatePath(PathNode endNode) {
        List<PathNode> path = new List<PathNode>();
        path.Add(endNode);
        PathNode currentNode = endNode;
        while (currentNode.previousNode != null) {
            path.Add(currentNode.previousNode);
            currentNode = currentNode.previousNode;
        }
        path.Reverse();
        return path;
    }

    public bool FindPath(PathNode startNode, PathNode endNode) {

        if (startNode == endNode) return true;

        openList = new List<PathNode> { startNode };
        closedList = new List<PathNode>();

        for (int x = 0; x < gridGenerator.width; x++) {
            for (int z = 0; z < gridGenerator.height; z++) {
                PathNode pathNode = GetNode(x, z);
                pathNode.gCost = int.MaxValue;
                pathNode.CalculateFCost();
                pathNode.previousNode = null;
            }
        }

        startNode.gCost = 0;
        startNode.hCost = CalculateDistanceCost(startNode, endNode);
        startNode.CalculateFCost();

        while (openList.Count > 0) {
            PathNode currentNode = GetLowestFCostNode(openList);
            if (currentNode == endNode) return true;
            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (PathNode neighbourNode in GetNeighbourList(currentNode)) {
                if (closedList.Contains(neighbourNode)) continue;
                if (!neighbourNode.isWalkable) {
                    closedList.Add(neighbourNode);
                    continue;
                }

                int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbourNode);
                if (tentativeGCost < neighbourNode.gCost) {
                    neighbourNode.previousNode = currentNode;
                    neighbourNode.gCost = tentativeGCost;
                    neighbourNode.hCost = CalculateDistanceCost(neighbourNode, endNode);
                    neighbourNode.CalculateFCost();

                    if (!openList.Contains(neighbourNode)) {
                        openList.Add(neighbourNode);
                    }
                }
            }
        }
        return false;
    }
}