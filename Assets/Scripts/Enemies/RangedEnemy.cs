using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    public Transform player;
    public GameObject projectilePrefab;
    public Transform shootPoint;
    public float shootCooldown = 2f;
    public float detectionRange = 10f;

    private float lastShootTime;

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        // Comprobar si el jugador est� en rango
        if (distance <= detectionRange)
        {
            // Si ha pasado suficiente tiempo, lanzar el proyectil
            if (Time.time >= lastShootTime + shootCooldown)
            {
                Shoot();
                lastShootTime = Time.time;
            }
        }
    }

    void Shoot()
    {
        // Instanciamos la lanza sin rotaci�n especial
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);

        // Escalamos la lanza (aj�stalo si sigue siendo muy grande o peque�a)
        projectile.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);

        // Calculamos la direcci�n hacia el jugador
        Vector3 direction = (player.position - shootPoint.position).normalized;

        // Rotamos la lanza para que mire hacia el jugador
        projectile.transform.rotation = Quaternion.LookRotation(direction);

        // Aplicamos la velocidad hacia el jugador
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = direction * 10f; // Puedes ajustar la velocidad aqu�
        }

        // Destruir la lanza despu�s de 5 segundos
        Destroy(projectile, 5f);
    }
}

