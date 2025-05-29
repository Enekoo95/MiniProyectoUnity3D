using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public GameObject dash; // Arma 1
    public GameObject gun;  // Arma 2
    private GameObject currentWeapon; // Arma actualmente equipada

    void Start()
    {
        currentWeapon = null;
        enabled = false; // Este script comienza desactivado
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && dash != null && currentWeapon != dash)
        {
            EquipWeapon(dash);
            if (gun != null) gun.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && gun != null && currentWeapon != gun)
        {
            EquipWeapon(gun);
            if (dash != null) dash.SetActive(false);
        }
    }

    void EquipWeapon(GameObject weaponToEquip)
    {
        if (weaponToEquip == null || weaponToEquip == currentWeapon)
            return;

        if (currentWeapon != null)
        {
            currentWeapon.SetActive(false);
        }

        currentWeapon = weaponToEquip;
        currentWeapon.SetActive(true);

        PlayerDash playerDash = GetComponent<PlayerDash>();
        if (playerDash != null)
        {
            bool enableDash = (currentWeapon == dash);
            playerDash.EnableDash(enableDash);
        }

        Debug.Log("Arma equipada: " + currentWeapon.name);
    }

    public void PickUpWeapon(GameObject weapon)
    {
        if (dash == null)
        {
            dash = weapon;
            EquipWeapon(dash);
            return;
        }

        if (gun == null)
        {
            gun = weapon;
            gun.SetActive(false);
            EquipWeapon(gun);
            if (dash != null) dash.SetActive(false);
            return;
        }

        Debug.LogWarning("No se ha podido recoger el arma, ya tienes ambas.");
    }
}
