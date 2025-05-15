using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    public GameObject menuPausaUI;
    private bool juegoPausado = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (juegoPausado)
                Reanudar();
            else
                Pausar();
        }
    }

    public void Reanudar()
    {
        menuPausaUI.SetActive(false);
        Time.timeScale = 1f;
        juegoPausado = false;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Pausar()
    {
        menuPausaUI.SetActive(true);
        Time.timeScale = 0f;
        juegoPausado = true;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void IrOpciones()
    {
        Time.timeScale = 1f;  // reanuda tiempo antes de cambiar escena
        SceneManager.LoadScene("OptionsScene");  // Cambia el nombre por el de tu escena opciones
    }

    public void VolverMenu()
    {
        Time.timeScale = 1f;  // reanuda tiempo antes de cambiar escena
        SceneManager.LoadScene("MenuScene");  // Cambia por el nombre de tu escena menú principal
    }

    public void SalirJuego()
    {
        Application.Quit();
        Debug.Log("Saliendo del juego...");
    }
}
