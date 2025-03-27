using UnityEngine;

public class TakeBow : MonoBehaviour
{
    public Transform bowPosition; // Referencia a la posición donde se colocará el arco

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Asegurarse de que el jugador tenga el tag "Player"
        {
            Debug.Log("¡Has recogido el arco!");

            // Hacer que el arco sea hijo del jugador y se coloque en la posición correcta
            transform.SetParent(bowPosition);
            transform.localPosition = new Vector3(0.3f, 0, 0.5f); 
            transform.localRotation = Quaternion.Euler(0,260,0); // Se alinea con la rotación

            // Desactiva el collider para que no vuelva a recogerse
            GetComponent<Collider>().enabled = false;
        }
    }
}