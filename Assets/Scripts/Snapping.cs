using UnityEngine;

public class Snapping : MonoBehaviour
{
    bool selected;
    PathNode pathNode;
    public GridGenerator gridGenerator;
    [SerializeField]
    Vector3 positionOffset;
    Vector3 runtimeOffset;

    bool storageGrid;

    private void Start() {
        gridGenerator=FindObjectOfType<GridGenerator>();
        SnapToNearest();
    }

    private void LateUpdate() {
        if (selected) {
            transform.position = SnappingManager.Instance.targetPosition+ positionOffset;
            float smallestDistance = SnappingManager.Instance.snapDistance;

            if (pathNode)
                pathNode.occupied = false;

            foreach (PathNode path in gridGenerator.pathNodes) {
                if (!pathNode.occupied && Vector3.Distance(pathNode.transform.position, SnappingManager.Instance.targetPosition) < smallestDistance) {
                    transform.position=path.transform.position+ positionOffset;
                    smallestDistance = Vector3.Distance(pathNode.transform.position, SnappingManager.Instance.targetPosition);
                    pathNode = path;
                }
            }
            pathNode.occupied = true;
        }
    }

    public void OnMouseDown() {
        if (selected)
            Deselect();
        else
            Invoke(nameof(Select), 2 * Time.deltaTime);
    }

    public void SnapToNearest() {
        int firstFreeTileIndex = 0;
        PathNode nearestTile = gridGenerator.pathNodes[0];
        if (!gridGenerator.StorageGrid) {
            for (int i = 0; i < gridGenerator.pathNodes.Count - gridGenerator.height; i += 2) {
                if (gridGenerator.pathNodes[i].x % 2 != 0)
                    continue;
                if (!gridGenerator.pathNodes[i].occupied || gridGenerator.pathNodes[i] == pathNode) {
                    nearestTile = gridGenerator.pathNodes[i];
                    firstFreeTileIndex = i;
                    break;
                }
            }
            if (pathNode)
                pathNode.occupied = false;
            for (int i = 0; i < gridGenerator.pathNodes.Count - gridGenerator.height; i += 2) {
                if (gridGenerator.pathNodes[i].x % 2 != 0)
                    continue;
                if (!gridGenerator.pathNodes[i].occupied && Vector3.Distance(nearestTile.transform.position, transform.position) > Vector3.Distance(gridGenerator.pathNodes[i].transform.position, transform.position)) {
                    nearestTile = gridGenerator.pathNodes[i];
                }
            }
            runtimeOffset = positionOffset;
        }
        else {
            for (int i = 0; i < gridGenerator.pathNodes.Count; i ++) {
                if (!gridGenerator.pathNodes[i].occupied || gridGenerator.pathNodes[i] == pathNode) {
                    nearestTile = gridGenerator.pathNodes[i];
                    firstFreeTileIndex = i;
                    break;
                }
            }
            if (pathNode)
                pathNode.occupied = false;
            for (int i = 0; i < gridGenerator.pathNodes.Count; i ++) {
                if (!gridGenerator.pathNodes[i].occupied && Vector3.Distance(nearestTile.transform.position, transform.position) > Vector3.Distance(gridGenerator.pathNodes[i].transform.position, transform.position)) {
                    nearestTile = gridGenerator.pathNodes[i];
                }
            }
            runtimeOffset = new Vector3(0, 1, 0);
        }
        transform.position = nearestTile.transform.position + runtimeOffset;
        pathNode = nearestTile;
        pathNode.occupied = true;
    }

    public void SnapToTile(int tileIndex) {
        if (gridGenerator.pathNodes.Exists(x => x.index == tileIndex) && !gridGenerator.pathNodes.Find(x => x.index == tileIndex).occupied) {
            PathNode tile = gridGenerator.pathNodes.Find(x => x.index == tileIndex);
            transform.position = tile.transform.position+ positionOffset;
            pathNode = tile;
            tile.occupied = true;
        }
        else {
            SnapToNearest();
        }
    }

    public void ReleaseTile() {
        if (pathNode != null) {
            pathNode.occupied = false;
            pathNode = null;
        }
    }

    public void Select() {
        if (SnappingManager.Instance.currentSnappedObject != null && SnappingManager.Instance.currentSnappedObject != this)
            SnappingManager.Instance.currentSnappedObject.Deselect();
        SnappingManager.Instance.currentSnappedObject = this;
        SnappingManager.Instance.currentSnappedObject.GetComponent<PathTileParent>().selected = true;
        selected = true;

    }

    public void Deselect() {
        if (selected)
            RemoveReference();
    }

    public void RemoveReference() {
        SnappingManager.Instance.currentSnappedObject.GetComponent<PathTileParent>().selected = false;
        SnappingManager.Instance.currentSnappedObject = null;
        selected = false;
        SnapToNearest();
    }
}
