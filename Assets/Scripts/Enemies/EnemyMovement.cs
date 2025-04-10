using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    public float speed = 30f;
    public float detectionRange = 100000f;
    public float visionAngle = 60f;
    public LayerMask obstacleMask;

    public float attackRange = 1f;

    private bool isChasing = false;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetBool("isChasing", isChasing);

        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (isChasing && distance <= attackRange)
        {
            animator.SetTrigger("attack");
        }

        // Logica enemigo

        Enemy enemyScript = GetComponent<Enemy>();
        PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();

        if (enemyScript != null && enemyScript.enabled && enemyScript.isPaused)
        {
            return;
        }

        if (playerHealth != null && playerHealth.currentHealth <= 0)
        {
            isChasing = false;
            return;
        }

        if (!isChasing && CanSeePlayer())
        {
            isChasing = true;
            Debug.Log("¡Enemigo detectó al jugador! Iniciando persecución.");
        }

        if (isChasing)
        {
            ChasePlayer();
        }
    }

    void ChasePlayer()
    {
        if (player == null) return;

        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0;

        transform.position += direction * speed * Time.deltaTime;

        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }

    bool CanSeePlayer()
    {
        if (player == null) return false;

        Vector3 directionToPlayer = player.position - transform.position;
        if (directionToPlayer.magnitude > detectionRange) return false;

        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
        if (angleToPlayer > visionAngle / 2f) return false;

        if (Physics.Raycast(transform.position, directionToPlayer.normalized, out RaycastHit hit, detectionRange, obstacleMask))
        {
            if (!hit.collider.CompareTag("Player"))
                return false;
        }

        return true;
    }
}
