using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10f;

    [SerializeField]
    private float turnSpeed = 10f;

    [SerializeField]
    private float moveSmooth = 10f;

    [SerializeField]
    private float moveThreshold = 0.05f;

    [SerializeField]
    private float fallThreshold = -0.1f;

    private Rigidbody rb;
    private Animator animator;
    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        this.rb = GetComponent<Rigidbody>();
        this.animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        //float horizontal = GameController.Instance.IsPaused ? 0f : Input.GetAxis(Constants.Input_Horizontal) * Time.fixedDeltaTime * moveSpeed;
        //float vertical = GameController.Instance.IsPaused ? 0f : Input.GetAxis(Constants.Input_Vertical) * Time.fixedDeltaTime * moveSpeed;

        float horizontal = Input.GetAxis(Constants.Input_Horizontal) * Time.fixedDeltaTime * this.moveSpeed;
        float vertical = Input.GetAxis(Constants.Input_Vertical) * Time.fixedDeltaTime * this.moveSpeed;

        Vector3 moveVector = new Vector3(horizontal, 0f, vertical);
        Vector3 targetVector = moveVector + this.rb.position;
        Vector3 smoothedPosition = Vector3.SmoothDamp(this.rb.position, targetVector, ref this.velocity, this.moveSmooth * Time.fixedDeltaTime);
        this.rb.position = smoothedPosition;

        if (moveVector.sqrMagnitude > this.moveThreshold)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveVector, Vector3.up);
            this.rb.rotation = Quaternion.Lerp(this.rb.rotation, targetRotation, this.moveSmooth * this.turnSpeed * Time.fixedDeltaTime);
        }

        this.animator.SetFloat(Constants.Anim_Speed, this.velocity.sqrMagnitude);
        this.animator.SetBool(Constants.Anim_Grounded, (this.rb.velocity.y < this.fallThreshold) ? false : true);
    }
}
