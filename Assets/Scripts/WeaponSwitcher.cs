using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public GameObject dash; // Arma 1
    public GameObject gun;  // Arma 2
    private GameObject currentWeapon; // Arma actual

    void Start()
    {
        // Inicializar el arma 1 si est� disponible
        if (dash != null)
        {
            EquipWeapon(dash);
        }
    }

    void Update()
    {
        // Cambiar a arma 1 cuando presionamos la tecla 1
        if (Input.GetKeyDown(KeyCode.Alpha1) && dash != null)
        {
            EquipWeapon(dash);
        }

        // Cambiar a arma 2 cuando presionamos la tecla 2
        if (Input.GetKeyDown(KeyCode.Alpha2) && gun != null)
        {
            EquipWeapon(gun);
        }
    }

    // Funci�n que maneja el cambio de arma
    void EquipWeapon(GameObject weaponToEquip)
    {
        if (currentWeapon != null)
        {
            currentWeapon.SetActive(false); // Desactiva el arma actual
        }

        currentWeapon = weaponToEquip; // Establece el arma actual
        currentWeapon.SetActive(true); // Activa la nueva arma
    }

    // Funci�n para recoger el arma
    public void PickUpWeapon(GameObject weapon)
    {
        // Si el arma 1 no est� equipada, asignarla a esa ranura
        if (dash == null)
        {
            dash = weapon;
            EquipWeapon(dash); // Equipamos el primer arma directamente
        }
        else if (gun == null)
        {
            gun = weapon;
            gun.SetActive(false); // Desactivamos el arma recogida
            Debug.Log("Desactivo");
        }
    }
}
