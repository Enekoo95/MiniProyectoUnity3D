using UnityEngine;
using System.Collections;

public class PlayerRespawn : MonoBehaviour
{
    private Vector3 checkpointPosition;
    private bool hasCheckpoint = false;
    private Rigidbody rb;
    private CharacterController cc;
    private Collider col;
    private Renderer rend;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CharacterController>();
        col = GetComponent<Collider>();
        rend = GetComponentInChildren<Renderer>(); // Buscar el primer renderer hijo (opcional)

        checkpointPosition = transform.position; // Posición inicial como fallback
    }

    // Guardar el checkpoint
    public void SetCheckpointPosition(Vector3 position)
    {
        checkpointPosition = position;
        hasCheckpoint = true;
        Debug.Log("Checkpoint guardado en: " + checkpointPosition);
    }

    // Método llamado cuando muere
    public void OnDeath()
    {
        if (!hasCheckpoint)
        {
            Debug.LogWarning("No hay checkpoint guardado.");
            return;
        }

        Debug.Log("Jugador ha muerto, iniciando respawn...");
        StartCoroutine(RespawnRoutine());
    }

    private IEnumerator RespawnRoutine()
    {
        // Desactivar colisión y render (opcional visualmente)
        if (col != null) col.enabled = false;
        if (rend != null) rend.enabled = false;

        // Resetear física
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        // Si es CC desactivarlo antes de moverlo
        if (cc != null)
            cc.enabled = false;

        // Esperar breve tiempo
        yield return new WaitForSeconds(0.5f);

        // Mover al checkpoint (añadir pequeña altura para evitar clip con el suelo)
        transform.position = checkpointPosition + Vector3.up * 2f;

        // Reactivar física
        if (cc != null)
            cc.enabled = true;

        if (col != null) col.enabled = true;
        if (rend != null) rend.enabled = true;

        Debug.Log("Jugador reaparecido en checkpoint: " + transform.position);
    }
}
