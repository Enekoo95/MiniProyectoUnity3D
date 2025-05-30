using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class WeaponShoot : MonoBehaviour
{
    [Header("Disparo")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 20f;
    public float fireRate = 0.1f;
    public int totalAmmo = 10;

    [Header("Audio")]
    public AudioClip shootSound;
    public AudioClip emptyAmmoSound;

    private float nextFireTime = 0f;
    private AudioSource audioSource;
    public bool isEquipped = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!isEquipped)
        {
            return;
        }

        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            if (totalAmmo > 0)
            {
                Shoot();
                nextFireTime = Time.time + fireRate;
            }
            else
            {
                Debug.Log("¡Sin balas! No hay más munición.");
                if (emptyAmmoSound != null)
                {
                    audioSource.PlayOneShot(emptyAmmoSound);
                }
            }
        }
    }

    void Shoot()
    {
        Debug.Log("Disparo realizado.");

        if (shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(firePoint.forward * bulletSpeed, ForceMode.Impulse);
        }

        totalAmmo--;
        Destroy(bullet, 2f);
    }

    public void EquipWeapon()
    {
        isEquipped = true;
        Debug.Log("¡Arma equipada!");
    }

    public void RefillAmmo(int amount)
    {
        totalAmmo += amount;
        Debug.Log("Munición recargada. Total actual: " + totalAmmo);
    }
}
