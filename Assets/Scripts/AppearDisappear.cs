using System.Collections;
using UnityEngine;

public class AppearDisappear : MonoBehaviour
{
    public GameObject objeto;                // Objeto que aparecerá y desaparecerá
    public float tiempoMin = 1.0f;            // Tiempo mínimo entre apariciones
    public float tiempoMax = 3.0f;            // Tiempo máximo entre apariciones
    public Vector3 areaMin = new Vector3(-10, 15, 0); // Límite inferior del área 
    public Vector3 areaMax = new Vector3(-40, 15, 20);   // Límite superior del área 

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

            // Generar una posición aleatoria 
            float posX = Random.Range(areaMin.x, areaMax.x);
            float posY = Random.Range(areaMin.y, areaMax.y);
            float posZ = Random.Range(areaMin.z, areaMax.z);
            Vector3 posicionAleatoria = new Vector3(posX, posY, posZ);

            // Mover el objeto a la posición aleatoria y hacerlo visible
            objeto.transform.position = posicionAleatoria;
            objeto.SetActive(true);

            // Mantener el objeto visible por un tiempo fijo
            yield return new WaitForSeconds(1f);

            // Desactivar el objeto 
            objeto.SetActive(false);
        }
    }
}
