using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    public GameObject puerta;  // Asigna la puerta desde el inspector
    public GameObject puerta2;
    private bool puertaAbierta = false;
    private bool puertaAbierta2 = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            puertaAbierta = !puertaAbierta;
            puerta.SetActive(!puertaAbierta);
            Debug.Log("¡Botón activado! Puerta " + (puertaAbierta ? "abierta" : "cerrada"));
        }
        if (collision.gameObject.CompareTag("Bullet"))
        {
            puertaAbierta2 = !puertaAbierta2;
            puerta2.SetActive(!puertaAbierta2);
            Debug.Log("¡Botón activado! Puerta " + (puertaAbierta2 ? "abierta" : "cerrada"));
        }
    }
}
