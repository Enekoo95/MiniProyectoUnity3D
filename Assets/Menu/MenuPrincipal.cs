using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public GameObject panelMenuPrincipal;
    public GameObject panelControles;

    private void Start()
    {
        // Only panel of main menu
        panelMenuPrincipal.SetActive(true);
        panelControles.SetActive(false);
    }

    public void Jugar()
    {
        SceneManager.LoadScene("Scene0"); // Game
    }

    public void Salir()
    {
        Application.Quit();
        Debug.Log("Saliendo del juego...");
    }

    public void Controles()
    {
        panelMenuPrincipal.SetActive(false);
        panelControles.SetActive(true);
    }

    public void VolverAlMenu()
    {
        panelControles.SetActive(false);
        panelMenuPrincipal.SetActive(true);
    }
}
