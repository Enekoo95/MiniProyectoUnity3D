using UnityEngine;
using System.Collections;

public class PlayerRespawn : MonoBehaviour
{
    private Vector3 checkpointPosition;
    private Quaternion checkpointRotation;
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
        rend = GetComponentInChildren<Renderer>();

        checkpointPosition = transform.position;
        checkpointRotation = transform.rotation;
    }

    public void SetCheckpointPosition(Vector3 position, Quaternion rotation)
    {
        checkpointPosition = position;
        checkpointRotation = rotation;
        hasCheckpoint = true;
        Debug.Log("Checkpoint guardado en: " + checkpointPosition);
    }

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
        if (col != null) col.enabled = false;
        if (rend != null) rend.enabled = false;

        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        if (cc != null)
            cc.enabled = false;

        yield return new WaitForSeconds(0.5f);

        // Offset relativo a la rotación del checkpoint
        Vector3 localOffset = new Vector3(1f, 2f, 0f); // 1 a la derecha, 2 arriba
        Vector3 worldOffset = checkpointRotation * localOffset;
        transform.position = checkpointPosition + worldOffset;

        // Restaurar salud
        PlayerHealth health = GetComponent<PlayerHealth>();
        if (health != null)
        {
            health.RestoreHealth();
        }

        if (cc != null)
            cc.enabled = true;

        if (col != null) col.enabled = true;
        if (rend != null) rend.enabled = true;

        Debug.Log("Jugador reaparecido en checkpoint: " + transform.position);
    }
}
