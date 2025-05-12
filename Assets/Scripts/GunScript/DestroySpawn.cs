using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySpawn : MonoBehaviour
{
    public GameObject spawn;

    private void OnTriggerEnter(Collider other)
    {
        Destroy(spawn);
    }
}
