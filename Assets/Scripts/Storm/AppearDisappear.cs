using System.Collections;
using UnityEngine;

public class AppearDisappear : MonoBehaviour
{
    public GameObject objeto;                // Objeto que aparecer� y desaparecer�
    public float tiempoMin = 1.0f;            // Tiempo m�nimo entre apariciones
    public float tiempoMax = 3.0f;            // Tiempo m�ximo entre apariciones
    public Vector3 areaMin = new Vector3(-10, 15, 0); // L�mite inferior del �rea 
    public Vector3 areaMax = new Vector3(-40, 15, 20);   // L�mite superior del �rea 

    private void Start()
    {
        // Inicia la corutina
        StartCoroutine(AppearDisappearCoroutine());
    }

    private IEnumerator AppearDisappearCoroutine()
    {
        while (true)
        {
            // Generar un tiempo de espera aleatorio
            float tiempoEspera = Random.Range(tiempoMin, tiempoMax);

            // Esperar el tiempo aleatorio antes de aparecer
            yield return new WaitForSeconds(tiempoEspera);

            // Generar una posici�n aleatoria 
            float posX = Random.Range(areaMin.x, areaMax.x);
            float posY = Random.Range(areaMin.y, areaMax.y);
            float posZ = Random.Range(areaMin.z, areaMax.z);
            Vector3 posicionAleatoria = new Vector3(posX, posY, posZ);

            // Mover el objeto a la posici�n aleatoria y hacerlo visible
            objeto.transform.position = posicionAleatoria;
            objeto.SetActive(true);

            // Mantener el objeto visible por un tiempo fijo
            yield return new WaitForSeconds(1f);

            // Desactivar el objeto 
            objeto.SetActive(false);
        }
    }
}
