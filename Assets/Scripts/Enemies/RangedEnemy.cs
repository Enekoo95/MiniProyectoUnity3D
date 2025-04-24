using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform shootPoint;
    public Transform player;
    public float shootForce = 20f;
    public float attackCooldown = 2f;
    public float attackRange = 10f;
    public float attackDelay = 0.5f; // Tiempo de espera antes de disparar

    private float lastAttackTime = 0f;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("No se encontr� el componente Animator en " + gameObject.name);
        }
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            FacePlayer();

            if (Time.time - lastAttackTime > attackCooldown)
            {
                animator.SetBool("IsAttacking", true);

                // Espera un momento antes de disparar
                Invoke(nameof(Shoot), attackDelay);

                lastAttackTime = Time.time + attackDelay;

                Invoke(nameof(ReturnToIdle), 1f); // Ajusta seg�n tu animaci�n
            }
        }
        else
        {
            animator.SetBool("IsAttacking", false);
        }
    }

    void Shoot()
    {
        Debug.Log("Disparando proyectil...");

        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);

        Physics.IgnoreCollision(projectile.GetComponent<Collider>(), GetComponent<Collider>());

        projectile.transform.localScale = new Vector3(1, 1, 1);

        // Direcci�n hacia el jugador con m�s inclinaci�n hacia abajo
        Vector3 toPlayer = (player.position - shootPoint.position).normalized;

        // A�adimos una inclinaci�n vertical manual
        Vector3 launchDirection = (toPlayer + Vector3.up * 0.1f).normalized;


        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = true;

            float adjustedForce = shootForce * 1.2f; // Puedes subir esto si se queda corto
            rb.AddForce(launchDirection * adjustedForce, ForceMode.Impulse);
        }
        else
        {
            Debug.LogWarning("El proyectil no tiene Rigidbody");
        }
    }

    void FacePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }

    void ReturnToIdle()
    {
        animator.SetBool("IsAttacking", false);
    }
}
