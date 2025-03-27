using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterImpact : MonoBehaviour
{
    public Material waterMaterial; // Asigna el material del agua en el Inspector
    public float impactStrength = 0.5f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 impactPosition = other.transform.position;
            waterMaterial.SetVector("_ImpactPosition", new Vector4(impactPosition.x, impactPosition.y, impactPosition.z, impactStrength));

            // Espera un frame antes de destruir al jugador para que el shader tenga tiempo de reaccionar
            StartCoroutine(DestroyPlayer(other.gameObject));
        }
    }

    IEnumerator DestroyPlayer(GameObject player)
    {
        yield return new WaitForEndOfFrame();
        Destroy(player);
    }
}