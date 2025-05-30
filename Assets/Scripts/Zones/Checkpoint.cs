using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public ParticleSystem idleParticles;
    public ParticleSystem activatedParticles;
    private bool activated = false;

    private void Start()
    {
        if (idleParticles != null)
            idleParticles.Play();

        if (activatedParticles != null)
            activatedParticles.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (activated) return;

        if (other.CompareTag("Player"))
        {
            PlayerRespawn respawn = other.GetComponent<PlayerRespawn>();
            if (respawn != null)
            {
                respawn.SetCheckpointPosition(transform.position, transform.rotation);
                ActivateCheckpoint();
            }
        }
    }

    private void ActivateCheckpoint()
    {
        activated = true;
        if (idleParticles != null)
            idleParticles.Stop();

        if (activatedParticles != null)
            activatedParticles.Play();

        Debug.Log("Checkpoint activado.");
    }
}
