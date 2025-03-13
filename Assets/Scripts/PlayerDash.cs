using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public float dashSpeed = 15f;    // Velocidad del dash
    public float dashDuration = 0.2f; // Duración del dash
    public float dashCooldown = 1f;  // Tiempo de espera entre dashes
    public float objectOffset = 0.5f; // Distancia del objeto al jugador
    public float dashDetectionRadius = 1f;  // Radio de detección para romper paredes

    private CharacterController controller;
    private bool isDashing = false;
    private bool canDash = false;  // Inicialmente el dash está desactivado
    private float dashTime = 0f;
    private float lastDashTime = -100f;
    private Vector3 dashDirection;
    private Transform carriedObject; // Referencia al objeto recogido

    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (controller == null)
        {
            Debug.LogError("No se encontró un CharacterController en " + gameObject.name);
        }
    }

    void Update()
    {
        if (controller == null) return;

        // Capturar movimiento del personaje
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        // DASH: Si se presiona ESPACIO y hay cooldown disponible
        if (canDash && Input.GetKeyDown(KeyCode.Space) && Time.time > lastDashTime + dashCooldown && moveDirection.magnitude > 0.1f)
        {
            isDashing = true;
            dashTime = Time.time + dashDuration;
            lastDashTime = Time.time;
            dashDirection = moveDirection.normalized; // Guardar la dirección del dash
        }

        // Aplicar dash si está activo
        if (isDashing)
        {
            controller.Move(dashDirection * dashSpeed * Time.deltaTime);

            // Detectar colisiones mientras haces dash
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, dashDetectionRadius);
            foreach (Collider collider in hitColliders)
            {
                if (collider.CompareTag("BreakableWall"))
                {
                    Destroy(collider.gameObject);  // Rompe la pared
                    Debug.Log("Pared rota con el dash!");
                }
            }

            // Termina el dash
            if (Time.time >= dashTime)
            {
                isDashing = false;
            }
        }

        // Si lleva un objeto, actualizar su posición enfrente del jugador y mantenerlo recto
        if (carriedObject != null)
        {
            Vector3 objectPosition = transform.position + transform.forward * objectOffset;
            carriedObject.position = objectPosition;
            carriedObject.rotation = Quaternion.identity; // Mantiene el objeto recto (sin rotación)
        }
    }

    // Método para recoger el objeto
    public void PickUpObject(Transform obj)
    {
        carriedObject = obj;
        canDash = true; // Habilitar dash
        Debug.Log("Objeto recogido y dash habilitado");
    }
}
