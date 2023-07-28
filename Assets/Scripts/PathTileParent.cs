using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PathTileParent : MonoBehaviour {
    public List<PathNode> childNodes = new List<PathNode>();
    public int index;
    public bool preOccupied;
    public bool selected = false;
    Snapping snapping;
    GameManager gameManager;

    private void Awake() {
        snapping = GetComponent<Snapping>();
    }

    private void Start() {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update() {
        Physics.Raycast(transform.position, -transform.up * 2, out RaycastHit info);
        if (info.collider != null) {
            if (info.transform.GetComponent<PathNode>() != null) {
                if (snapping.gridGenerator != info.transform.GetComponent<PathNode>().gridGenerator)
                    snapping.gridGenerator = info.transform.GetComponent<PathNode>().gridGenerator;
            }
        }
        if (selected)
            return;
        foreach (PathNode node in childNodes) {
            RaycastHit hit;
            if (Physics.Raycast(node.transform.position, -node.transform.up, out hit, 2f)) {
                hit.transform.GetComponent<PathNode>().SetWalkable(node.isWalkable);
            }
        }
    }

    private void OnMouseOver() {
        if (gameManager.actionDice == ActionDice.ROTATE) {
            if (Input.GetMouseButtonDown(1))
                transform.Rotate(transform.up, 90);
        }
    }
}