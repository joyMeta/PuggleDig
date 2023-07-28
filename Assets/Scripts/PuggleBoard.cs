using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuggleBoard : MonoBehaviour
{
    public List<PathTileParent> storedPathTiles = new List<PathTileParent>();
    int pugglePowerLevel = 0;
    int maxPowerLevel = 4;

    public void Diamond() {
        if (pugglePowerLevel < maxPowerLevel)
            pugglePowerLevel++;
        else
            pugglePowerLevel = 0;
    }

    public void StoreTile(PathTileParent tile) {
        if (storedPathTiles.Count < 4)
            storedPathTiles.Add(tile);
        else
            Debug.Log("Tiles full");
    }
}
