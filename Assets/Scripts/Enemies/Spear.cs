using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
    public float lifeTime = 5f; // Tiempo m�ximo antes de destruirse si no choca

    void Start()
    {
        Destroy(gameObject, lifeTime); // Por si nunca choca, se destruye despu�s de X segundos
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Aqu� puedes poner l�gica para hacer da�o si es el jugador, por ejemplo:
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("�Golpe� al jugador!");
            // Aqu� podr�as hacer da�o al jugador si tienes su script
        }

        // En cualquier caso, se destruye
        Destroy(gameObject);
    }
}
