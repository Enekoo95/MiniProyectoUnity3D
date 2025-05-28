using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    public GameObject menuPausaUI;
    public GameObject panelControlesUI;
    public PlayerMovement playerMovementScript;

    private bool juegoPausado = false;

    void Start()
    {
        // Al iniciar, aseguramos que el panel de controles está oculto
        if (panelControlesUI != null)
            panelControlesUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (panelControlesUI.activeSelf)
            {
                // Si está en el panel de controles, volver al menú de pausa
                VolverDeControles();
            }
            else
            {
                if (juegoPausado)
                    Reanudar();
                else
                    Pausar();
            }
        }
    }

    public void Reanudar()
    {
        menuPausaUI.SetActive(false);
        Time.timeScale = 1f;
        juegoPausado = false;

        if (playerMovementScript != null)
            playerMovementScript.enabled = true;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Pausar()
    {
        menuPausaUI.SetActive(true);
        Time.timeScale = 0f;
        juegoPausado = true;

        if (playerMovementScript != null)
            playerMovementScript.enabled = false;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void MostrarControles()
    {
        menuPausaUI.SetActive(false);
        panelControlesUI.SetActive(true);
    }

    public void VolverDeControles()
    {
        panelControlesUI.SetActive(false);
        menuPausaUI.SetActive(true);
    }

    public void VolverMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuScene");
    }

    public void SalirJuego()
    {
        Application.Quit();
        Debug.Log("Saliendo del juego...");
    }
}
