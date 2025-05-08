using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    public GameObject puerta;  // Asignar la puerta desde el inspector
    private bool puertaAbierta = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            puertaAbierta = !puertaAbierta;
            puerta.SetActive(!puertaAbierta);
            Debug.Log("¡Botón activado! Puerta " + (puertaAbierta ? "abierta" : "cerrada"));
        }
    }
}
