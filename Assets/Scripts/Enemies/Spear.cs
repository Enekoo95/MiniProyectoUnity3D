using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
    public float lifeTime = 5f; // Tiempo máximo antes de destruirse si no choca

    void Start()
    {
        Destroy(gameObject, lifeTime); // Por si nunca choca, se destruye después de X segundos
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Aquí puedes poner lógica para hacer daño si es el jugador, por ejemplo:
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("¡Golpeó al jugador!");
            // Aquí podrías hacer daño al jugador si tienes su script
        }

        // En cualquier caso, se destruye
        Destroy(gameObject);
    }
}
