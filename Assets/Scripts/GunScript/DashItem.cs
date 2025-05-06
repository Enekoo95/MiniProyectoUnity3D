using UnityEngine;

public class DashItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerDash playerDash = other.GetComponent<PlayerDash>();

        if (playerDash != null)
        {
            // Pegar la espada al jugador y habilitar el dash
            playerDash.PickUpObject(transform);
            playerDash.SetSwordEquipped(true);

            Debug.Log("Espada recogida y equipada");
        }
    }
}