using UnityEngine;

public class ButtonSwitch : MonoBehaviour
{
    public GameObject doorToggle;   // Puerta que este bot�n controla
    public GameObject doorToggle2;   
    public ButtonSwitch linkedButton; // Otro bot�n que controla otra puerta

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet")) // Aseg�rate de que la bala tenga el tag "Bullet"
        {
            // Alternar la puerta de este bot�n
            ToggleDoor();

            // Alternar la puerta del bot�n vinculado si existe
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
            Debug.Log("Puerta " + doorToggle.name + " ahora est� " + (!isActive ? "activa" : "desactivada"));
        }
    }
}
