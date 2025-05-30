using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    public float speed = 5f;
    public float detectionRange = 30f;
    public float visionAngle = 60f;
    public LayerMask obstacleMask;
    public LayerMask groundLayer;
    public float attackRange = 2f;

    [Header("Audio")]
    public AudioClip spottedSound;

    private bool isChasing = false;
    private bool hasPlayedSpottedSound = false;

    private Animator animator;
    private CharacterController controller;
    private AudioSource audioSource;

    private float verticalVelocity = 0f;
    public float gravity = -20f;

    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();

        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
        }
    }

    void Update()
    {
        animator.SetBool("isChasing", isChasing);
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);
        if (isChasing && distance <= attackRange)
        {
            animator.ResetTrigger("attack");
            animator.SetTrigger("attack");
        }

        Enemy enemyScript = GetComponent<Enemy>();
        if (enemyScript != null && enemyScript.isPaused) return;

        if (!isChasing && CanSeePlayer())
        {
            isChasing = true;
            Debug.Log("¡Enemigo detectó al jugador! Iniciando persecución.");

            if (!hasPlayedSpottedSound && spottedSound != null)
            {
                audioSource.PlayOneShot(spottedSound);
                hasPlayedSpottedSound = true;
            }
        }

        ApplyGravity();

        if (isChasing)
        {
            ChasePlayer();
        }
        else
        {
            controller.Move(Vector3.up * verticalVelocity * Time.deltaTime);
        }
    }

    void ChasePlayer()
    {
        if (player == null) return;

        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0;

        Vector3 movement = direction * speed;
        movement.y = verticalVelocity;

        controller.Move(movement * Time.deltaTime);

        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }

    void ApplyGravity()
    {
        Vector3 rayOrigin = transform.position + Vector3.up * 0.1f;
        bool isGrounded = Physics.Raycast(rayOrigin, Vector3.down, 0.3f, groundLayer);

        if (isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }
    }

    bool CanSeePlayer()
    {
        if (player == null) return false;

        Vector3 directionToPlayer = player.position - transform.position;
        if (directionToPlayer.magnitude > detectionRange) return false;

        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
        if (angleToPlayer > visionAngle / 2f) return false;

        Vector3 origin = transform.position + Vector3.up * 1f;
        if (Physics.Raycast(origin, directionToPlayer.normalized, out RaycastHit hit, detectionRange, obstacleMask))
        {
            if (!hit.collider.CompareTag("Player"))
                return false;
        }

        return true;
    }
}
