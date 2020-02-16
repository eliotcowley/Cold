using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class JailCellDoor : ConnectedToSwitch
{
    private Animator animator;

    private void Start()
    {
        this.animator = GetComponent<Animator>();
    }

    public override void OnSwitchFlip()
    {
        this.animator.SetTrigger(Constants.Anim_JailCellDoorMoveDown);
    }
}
