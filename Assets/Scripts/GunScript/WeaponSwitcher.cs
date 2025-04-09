using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public GameObject dash; // Arma 1
    public GameObject gun;  // Arma 2
    private GameObject currentWeapon; // Arma actual

    void Start()
    {
        // Inicializa el arma 1 si está disponible
        if (dash != null)
        {
            EquipWeapon(dash);
        }
    }

    void Update()
    {
        // Cambiar a arma 1 cuando presionamos la tecla 1 y si el arma está equipada
        if (Input.GetKeyDown(KeyCode.Alpha1) && dash != null && currentWeapon != dash)
        {
            EquipWeapon(dash);
        }

        // Cambiar a arma 2 cuando presionamos la tecla 2 y si el arma está equipada
        if (Input.GetKeyDown(KeyCode.Alpha2) && gun != null && currentWeapon != gun)
        {
            EquipWeapon(gun);
        }
    }

    // Función que maneja el cambio de arma
    void EquipWeapon(GameObject weaponToEquip)
    {
        if (currentWeapon != null)
        {
            currentWeapon.SetActive(false); // Desactiva el arma actual
        }

        currentWeapon = weaponToEquip; // Establece el arma actual
        currentWeapon.SetActive(true); // Activa la nueva arma
    }

    // Función para recoger el arma
    public void PickUpWeapon(GameObject weapon)
    {
        // Si el arma 1 no está equipada, asignarla a esa ranura
        if (dash == null)
        {
            dash = weapon;
            EquipWeapon(dash); // Equipamos el primer arma directamente
            Debug.Log("Arma 1 recogida: " + dash.name);
        }
        // Si el arma 2 no está equipada, asignarla a esa ranura
        else if (gun == null)
        {
            gun = weapon;
            Debug.Log("Arma 2 recogida: " + gun.name);
        }
        else
        {
            Debug.Log("Ya tienes las dos armas.");
        }
    }
}
