using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public float dashSpeed = 15f;    // Velocidad del dash
    public float dashDuration = 0.2f; // Duración del dash
    public float dashCooldown = 1f;  // Tiempo de espera entre dashes

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
        if (controller == null || !canDash) return; //No puede hacer dash sin objeto

        // Capturar movimiento del personaje (de otro script)
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        // DASH: Si se presiona ESPACIO y hay cooldown disponible
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > lastDashTime + dashCooldown && moveDirection.magnitude > 0.1f)
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
            if (carriedObject != null)
            {
                carriedObject.position = transform.position + transform.forward * 1.5f; // Mantener el objeto en frente
            }

            if (Time.time >= dashTime)
            {
                isDashing = false; // Termina el dash
            }
        }

        // Si lleva un objeto, actualizar su posición cerca del jugador
        if (carriedObject != null && !isDashing)
        {
            carriedObject.position = transform.position + transform.forward * 1f;
        }
    }

    // Método para recoger el objeto
    public void PickUpObject(Transform obj)
    {
        carriedObject = obj;
        carriedObject.SetParent(transform); // Hace que el objeto siga al jugador
        canDash = true; // Habilitar dash
    }

}


