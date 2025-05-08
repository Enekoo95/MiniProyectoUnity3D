using UnityEngine;

public class HealthRecover : MonoBehaviour
{
    public int healAmount = 10; // Vida que recupera el objeto

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el jugador entr� en el trigger
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                // Recuperar vida y actualizar corazones
                playerHealth.Heal(healAmount);
                Debug.Log("�Vida recuperada! + " + healAmount);

                // Destruir el objeto despu�s de recogerlo
                Destroy(gameObject);
            }
        }
    }
}
