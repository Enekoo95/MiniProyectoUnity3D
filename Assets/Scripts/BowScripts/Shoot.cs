using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField]
    float pullSpeed;
    [SerializeField]
    GameObject arrowPrefab;
    [SerializeField]
    GameObject arrow;
    [SerializeField]
    int numberOfArrows = 10;
    [SerializeField]
    GameObject bow;
    bool arrowSlotted = false;
    float pullAmount = 0;

    // Start is called before the first frame update
    void Start()
    {
        SpawnArrow();
    }

    // Update is called once per frame
    void Update()
    {
        ShootLogic();
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.None; // Permitir mover el mouse
            Cursor.visible = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            Cursor.lockState = CursorLockMode.Locked; // Ocultar y bloquear el mouse
            Cursor.visible = false;
        }

    }
    void SpawnArrow()
    {
        if (numberOfArrows > 0)
        {
            arrowSlotted = true;
            arrow = Instantiate(arrowPrefab, transform.position, transform.rotation) as GameObject;
            arrow.transform.parent = transform;
            transform.localPosition = new Vector3(0.2f, 0.3f, 0f);
            transform.localRotation = Quaternion.Euler(0, 180, 90); 
        }
    }
    void ShootLogic()
    {
        if (numberOfArrows > 0)
        {
            if (pullAmount > 100)
            {
                pullAmount = 100;

                SkinnedMeshRenderer _bowSkin = bow.transform.GetComponent<SkinnedMeshRenderer>();
                SkinnedMeshRenderer _arrowSkin = arrow.transform.GetComponent<SkinnedMeshRenderer>();
                Rigidbody _arrowRigidbody = arrow.transform.GetComponent<Rigidbody>();
                ProyectileAddForce _arrowProyectile = GetComponent<ProyectileAddForce>();


                if (Input.GetMouseButton(0))
                {
                    pullAmount += Time.deltaTime * pullSpeed;
                }
                if (Input.GetMouseButtonUp(0))
                {
                    arrowSlotted = false;
                    _arrowRigidbody.isKinematic = false;
                    arrow.transform.parent = null;
                    numberOfArrows -= 1;
                    _arrowProyectile.shootForce = _arrowProyectile.shootForce * ((pullAmount / 100)+.05f);

                    pullAmount = 0;

                    _arrowProyectile.enabled = true;
                }
                _bowSkin.SetBlendShapeWeight(0, pullAmount);
                _arrowSkin.SetBlendShapeWeight(0, pullAmount);

                if (Input.GetMouseButtonDown(0) && arrowSlotted == false)
                    SpawnArrow();
            }

        }
    }

}
