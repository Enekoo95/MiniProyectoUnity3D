using UnityEngine;
using UnityEngine.UI;

public class WinZone : MonoBehaviour
{
    public GameObject winScreenUI; // Asigna el panel de victoria aquí

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("¡Ganaste!");
            if (winScreenUI != null)
            {
                winScreenUI.SetActive(true);
                Time.timeScale = 0f; // Pausa el juego si quieres
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
}