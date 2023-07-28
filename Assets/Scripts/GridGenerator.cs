using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour {
    public int width;
    public int height;

    [SerializeField]
    float gridOffset;

    public List<PathNode> pathNodes = new List<PathNode>();

    public GameObject pathNodePrefab;

    [SerializeField]
    int startTileIndex;
    [SerializeField]
    int endTileIndex_1;
    [SerializeField]
    int endTileIndex_2;

    public PathNode startTile;
    public PathNode endTile_1;
    public PathNode endTile_2;

    [SerializeField]
    bool storageGrid;
    public bool StorageGrid=>storageGrid;

    public void Start() {
        int counter = 0;
        for (int x = 0; x < width; x++) {
            for (int z = 0; z < height; z++) {
                GameObject go = Instantiate(pathNodePrefab, transform);
                go.transform.name +=" "+ counter;
                go.transform.localPosition = new Vector3(x * gridOffset, 0, z * gridOffset);
                PathNode node = go.GetComponent<PathNode>();
                node.gridGenerator = this;
                node.InitializePathNode(x, z);
                pathNodes.Add(node);
                go.transform.SetParent(transform);
                counter++;
            }
        }
        pathNodes[startTileIndex].startTile = true;
        startTile = pathNodes[startTileIndex];
        pathNodes[endTileIndex_1].endTile = true;
        endTile_1 = pathNodes[endTileIndex_1];
        pathNodes[endTileIndex_2].endTile = true;
        endTile_2 = pathNodes[endTileIndex_2];
    }
}