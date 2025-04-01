using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;  // Asigna el jugador desde el inspector
    public float speed = 3f;  // Velocidad de movimiento
    public float detectionRange = 100000f;  // Rango de visión
    public float visionAngle = 60f;     // Ángulo de visión
    public LayerMask obstacleMask;      // Capa de obstáculos
    private bool isChasing = false;     // Solo persigue después de verte


    void Update()
    {
        // Si el enemigo está en pausa, no hace nada
        Enemy enemyScript = GetComponent<Enemy>();
        // Aquí obtenemos la referencia al componente PlayerHealth
        PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();


        // Verificar si el jugador está muerto
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

        // Calcula la dirección hacia el jugador
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0;  // Evitar que el enemigo se incline hacia adelante/atrás

        // Mueve al enemigo hacia el jugador
        transform.position += direction * speed * Time.deltaTime;

        // Rotar solo en el eje Y
        if (direction != Vector3.zero)  // Evita errores al intentar rotar hacia el vector cero
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }

    bool CanSeePlayer()
    {
        if (player == null) return false;

        // Comprobar si el jugador está dentro del rango
        Vector3 directionToPlayer = (player.position - transform.position);
        if (directionToPlayer.magnitude > detectionRange) return false;

        // Comprobar si el jugador está dentro del ángulo de visión
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
        if (angleToPlayer > visionAngle / 2f) return false;

        // Comprobar si hay obstáculos entre el enemigo y el jugador
        if (Physics.Raycast(transform.position, directionToPlayer.normalized, out RaycastHit hit, detectionRange, obstacleMask))
        {
            if (!hit.collider.CompareTag("Player"))
            {
                return false;  // Algo bloquea la visión
            }
        }

        return true;
    }

}