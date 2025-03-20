using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Colisión detectada con: {other.name}");

        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
            Debug.Log("¡Jugador ha caído en la piscina y ha muerto!");
        }
    }
}
