using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public string keyID = "DoorKey";
    public GameObject doorToDestroy; 
    public GameObject wallToDestroy;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();
            if (inventory != null)
            {
                inventory.AddKey(keyID);
                Debug.Log("Llave recogida");

                if (doorToDestroy != null)
                {
                    Destroy(doorToDestroy); // Destruye la puerta directamente
                    Debug.Log("¡Puerta destruida al recoger la llave!");
                }
                if (wallToDestroy != null)
                {
                    Destroy(wallToDestroy); // Destruye la puerta directamente
                    Debug.Log("¡Puerta destruida al recoger la llave!");
                }

                Destroy(gameObject); // Destruye la llave
            }
        }
    }
}