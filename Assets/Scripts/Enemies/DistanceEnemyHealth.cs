using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceEnemyHealth : MonoBehaviour
{
    public int health = 1;
    private int currentHealth;
    public int damageAmount = 1;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("¡Impacto en el enemigo! Vida restante: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemigo eliminado");
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Bullet bullet = collision.gameObject.GetComponent<Bullet>();
        if (bullet != null)
        {
            TakeDamage(bullet.damage);
            Destroy(collision.gameObject);
        }
    }
}
