using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public GameObject dash; // Arma 1
    public GameObject gun;  // Arma 2
    private GameObject currentWeapon; // Arma actualmente equipada

    void Start()
    {
        // No equipamos ningún arma al principio
        currentWeapon = null;
    }

    void Update()
    {
        // Cambiar a arma 1 cuando presionamos la tecla 1
        if (Input.GetKeyDown(KeyCode.Alpha1) && dash != null && currentWeapon != dash)
        {
            EquipWeapon(dash);
            gun.SetActive(false);
        }

        // Cambiar a arma 2 cuando presionamos la tecla 2
        if (Input.GetKeyDown(KeyCode.Alpha2) && gun != null && currentWeapon != gun)
        {
            EquipWeapon(gun);
            dash.SetActive(false);
        }
    }

    void EquipWeapon(GameObject weaponToEquip)
    {
        if (weaponToEquip == null || weaponToEquip == currentWeapon)
            return;

        // Si ya tienes un arma equipada, desactívala
        if (currentWeapon != null)
        {
            currentWeapon.SetActive(false);
        }

        // Ahora, equipamos el nuevo arma
        currentWeapon = weaponToEquip;
        currentWeapon.SetActive(true);

        // Habilitar o deshabilitar el dash según el arma equipada
        PlayerDash playerDash = GetComponent<PlayerDash>();
        if (playerDash != null)
        {
            bool enableDash = (currentWeapon == dash);  // Solo habilitar el dash si tienes el arma 1
            playerDash.EnableDash(enableDash);
        }

        Debug.Log("Arma equipada: " + currentWeapon.name);
    }

    public void PickUpWeapon(GameObject weapon)
    {
        // Si no tienes ninguna arma aún (arma 1)
        if (dash == null)
        {
            dash = weapon;
            EquipWeapon(dash);
            return;
        }

        // Si tienes solo el arma 1, recoges el arma 2
        if (gun == null)
        {
            gun = weapon;
            gun.SetActive(false); // Ocultar hasta que se equipe
            EquipWeapon(gun);     // Equipar la nueva (arma 2)
            if (dash != null)
                dash.SetActive(false); // Desactivar el arma 1, solo puede haber uno activo a la vez
            return;
        }

        // Si ya tienes ambas armas, no hagas nada
        Debug.LogWarning("No se ha podido recoger el arma, ya tienes ambas.");
    }
}
