using UnityEngine;

public class Spear : MonoBehaviour
{
    public float lifeTime = 5f;
    public int damage = 10;
    private bool hasHit = false;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.LogError("La lanza no tiene Rigidbody.");
            return;
        }

        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        if (!hasHit && rb != null && rb.velocity.magnitude > 0.1f)
        {
            // Dirección de movimiento
            Vector3 direction = rb.velocity.normalized;

            // Ajustamos la rotación para que apunte hacia adelante
            Quaternion baseRotation = Quaternion.LookRotation(direction);

            transform.rotation = baseRotation * Quaternion.Euler(90, 0, 0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasHit) return;

        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("¡Golpeó al jugador!");
            PlayerHealth playerHealth = collision.collider.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            hasHit = true;
        }

        Destroy(gameObject);
    }
}
