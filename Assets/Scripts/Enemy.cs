using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 3;
    public int dashesToKill = 3;
    private int currentHealth;
    public float health = 50;
    public void TakeDamage (float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamageFromDash()
    {
        int dashDamage = maxHealth / dashesToKill;
        currentHealth -= dashDamage;
        Debug.Log("Enemigo recibió " + dashDamage + " de daño. Vida restante: " + currentHealth);

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
