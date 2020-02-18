using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    private float maxDistance = 1f;

    [SerializeField]
    private Transform raycastObject;

    private int layerMask;
    private Interactable interactingObject;

    private void Start()
    {
        this.layerMask = 1 << LayerMask.NameToLayer(Constants.Layer_Interactable);
    }

    private void Update()
    {
        if (Physics.Raycast(this.raycastObject.position,
                            this.transform.TransformDirection(Vector3.forward),
                            out RaycastHit raycastHit,
                            this.maxDistance,
                            this.layerMask))
        {
            Debug.DrawRay(this.raycastObject.position, this.transform.TransformDirection(Vector3.forward) * raycastHit.distance, Color.yellow);
            this.interactingObject = raycastHit.collider.gameObject.GetComponent<Interactable>();
            this.interactingObject.SetInteractable(true);
        }
        else
        {
            if (this.interactingObject != null)
            {
                this.interactingObject.SetInteractable(false);
            }
        }

        if (Input.GetButtonDown(Constants.Input_Interact))
        {
            if (this.interactingObject != null)
            {
                this.interactingObject.Interact();
            }
        }
    }
}
