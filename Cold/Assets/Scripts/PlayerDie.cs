using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerDie : MonoBehaviour
{
    private Animator playerAnimator;
    private PlayerMovement playerMovement;

    private void Start()
    {
        this.playerAnimator = GetComponent<Animator>();
        this.playerMovement = GetComponent<PlayerMovement>();
    }

    public void Die()
    {
        this.playerAnimator.SetBool(Constants.Anim_PlayerDead, true);
        this.playerMovement.CanMove = false;
    }
}
