using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("El jugador recibió " + damage + " de daño. Vida restante: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("¡El jugador ha muerto!");

        // Desactivar el movimiento del jugador
        GetComponent<PlayerController>().enabled = false;
        GetComponent<CharacterController>().enabled = false;

        // Opcional: Desactivar el objeto del jugador
        gameObject.SetActive(false);

        // Opcional: Reiniciar el nivel después de 2 segundos
        Invoke("RestartLevel", 2f);
    }

    void RestartLevel()
    {
        // Recargar la escena actual
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}