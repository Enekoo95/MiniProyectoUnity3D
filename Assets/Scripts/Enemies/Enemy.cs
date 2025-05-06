using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 3;
    public int dashesToKill = 3;
    private int currentHealth;
    public float health = 50;
    public int damageAmount = 10;
    public float hitPauseTime = 1f;
    public float attackRange = 5f; // Rango para activar la animación de ataque 

    private Animator animator;
    public bool isPaused = false;
    private Transform player;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform; // Busca al jugador
    }

    void Update()
    {
        if (player == null) return;

        // Verificar la distancia al jugador
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Si está dentro del rango de ataque, activar la animación de ataque
        if (distanceToPlayer <= attackRange && !isPaused)
        {
            // Activar animación de ataque si está dentro del rango
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                animator.SetTrigger("attack");
            }
        }
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
        if (isPaused) return;

        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            PlayerDash playerDash = other.GetComponent<PlayerDash>();

            if (playerDash != null && playerDash.IsDashing)
            {
                // No hacer daño si el jugador está dashing
                Debug.Log("Jugador está dashing, no se aplica daño.");
                return;
            }

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
                Debug.Log("El enemigo hizo " + damageAmount + " de daño al jugador.");

                // Empuje hacia atrás
                Vector3 knockbackDirection = (other.transform.position - transform.position).normalized;
                CharacterController playerController = other.GetComponent<CharacterController>();
                if (playerController != null)
                {
                    playerController.Move(knockbackDirection * 0.5f);
                }

                StartCoroutine(HitPause());
            }
        }
    }

    private IEnumerator HitPause()
    {
        isPaused = true;
        Debug.Log("Enemigo en pausa después del golpe.");
        yield return new WaitForSeconds(hitPauseTime);
        isPaused = false;
        Debug.Log("El enemigo vuelve a perseguir.");
    }
}
