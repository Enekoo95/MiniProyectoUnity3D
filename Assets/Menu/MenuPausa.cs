using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    public GameObject menuPausaUI;
    public GameObject panelControlesUI;
    public PlayerMovement playerMovementScript;
    public AudioSource musicaMenuPausa;  // Referencia al AudioSource para la música

    private bool juegoPausado = false;

    void Start()
    {
        // Aseguramos que el panel de controles está oculto al inicio
        if (panelControlesUI != null)
            panelControlesUI.SetActive(false);

        // Nos aseguramos de que la música esté detenida al inicio
        if (musicaMenuPausa != null)
        {
            musicaMenuPausa.loop = true;       // Que la música se repita mientras esté el menú
            musicaMenuPausa.Stop();            // No reproducirla al principio
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (panelControlesUI.activeSelf)
            {
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

        if (musicaMenuPausa != null && musicaMenuPausa.isPlaying)
            musicaMenuPausa.Stop();

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

        if (musicaMenuPausa != null && !musicaMenuPausa.isPlaying)
            musicaMenuPausa.Play();

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
