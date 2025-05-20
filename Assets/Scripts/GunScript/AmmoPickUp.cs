using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public int ammoAmount = 10; // Cantidad que recarga

    private void OnTriggerEnter(Collider other)
    {
        WeaponShoot gun = other.GetComponentInChildren<WeaponShoot>();

        if (gun != null && gun.isEquipped)
        {
            gun.RefillAmmo(ammoAmount);
            Debug.Log("¡Munición recogida!");


            Destroy(gameObject);
        }
    }
}
