using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerRespawn respawn = other.GetComponent<PlayerRespawn>();
            if (respawn != null)
            {
                respawn.OnDeath();
            }

            Debug.Log("�Jugador entr� en la KillZone!");
        }

        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            Debug.Log("�Enemigo destruido por la KillZone!");
        }
    }
}
