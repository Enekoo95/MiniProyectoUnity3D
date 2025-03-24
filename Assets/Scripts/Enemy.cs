using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 3;
    public int dashesToKill = 3;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamageFromDash()
    {
        int dashDamage = maxHealth / dashesToKill;
        currentHealth -= dashDamage;
        Debug.Log("Enemigo recibi� " + dashDamage + " de da�o. Vida restante: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemigo eliminado");
        Destroy(gameObject);
    }
}
