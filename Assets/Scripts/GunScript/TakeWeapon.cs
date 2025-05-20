using UnityEngine;

public class TakeWeapon : MonoBehaviour
{
    public Transform weaponPosition; // Posición del arma en el jugador
    public GameObject crosshairUI;   // Imagen de la mira en el HUD
    public Transform playerCamera;   // Cámara del jugador


    private bool isEquipped = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("¡Has recogido la pistola!");

            // Hacer que la pistola sea hija del jugador y se coloque en la posición correcta
            transform.SetParent(weaponPosition);
            transform.localPosition = new Vector3(0, 0.05f, 0.15f);
            transform.localRotation = Quaternion.Euler(0, 0, 0);

            // Desactivar el collider para que no vuelva a recogerse
            GetComponent<Collider>().enabled = false;

            // Activar la mira en el HUD
            if (crosshairUI != null)
            {
                crosshairUI.SetActive(true);
            }
            else
            {
                Debug.LogWarning("No se ha asignado la mira en el inspector.");
            }

            // Equipar el arma llamando al script Gun
            WeaponShoot gun = GetComponent<WeaponShoot>();
            if (gun != null)
            {
                gun.EquipWeapon();
                isEquipped = true; // Marcar el arma como equipada
            }
            else
            {
                Debug.LogWarning("No se encontró el script 'Gun' en el objeto del arma.");
            }

            // Equipar el arma llamando al script WeaponSwitcher
            WeaponSwitcher weaponSwitcher = other.GetComponent<WeaponSwitcher>();
            if (weaponSwitcher != null)
            {
                weaponSwitcher.PickUpWeapon(gameObject);
                isEquipped = true;
                gameObject.SetActive(false);
            }
            else
            {
                Debug.LogWarning("El jugador no tiene el componente WeaponSwitcher.");
            }
            Debug.Log("Arma recogida: " + gameObject.name);
        }
    }

    void Update()
    {
        // Si el arma está equipada, seguir la rotación de la cámara
        if (isEquipped && playerCamera != null)
        {
            transform.rotation = playerCamera.rotation;
        }
    }
}