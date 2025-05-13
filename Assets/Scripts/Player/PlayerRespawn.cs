using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector3 checkpointPosition;
    private bool hasCheckpoint = false;
    private Rigidbody rb;
    private CharacterController cc;

    private void Start()
    {
        // Asegúrate de que el Rigidbody o CharacterController esté en el Player principal
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CharacterController>();

        if (rb != null)
            Debug.Log("Rigidbody encontrado en: " + rb.gameObject.name);

        if (cc != null)
            Debug.Log("CharacterController encontrado en: " + cc.gameObject.name);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (hasCheckpoint)
            {
                RespawnAtCheckpoint();
            }
            else
            {
                Debug.LogWarning("No hay checkpoint guardado.");
            }
        }
    }

    // Método para guardar la posición del checkpoint
    public void SetCheckpointPosition(Vector3 position)
    {
        checkpointPosition = position;
        hasCheckpoint = true;
        Debug.Log("Checkpoint guardado en: " + checkpointPosition);
    }

    // Método para respawnear al jugador
    public void RespawnAtCheckpoint()
    {
        if (!hasCheckpoint)
        {
            Debug.LogWarning("No hay checkpoint guardado.");
            return;
        }

        Debug.Log("Reapareciendo en: " + checkpointPosition);

        if (rb != null)
        {
            // Resetear la velocidad del Rigidbody antes de moverlo
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.position = checkpointPosition; // Mover Rigidbody
        }
        else if (cc != null)
        {
            // Si usas CharacterController, desactivar y reactivar para moverlo
            cc.enabled = false;
            transform.position = checkpointPosition;
            cc.enabled = true;
        }
        else
        {
            // Solo si no hay Rigidbody ni CC
            transform.position = checkpointPosition;
        }
    }

    // Método que se llama cuando el jugador muere (cae en la piscina)
    public void OnDeath()
    {
        Debug.Log("El jugador ha muerto, reapareciendo en el checkpoint.");
        RespawnAtCheckpoint();
    }
}
