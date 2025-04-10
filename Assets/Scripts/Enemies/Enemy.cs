using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 3;
    public int dashesToKill = 3;
    private int currentHealth;
    public float health = 50;
    public int damageAmount = 10;
    public float hitPauseTime = 1f; // Tiempo de pausa después de hacer daño
    private Animator animator;

    public bool isPaused = false; // Para controlar si está en pausa

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }



    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("¡Impacto en el enemigo! Vida restante: " + health);

        if (health <= 0)
        {
            Die();
        }
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isPaused)
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
                Debug.Log("El enemigo hizo " + damageAmount + " de daño al jugador.");

                // Añadir un pequeño empuje hacia atrás
                Vector3 knockbackDirection = (other.transform.position - transform.position).normalized;
                CharacterController playerController = other.GetComponent<CharacterController>();
                if (playerController != null)
                {
                    playerController.Move(knockbackDirection * 0.5f);
                }

                // Iniciar la pausa después de dar el golpe
                StartCoroutine(HitPause());
            }
        }
    }

    // Corutina para pausar al enemigo después de golpear
    private IEnumerator HitPause()
    {
        isPaused = true;
        Debug.Log("Enemigo en pausa después del golpe.");

        // Esperar el tiempo de pausa
        yield return new WaitForSeconds(hitPauseTime);

        isPaused = false;
        Debug.Log("El enemigo vuelve a perseguir.");
    }


}
