using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public GameObject panelMenuPrincipal;
    public GameObject panelControles;
    public AudioSource musicaFondo;

    private void Start()
    {
        panelMenuPrincipal.SetActive(true);
        panelControles.SetActive(false);

        if (musicaFondo != null && !musicaFondo.isPlaying)
        {
            musicaFondo.loop = true;
            musicaFondo.Play();
        }
    }

    public void Jugar()
    {
        SceneManager.LoadScene("Scene0");
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
