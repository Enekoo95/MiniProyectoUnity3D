using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float mouseSensitivity = 2f;
    public Transform cameraTransform;
    public float gravity = 9.81f;

    private CharacterController controller;
    private Vector3 velocity;
    private float verticalRotation = 0f;
    private bool isDead = false;
    private PlayerDash playerDash;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerDash = GetComponent<PlayerDash>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HandleMovement();
        HandleMouseLook();
        ApplyGravity();
    }

    void HandleMovement()
    {
        // No moverse si se está haciendo dash
        if (playerDash != null && playerDash.IsDashing) return;

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * speed * Time.deltaTime);
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void ApplyGravity()
    {
        bool isGrounded = controller.isGrounded || Physics.Raycast(transform.position, Vector3.down, controller.height / 2 + 0.1f);

        if (!controller.isGrounded)
        {
            velocity.y -= gravity * Time.deltaTime;
        }
        else
        {
            velocity.y = -2f;
        }

        if (velocity.y > 0 && !isGrounded)
        {
            velocity.y = 0f;
        }

        controller.Move(velocity * Time.deltaTime);
    }

    public void Die()
    {
        if (isDead) return;

        isDead = true;
        Debug.Log("¡El jugador ha muerto!");
        GetComponent<CharacterController>().enabled = false;
        enabled = false;

        gameObject.SetActive(false);
        Invoke("RestartLevel", 2f);
    }

    void RestartLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
