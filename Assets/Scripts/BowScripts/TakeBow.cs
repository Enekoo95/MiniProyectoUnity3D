using UnityEngine;

public class TakeBow : MonoBehaviour
{
    public Transform bowPosition; // Referencia a la posici�n donde se colocar� el arco

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Asegurarse de que el jugador tenga el tag "Player"
        {
            Debug.Log("�Has recogido el arco!");

            // Hacer que el arco sea hijo del jugador y se coloque en la posici�n correcta
            transform.SetParent(bowPosition);
            transform.localPosition = new Vector3(0.3f, 0, 0.5f); 
            transform.localRotation = Quaternion.Euler(0,260,0); // Se alinea con la rotaci�n

            // Desactiva el collider para que no vuelva a recogerse
            GetComponent<Collider>().enabled = false;
        }
    }
}