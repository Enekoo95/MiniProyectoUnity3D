using UnityEngine;

public class TakeWeapon : MonoBehaviour
{
    public Transform weaponPosition;
    public GameObject crosshairUI;
    public Transform playerCamera;

    private bool isEquipped = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("¡Has recogido el arma!");

            transform.SetParent(weaponPosition);
            transform.localPosition = new Vector3(0, 0.05f, 0.15f);
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            GetComponent<Collider>().enabled = false;

            if (crosshairUI != null)
            {
                crosshairUI.SetActive(true);
            }
            else
            {
                Debug.LogWarning("No se ha asignado la mira en el inspector.");
            }

            WeaponShoot gun = GetComponent<WeaponShoot>();
            if (gun != null)
            {
                gun.EquipWeapon();
                isEquipped = true;
            }
            else
            {
                Debug.LogWarning("No se encontró el script 'WeaponShoot' en el objeto del arma.");
            }

            WeaponSwitcher weaponSwitcher = other.GetComponent<WeaponSwitcher>();
            if (weaponSwitcher != null)
            {
                weaponSwitcher.enabled = true; // ACTIVAMOS el script
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
        if (isEquipped && playerCamera != null)
        {
            transform.rotation = playerCamera.rotation;
        }
    }
}
