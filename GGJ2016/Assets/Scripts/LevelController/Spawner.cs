using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public GameObject spawn;
    public GameObject prefab;
    public Transform spawnLocation;
    public bool isActive = true;

    void Update()
    {
        if (spawn == null && isActive)
        {
            spawn = Instantiate(prefab, spawnLocation.position, Quaternion.identity) as GameObject;
        }
    }
}
