using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 50;
    public int currentHealth;

    public GameObject[] hearts;

    private PlayerRespawn playerRespawn;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHearts();

        playerRespawn = GetComponent<PlayerRespawn>();
        if (playerRespawn == null)
            Debug.LogError("PlayerRespawn no encontrado en el jugador.");
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

        if (playerRespawn != null)
        {
            playerRespawn.OnDeath();
        }
        else
        {
            Debug.LogError("No se encontró PlayerRespawn, recargando escena...");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        Debug.Log("El jugador ha sanado " + amount + " puntos de vida. Vida actual: " + currentHealth);
        UpdateHearts();
    }

    public void RestoreHealth()
    {
        currentHealth = maxHealth;
        UpdateHearts();
    }
}
