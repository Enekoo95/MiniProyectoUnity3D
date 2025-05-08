using UnityEngine;

public class MenuPausa : MonoBehaviour
{
    public GameObject menuPausaUI;  // Asigna el panel del menú de pausa en el inspector
    private bool juegoPausado = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (juegoPausado)
            {
                Reanudar();
            }
            else
            {
                Pausar();
            }
        }
    }

    public void Reanudar()
    {
        menuPausaUI.SetActive(false);
        Time.timeScale = 1f;  // Reanuda el tiempo
        juegoPausado = false;
    }

    public void Pausar()
    {
        menuPausaUI.SetActive(true);
        Time.timeScale = 0f;  // Pausa el tiempo
        juegoPausado = true;
    }

    public void VolverAlMenu()
    {
        Time.timeScale = 1f;  // Asegúrate de reanudar antes de cambiar de escena
        UnityEngine.SceneManagement.SceneManager.LoadScene("MenuPrincipal");
    }

    public void SalirDelJuego()
    {
        Application.Quit();
        Debug.Log("Saliendo del juego...");
    }
}
