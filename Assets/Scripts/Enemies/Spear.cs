using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
    public float lifeTime = 5f; // Tiempo máximo antes de destruirse si no choca
    public int damage = 10; // Daño que hace la lanza
    private bool hasHit = false; // Bandera para asegurarse de que solo hace daño una vez

    void Start()
    {
        Destroy(gameObject, lifeTime); // Por si nunca choca, se destruye después de X segundos
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Si ya ha golpeado algo, no hacemos nada
        if (hasHit) return;

        // Verificamos si el objeto con el que colisionamos tiene el tag "Player"
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("¡Golpeó al jugador!");

            // Intentamos obtener el componente de salud del jugador
            PlayerHealth playerHealth = collision.collider.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage); // Le hace el daño al jugador
            }

            // Marcamos que la lanza ya ha golpeado
            hasHit = true;
        }

        // En cualquier caso, la lanza se destruye después de una sola colisión
        Destroy(gameObject);
    }
}
