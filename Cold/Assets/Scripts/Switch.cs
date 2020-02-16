using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : Interactable
{
    [SerializeField]
    private Animator handleAnimator;

    [SerializeField]
    private ConnectedToSwitch[] connectedToSwitchArray;

    public override void Interact()
    {
        base.Interact();
        this.handleAnimator.SetTrigger(Constants.Anim_Switch);

        foreach (var item in this.connectedToSwitchArray)
        {
            item.OnSwitchFlip();
        }
    }
}
