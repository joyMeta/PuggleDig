using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnappingManager : MonoBehaviour
{
    public static SnappingManager Instance;
    public Snapping currentSnappedObject;
    public GridGenerator gridGenerator;
    public Vector3 targetPosition;
    public float snapDistance = 1;

    private Vector3 worldPosition;
    private Plane plane = new Plane(Vector3.up, 0);

    public void Awake() {
        if (Instance!= null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void Update() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out float distance)) {
            worldPosition=ray.GetPoint(distance);
        }
        targetPosition = worldPosition;
    }

    public void SetGridGenerator(GridGenerator grid) {
        gridGenerator= grid;
    }

    public void RemoveReference() {
        currentSnappedObject = null;
    }
}
