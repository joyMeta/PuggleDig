using UnityEngine;

[System.Serializable]
public class PathNode:MonoBehaviour
{
    public GridGenerator gridGenerator;

    public int index;
    public bool occupied;

    public int x;
    public int z;

    public int gCost;
    public int hCost;
    public int fCost;
    public bool isWalkable = false;
    public PathNode previousNode;

    public bool startTile=false;
    public bool endTile=false;

    public void InitializePathNode(int x, int z) {
        this.x = x;
        this.z = z;
    }

    public void CalculateFCost() {
        fCost=gCost+hCost;
    }

    public void SetWalkable(bool isWalkable) {
        this.isWalkable = isWalkable;
    }
}
