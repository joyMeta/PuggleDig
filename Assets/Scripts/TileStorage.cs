using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileStorage : MonoBehaviour {
    public PuggleBoard board;

    private void Awake() {
        board = GetComponentInParent<PuggleBoard>();
    }

    private void OnTriggerStay(Collider other) {
        Debug.Log(other.name);
        if (other.GetComponent<PathTileParent>() != null && !other.GetComponent<PathTileParent>().selected)
            board.StoreTile(other.GetComponent<PathTileParent>());
    }
}