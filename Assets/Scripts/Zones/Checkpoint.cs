using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public ParticleSystem idleParticles;
    public ParticleSystem activatedParticles;

    public GameObject player;
    public List<GameObject> checkPoints;
    public Vector3 vectorPoint;
    public float dead;
    private bool activated = false;

    private void Start()
    {
        if (idleParticles != null)
            idleParticles.Play();

        if (activatedParticles != null)
            activatedParticles.Stop();
    }
    void Update()
    {
        if (player.transform.position.y < -dead)
        {
            player.transform.position = vectorPoint;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (activated) return;

        if (other.CompareTag("Player"))
        {
            ActivateCheckpoint(other.gameObject);
        }
    }

    // Activar el checkpoint y guardar la posición
    void ActivateCheckpoint(GameObject player)
    {
        activated = true;

        if (idleParticles != null)
            idleParticles.Stop();

        if (activatedParticles != null)
            activatedParticles.Play();

    }
}
