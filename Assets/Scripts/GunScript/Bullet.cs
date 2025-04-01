using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1;  // Daño que inflige la bala

    private void OnCollisionEnter(Collision collision)
    {
        // Verificar si la bala impacta contra el enemigo
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);  // Hacer daño al enemigo
        }

        // Destruir la bala tras el impacto
        Destroy(gameObject);
    }
}