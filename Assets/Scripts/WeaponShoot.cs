using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShoot : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 0.5f; // Tiempo entre disparos
    private float nextTimeToFire = 0f;

    public Camera cam;
    public ParticleSystem muzzleFlash; // Efecto de disparo
    public GameObject impactEffect; // Efecto de impacto

    void Start()
    {
        if (cam == null)
        {
            cam = Camera.main;
            if (cam == null)
            {
                Debug.LogError("No se ha asignado la cámara en WeaponShoot.");
            }
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        if (muzzleFlash != null)
        {
            muzzleFlash.Play(); // Reproduce el efecto de disparo
        }

        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            Debug.Log("Impacto en: " + hit.transform.name);

            // Verificar si golpeamos un enemigo y aplicarle daño
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            // Instanciar un efecto de impacto
            if (impactEffect != null)
            {
                GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 1f); // Destruir después de 1 segundo
            }
        }
    }
}