using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform shootPoint;
    public Transform player;
    public float shootForce = 20f;
    public float attackCooldown = 2f;
    public float attackRange = 10f;

    private float lastAttackTime = 0f;

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            FacePlayer();

            if (Time.time - lastAttackTime > attackCooldown)
            {
                Shoot();
                lastAttackTime = Time.time;
            }
        }
    }

    void Shoot()
    {
        Debug.Log("Disparando proyectil...");

        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);

        // Escalamos correctamente la lanza
        projectile.transform.localScale = new Vector3(1, 1, 1); // Ajusta si lo necesitas

        // Direcciona hacia el jugador
        Vector3 direction = (player.position - shootPoint.position).normalized;

        // Rotamos el proyectil para que apunte correctamente
        projectile.transform.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(90, 0, 0);

        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = direction * shootForce;
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

}

