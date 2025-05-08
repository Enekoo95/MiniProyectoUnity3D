using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene("Scene0"); // Reemplaza con el nombre real
    }

    public void Salir()
    {
        Application.Quit();
        Debug.Log("Saliendo del juego...");
    }

    public void Opciones()
    {
        Debug.Log("Menú de opciones aún no implementado.");
    }
}