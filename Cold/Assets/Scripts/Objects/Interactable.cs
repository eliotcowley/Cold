using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField]
    private Transform mainCamera;

    [SerializeField]
    private Transform canvas;

    [SerializeField]
    private bool oneTimeInteract = true;

    private bool interactable = false;
    private bool hasInteracted = false;

    public void SetInteractable(bool interactable)
    {
        if (interactable && this.oneTimeInteract && this.hasInteracted)
        {
            return;
        }

        this.interactable = interactable;
        this.canvas.gameObject.SetActive(interactable);
    }

    public virtual void Interact()
    {
        if (!this.interactable)
        {
            return;
        }

        this.hasInteracted = true;

        if (this.oneTimeInteract)
        {
            this.interactable = false;
            this.canvas.gameObject.SetActive(false);
        }
    }

    private void LateUpdate()
    {
        this.canvas.LookAt(this.canvas.position + this.mainCamera.forward);
    }
}
