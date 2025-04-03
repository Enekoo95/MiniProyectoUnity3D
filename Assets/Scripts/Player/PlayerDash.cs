using UnityEngine;
using System.Collections.Generic;

public class PlayerDash : MonoBehaviour
{
    public float dashSpeed = 15f;
    public float minDashDistance = 2f;
    public float maxDashDistance = 8f;
    public float dashCooldown = 1f;
    public float objectOffset = 0.5f;
    public float dashDetectionRadius = 1f;
    public float maxDashDuration = 1f;
    public int dashDamage = 10;

    private CharacterController controller;
    private bool isDashing = false;
    private bool canDash = false;
    private float dashDistance = 0f;
    private float lastDashTime = -100f;
    private float chargeTime = 0f;
    private Vector3 dashDirection;
    private float fixedYPosition;
    private Transform carriedObject;
    private int defaultLayer;
    private HashSet<Enemy> hitEnemies = new HashSet<Enemy>();

    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (controller == null)
        {
            Debug.LogError("No se encontró un CharacterController en " + gameObject.name);
        }

        defaultLayer = gameObject.layer;
    }

    void Update()
    {
        if (controller == null) return;

        // Cargar el dash
        if (canDash && Input.GetKey(KeyCode.Space) && Time.time > lastDashTime + dashCooldown)
        {
            chargeTime += Time.deltaTime;
            chargeTime = Mathf.Min(chargeTime, maxDashDuration);
        }

        // Iniciar el dash al soltar el botón
        if (canDash && Input.GetKeyUp(KeyCode.Space) && Time.time > lastDashTime + dashCooldown)
        {
            isDashing = true;
            dashDistance = Mathf.Lerp(minDashDistance, maxDashDistance, chargeTime / maxDashDuration);
            lastDashTime = Time.time;
            dashDirection = transform.forward.normalized;
            gameObject.layer = LayerMask.NameToLayer("DashingPlayer");
            chargeTime = 0f;
            fixedYPosition = transform.position.y; // Guardar la posición Y actual
        }

        // Aplicar el dash
        if (isDashing)
        {
            float distanceThisFrame = dashSpeed * Time.deltaTime;
            dashDistance -= distanceThisFrame;

            if (dashDistance <= 0f)
            {
                isDashing = false;
                gameObject.layer = defaultLayer;
                hitEnemies.Clear();
                return;
            }

            Vector3 move = dashDirection * distanceThisFrame;
            move.y = 0;  // Asegurar movimiento horizontal
            Vector3 targetPosition = transform.position + move;
            targetPosition.y = fixedYPosition; // Fijar la posición Y
            controller.Move(targetPosition - transform.position);

            Collider[] hitColliders = Physics.OverlapSphere(transform.position, dashDetectionRadius);
            foreach (Collider collider in hitColliders)
            {
                if (collider.CompareTag("BreakableWall"))
                {
                    Destroy(collider.gameObject);
                    Debug.Log("Pared rota con el dash!");
                }

                if (collider.CompareTag("Enemy"))
                {
                    Enemy enemy = collider.GetComponent<Enemy>();
                    if (enemy != null && !hitEnemies.Contains(enemy))
                    {
                        enemy.TakeDamageFromDash();
                        hitEnemies.Add(enemy);
                        Debug.Log("Enemigo golpeado con el dash!");
                    }
                }
            }
        }

        if (carriedObject != null)
        {
            Vector3 objectPosition = transform.position + transform.forward * objectOffset;
            carriedObject.position = objectPosition;
            carriedObject.rotation = Quaternion.identity;
        }
    }

    public void PickUpObject(Transform obj)
    {
        carriedObject = obj;
        canDash = true;
        Debug.Log("Objeto recogido y dash habilitado");
    }
}
