using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public int ammoAmount = 10; // Cantidad que recarga

    private void OnTriggerEnter(Collider other)
    {
        // Buscar en el objeto que entra y tambi�n en sus hijos
        WeaponShoot gun = other.GetComponentInChildren<WeaponShoot>();
        if (gun == null)
        {
            gun = other.GetComponent<WeaponShoot>();
        }

        if (gun != null && gun.isEquipped)
        {
            gun.RefillAmmo(ammoAmount);
            Debug.Log("�Munici�n recogida!");
            Destroy(gameObject);
        }
    }
}
