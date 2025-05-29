using UnityEngine;

public class DashItem : MonoBehaviour
{
    public Transform dashPosition;
    private void OnTriggerEnter(Collider other)
    {
        PlayerDash playerDash = other.GetComponent<PlayerDash>();

        if (playerDash != null)
        {
            // Pegar la espada al jugador y habilitar el dash
            playerDash.PickUpObject(transform);
            playerDash.SetSwordEquipped(true);

            Debug.Log("Espada recogida y equipada");
        }

        if (other.CompareTag("Player"))
        {
            Debug.Log("¡Has recogido la espada!");

            // Hacer que la espada sea hija del jugador y se coloque en la posición correcta
            transform.SetParent(dashPosition);
            transform.localPosition = new Vector3(0, 0.05f, 0.15f);
            transform.localRotation = Quaternion.Euler(0, 0, 0);

            // Desactivar el collider para que no vuelva a recogerse
            GetComponent<Collider>().enabled = false;

        }
    }
}