using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AudioSource))]
public class PlayerDash : MonoBehaviour
{
    [Header("Dash Settings")]
    public float dashSpeed = 15f;
    public float minDashDistance = 2f;
    public float maxDashDistance = 8f;
    public float dashCooldown = 1f;
    public float objectOffset = 0.5f;
    public float dashDetectionRadius = 1f;
    public float maxDashDuration = 1f;
    public int dashDamage = 10;

    [Header("UI")]
    public Slider dashChargeSlider;

    [Header("Audio")]
    public AudioClip dashSound;

    private AudioSource audioSource;
    private CharacterController controller;
    private bool isDashing = false;
    public bool IsDashing => isDashing;

    private bool canDash = false;
    private bool hasSwordEquipped = false;

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
        audioSource = GetComponent<AudioSource>();
        defaultLayer = gameObject.layer;

        if (dashChargeSlider != null)
        {
            dashChargeSlider.minValue = 0f;
            dashChargeSlider.maxValue = maxDashDuration;
            dashChargeSlider.value = 0f;
            dashChargeSlider.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (controller == null) return;

        bool isChargingDash = hasSwordEquipped && canDash && Input.GetKey(KeyCode.Space) && Time.time > lastDashTime + dashCooldown;

        if (isChargingDash)
        {
            chargeTime += Time.deltaTime;
            chargeTime = Mathf.Min(chargeTime, maxDashDuration);

            if (dashChargeSlider != null)
            {
                dashChargeSlider.gameObject.SetActive(true);
                dashChargeSlider.value = chargeTime;
            }
        }
        else if (!isDashing && dashChargeSlider != null && dashChargeSlider.gameObject.activeSelf)
        {
            dashChargeSlider.gameObject.SetActive(false);
            dashChargeSlider.value = 0f;
        }

        if (hasSwordEquipped && canDash && Input.GetKeyUp(KeyCode.Space) && Time.time > lastDashTime + dashCooldown)
        {
            isDashing = true;
            dashDistance = Mathf.Lerp(minDashDistance, maxDashDistance, chargeTime / maxDashDuration);
            lastDashTime = Time.time;
            dashDirection = transform.forward.normalized;
            fixedYPosition = transform.position.y;
            gameObject.layer = LayerMask.NameToLayer("DashingPlayer");
            chargeTime = 0f;

            if (dashChargeSlider != null)
            {
                dashChargeSlider.gameObject.SetActive(false);
                dashChargeSlider.value = 0f;
            }

            // Reproducir sonido de dash
            if (dashSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(dashSound);
            }
        }

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
            move.y = 0;
            Vector3 targetPosition = transform.position + move;
            targetPosition.y = fixedYPosition;
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

    public void EnableDash(bool enabled)
    {
        canDash = enabled;
        if (!enabled)
        {
            isDashing = false;
            chargeTime = 0f;
        }

        Debug.Log("Dash " + (enabled ? "habilitado" : "deshabilitado"));
    }

    public void SetSwordEquipped(bool equipped)
    {
        hasSwordEquipped = equipped;
        Debug.Log("Espada " + (equipped ? "equipada" : "desequipada"));
    }
}
