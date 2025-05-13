using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Llamar al m�todo OnDeath para que el jugador respawnee
            PlayerRespawn respawn = other.GetComponent<PlayerRespawn>();
            if (respawn != null)
            {
                respawn.OnDeath();
            }

            // Opcional: Destruir el jugador
            Destroy(other.gameObject);

            Debug.Log("�Jugador ha ca�do en la piscina y ha muerto!");
        }

        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            Debug.Log("�Enemigo ha ca�do en la piscina y ha muerto!");
        }
    }
}
