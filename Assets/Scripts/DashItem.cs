using UnityEngine;

public class DashItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el jugador tiene el script PlayerDash
        PlayerDash playerDash = other.GetComponent<PlayerDash>();

        if (playerDash != null)
        {
            playerDash.PickUpObject(transform); // Pegar el objeto al jugador
            GetComponent<Collider>().enabled = false; // Desactivar colisión para evitar doble recogida
        }
    }
}