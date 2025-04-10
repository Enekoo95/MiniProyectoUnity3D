using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    public GameObject projectilePrefab; // Prefab del proyectil
    public Transform shootPoint;        // Punto desde donde dispara
    public Transform player;            // Referencia al jugador
    public float shootForce = 10f;      // Fuerza del proyectil (ajustar para controlar la velocidad)
    public float attackCooldown = 2f;   // Tiempo entre disparos
    public float attackRange = 10f;     // Rango del ataque

    private float lastAttackTime = 0f;  // Última vez que se disparó
    private Animator animator;          // Referencia al Animator del enemigo

    void Start()
    {
        // Obtenemos el componente Animator
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("No se encontró el componente Animator en " + gameObject.name);
        }
    }

    void Update()
    {
        if (player == null) return;

        // Calculamos la distancia entre el enemigo y el jugador
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Si el jugador está dentro del rango de ataque
        if (distanceToPlayer <= attackRange)
        {
            FacePlayer(); // Miramos al jugador

            // Comprobamos si ha pasado suficiente tiempo desde el último disparo
            if (Time.time - lastAttackTime > attackCooldown)
            {
                // Activamos la animación de ataque antes de disparar
                animator.SetBool("IsAttacking", true);

                // Disparamos
                Shoot();

                // Actualizamos el tiempo del último ataque
                lastAttackTime = Time.time;

                // Volver a la animación Idle después de un retraso (sin interrumpir el disparo)
                Invoke("ReturnToIdle", 0.5f); // Ajusta el tiempo según la duración de la animación de ataque
            }
        }
        else
        {
            // Si el jugador no está en el rango de ataque, volvemos al estado Idle
            animator.SetBool("IsAttacking", false);
        }
    }

    void Shoot()
    {
        // Creamos el proyectil
        Debug.Log("Disparando proyectil...");

        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);

        // Ajustamos la escala del proyectil si es necesario
        projectile.transform.localScale = new Vector3(1, 1, 1); // Ajusta según sea necesario

        // Calculamos la dirección hacia el jugador
        Vector3 direction = (player.position - shootPoint.position).normalized;

        // Rotamos el proyectil para que apunte correctamente hacia el jugador
        projectile.transform.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(90, 0, 0);

        // Aseguramos que el proyectil tiene un Rigidbody para moverse
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Aplicamos la fuerza ajustada, controlando la velocidad final
            rb.velocity = direction * shootForce;  // Aquí usamos la shootForce como la velocidad final
        }
        else
        {
            Debug.LogWarning("El proyectil no tiene Rigidbody");
        }
    }

    void FacePlayer()
    {
        // Hacemos que el enemigo mire al jugador
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0; // Ignoramos la altura para que no se incline

        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }

    // Método para regresar a la animación Idle
    void ReturnToIdle()
    {
        animator.SetBool("IsAttacking", false); // Regresamos al estado Idle
    }
}
