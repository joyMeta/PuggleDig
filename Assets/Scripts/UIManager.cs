using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public List<GameObject> prefabs=new List<GameObject>();

    public void SpawnPrefab(int index) {
        GameObject go = Instantiate(prefabs[index],Input.mousePosition,Quaternion.identity);
    }
}
