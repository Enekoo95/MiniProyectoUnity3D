using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 10f;
    public float gravity = 9.81f;

    private CharacterController controller;
    private Vector3 velocity;

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

        // Capturar entrada de teclas (WASD o Flechas)
        float moveX = Input.GetAxis("Horizontal"); // A/D o Izq/Der
        float moveZ = Input.GetAxis("Vertical");   // W/S o Arriba/Abajo

        // Vector de movimiento
        Vector3 move = new Vector3(moveX, 0, moveZ);

        // Si hay movimiento, girar hacia la dirección en la que se mueve
        if (move.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Aplicar gravedad
        if (!controller.isGrounded)
        {
            velocity.y -= gravity * Time.deltaTime;
        }
        else
        {
            velocity.y = -2f; // Pequeño empuje para mantener contacto con el suelo
        }

        // Mover personaje
        controller.Move((move * speed + velocity) * Time.deltaTime);
    }
}
