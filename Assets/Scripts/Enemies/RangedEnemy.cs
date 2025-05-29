using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RangedEnemy : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform shootPoint;
    public Transform player;
    public float shootForce = 20f;
    public float attackCooldown = 2f;
    public float attackRange = 10f;
    public float attackDelay = 0.5f;

    [Header("Audio")]
    public AudioClip spottedSound;

    private float lastAttackTime = 0f;
    private Animator animator;
    private AudioSource audioSource;

    private bool hasPlayedSpottedSound = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        if (animator == null)
        {
            Debug.LogError("No se encontró el componente Animator en " + gameObject.name);
        }
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            FacePlayer();

            if (!hasPlayedSpottedSound && spottedSound != null)
            {
                audioSource.PlayOneShot(spottedSound);
                hasPlayedSpottedSound = true;
            }

            if (Time.time - lastAttackTime > attackCooldown)
            {
                animator.SetBool("IsAttacking", true);
                Invoke(nameof(Shoot), attackDelay);
                lastAttackTime = Time.time + attackDelay;
                Invoke(nameof(ReturnToIdle), 1f);
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

        Vector3 toPlayer = (player.position - shootPoint.position).normalized;
        Vector3 launchDirection = (toPlayer + Vector3.up * 0.1f).normalized;

        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = true;
            float adjustedForce = shootForce * 1.2f;
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
