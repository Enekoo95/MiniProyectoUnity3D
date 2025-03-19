using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public float dashSpeed = 15f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    public float objectOffset = 1.5f;
    public float dashDetectionRadius = 1.5f;
    public int dashDamage = 50;

    private CharacterController controller;
    private bool isDashing = false;
    private bool canDash = false;
    private float dashTime = 0f;
    private float lastDashTime = -100f;
    private Vector3 dashDirection;
    private Transform carriedObject;
    private int defaultLayer;  // Guarda la capa original

    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (controller == null)
        {
            Debug.LogError("No se encontró un CharacterController en " + gameObject.name);
        }

        defaultLayer = gameObject.layer;  // Guarda la capa inicial del jugador
    }

    void Update()
    {
        if (controller == null) return;

        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        // Iniciar Dash
        if (canDash && Input.GetKeyDown(KeyCode.Space) && Time.time > lastDashTime + dashCooldown && moveDirection.magnitude > 0.1f)
        {
            isDashing = true;
            dashTime = Time.time + dashDuration;
            lastDashTime = Time.time;
            dashDirection = moveDirection.normalized;

            // Cambiar la capa a "DashingPlayer"
            gameObject.layer = LayerMask.NameToLayer("DashingPlayer");
        }

        // Aplicar Dash
        if (isDashing)
        {
            controller.Move(dashDirection * dashSpeed * Time.deltaTime);

            // Detectar enemigos y paredes
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, dashDetectionRadius);
            foreach (Collider collider in hitColliders)
            {
                // Romper paredes
                if (collider.CompareTag("BreakableWall"))
                {
                    Destroy(collider.gameObject);
                    Debug.Log("Pared rota con el dash!");
                }

                // Dañar enemigos
                if (collider.CompareTag("Enemy"))
                {
                    Enemy enemy = collider.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        enemy.TakeDamage(dashDamage);
                        Debug.Log("Enemigo golpeado con el dash!");
                    }
                }
            }

            // Termina el dash
            if (Time.time >= dashTime)
            {
                isDashing = false;
                // Volver a la capa original para que el jugador vuelva a chocar con los enemigos
                gameObject.layer = defaultLayer;
            }
        }

        // Mantener el objeto recogido enfrente
        if (carriedObject != null)
        {
            Vector3 objectPosition = transform.position + transform.forward * objectOffset;
            carriedObject.position = objectPosition;
            carriedObject.rotation = Quaternion.identity;
        }
    }

    public void PickUpObject(Transform obj)
    {
        carriedObject = obj;
        canDash = true;
        Debug.Log("Objeto recogido y dash habilitado");
    }
}
