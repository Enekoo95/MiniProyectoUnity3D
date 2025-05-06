using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 50;
    public int currentHealth;

    // Array de corazones
    public GameObject[] hearts;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHearts();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("El jugador recibió " + damage + " de daño. Vida restante: " + currentHealth);

        UpdateHearts();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHearts()
    {
        int heartCount = Mathf.CeilToInt((float)currentHealth / (maxHealth / hearts.Length));

        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].SetActive(i < heartCount);
        }
    }

    void Die()
    {
        Debug.Log("¡El jugador ha muerto!");

        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<CharacterController>().enabled = false;

        gameObject.SetActive(false);

        Invoke("RestartLevel", 2f);
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Heal(int amount)
    {
        currentHealth += amount;

        // Limitar la salud al máximo
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        Debug.Log("El jugador ha sanado " + amount + " puntos de vida. Vida actual: " + currentHealth);
        UpdateHearts();
    }
}
