using UnityEngine;

public class ButtonSwitch : MonoBehaviour
{
    public GameObject doorToggle;   // Puerta que este botón controla
    public GameObject doorToggle2;   
    public ButtonSwitch linkedButton; // Otro botón que controla otra puerta

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet")) // Asegúrate de que la bala tenga el tag "Bullet"
        {
            // Alternar la puerta de este botón
            ToggleDoor();

            // Alternar la puerta del botón vinculado si existe
            if (linkedButton != null)
            {
                linkedButton.ToggleDoor();
            }
        }
    }

    public void ToggleDoor()
    {
        if (doorToggle != null)
        {
            bool isActive = doorToggle.activeSelf;
            doorToggle.SetActive(!isActive); // Cambia el estado de la puerta
            Debug.Log("Puerta " + doorToggle.name + " ahora está " + (!isActive ? "activa" : "desactivada"));
        }
    }
}
