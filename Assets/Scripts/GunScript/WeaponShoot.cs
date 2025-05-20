using UnityEngine;

public class WeaponShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 20f;
    public float fireRate = 0.1f;
    public int totalAmmo = 10;   // Total de balas disponibles
    private float nextFireTime = 0f;
    public bool isEquipped = false;  // Verifica si el arma est� equipada

    void Start()
    {
        // Iniciar sin equipar el arma
        isEquipped = false;
    }

    void Update()
    {
        // No hacer nada si el arma no est� equipada
        if (!isEquipped) return;

        // Detectar clic izquierdo y verificar si hay balas disponibles
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            if (totalAmmo > 0)
            {
                Shoot();
                nextFireTime = Time.time + fireRate;
            }
            else
            {
                Debug.Log("�Sin balas! No hay m�s munici�n.");
            }
        }
    }

    void Shoot()
    {
        // Crear la bala en el punto de disparo
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Aplicar una fuerza de impulso en la direcci�n hacia adelante
            rb.AddForce(firePoint.forward * bulletSpeed, ForceMode.Impulse);
        }

        // Reducir el conteo de balas
        totalAmmo--;

        // Destruir la bala despu�s de 2 segundos
        Destroy(bullet, 2f);

        Debug.Log("Disparo realizado. Balas restantes: " + totalAmmo);
    }

    // M�todo para equipar el arma
    public void EquipWeapon()
    {
        isEquipped = true;
        Debug.Log("�Arma equipada!");
    }

    public void RefillAmmo(int amount)
    {
        totalAmmo += amount;
        Debug.Log("Munici�n recargada. Total actual: " + totalAmmo);
    }
}
