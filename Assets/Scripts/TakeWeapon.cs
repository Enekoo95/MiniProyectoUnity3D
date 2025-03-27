using UnityEngine;

public class TakeWeapon : MonoBehaviour
{
    public Transform weaponPosition; // Referencia a la posición donde se colocará el arco
    public GameObject crosshairUI; // Referencia a la imagen de la mira en el Canvas

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Asegurarse de que el jugador tenga el tag "Player"
        {
            Debug.Log("¡Has recogido la pistola");

            // Hacer que la pistola sea hijo del jugador y se coloque en la posición correcta
            transform.SetParent(weaponPosition);
            transform.localPosition = new Vector3(0, 0.05f, 0.15f); 
            transform.localRotation = Quaternion.Euler(0,0,0); // Se alinea con la rotación

            // Desactiva el collider para que no vuelva a recogerse
            GetComponent<Collider>().enabled = false;
        }
        // Activar la mira en el HUD
        if (crosshairUI != null)
        {
            crosshairUI.SetActive(true);
        }
        else
        {
            Debug.LogWarning("No se ha asignado la mira en el inspector.");
        }
    }
}