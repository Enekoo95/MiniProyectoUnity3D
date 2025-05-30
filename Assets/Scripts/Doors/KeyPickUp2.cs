using UnityEngine;

public class KeyPickup2 : MonoBehaviour
{
    public string keyID = "DoorKey";
    public GameObject doorToDestroy;
    public GameObject wall1;
    public GameObject wall2;
    public GameObject appearWall1;
    public GameObject appearWall2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();
            if (inventory != null)
            {
                inventory.AddKey(keyID);
                Debug.Log("Llave recogida");

                if (doorToDestroy != null) //Destruye la puerta
                {
                    Destroy(doorToDestroy);
                    Debug.Log("¡Puerta destruida al recoger la llave!");
                }

                if (wall1 != null) //Destruye la pared
                {
                    Destroy(wall1);
                    Debug.Log("¡Pared destruida al recoger la llave!");
                }

                if (wall2 != null) //Destruye la pared
                {
                    Destroy(wall2);
                    Debug.Log("¡Pared destruida al recoger la llave!");
                }

                if (appearWall1 != null) //Aparece pared
                {
                    appearWall1.SetActive(true);
                    Debug.Log("¡appearWall1 activada!");
                }

                if (appearWall2 != null) //Aparece pared
                {
                    appearWall2.SetActive(true);
                    Debug.Log("¡appearWall2 activada!");
                }

                Destroy(gameObject); // Destruye la llave
            }
        }
    }
}
