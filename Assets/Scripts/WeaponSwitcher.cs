using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public GameObject dash; // Arma 1
    public GameObject gun; // Arma 2
    private GameObject currentWeapon; // Arma actual

    void Start()
    {
        // Inicializamos con el arma 1 activa
        EquipWeapon(dash);
    }

    void Update()
    {
        // Cambiar a arma 1 cuando presionamos la tecla 1
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipWeapon(dash);
        }

        // Cambiar a arma 2 cuando presionamos la tecla 2
        if (Input.GetKeyDown(KeyCode.Alpha2))
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
            EquipWeapon(dash); // Equipamos el primer arma
        }
        else if (gun == null)
        {
            gun = weapon;
        }
    }
}
