using UnityEngine;

public class PlayerWinTest : MonoBehaviour
{
    PathFinding pathFinding;
    GridGenerator gridGenerator;

    private void Start() {
        gridGenerator = GetComponent<GridGenerator>();
        pathFinding = GetComponent<PathFinding>();
    }

    public void Update() {
        TestPath();
    }

    public void TestPath() {
        if (gridGenerator.startTile.isWalkable) {
            if (pathFinding.FindPath(gridGenerator.startTile, gridGenerator.endTile_1)) {
                Debug.Log(transform.name + " won the game");
            }
            else if (pathFinding.FindPath(gridGenerator.startTile, gridGenerator.endTile_2)) {
                Debug.Log(transform.name + " won the game");
            }
        }
        //else {
        //    Debug.Log("Game not won yet");
        //}
    }
}
