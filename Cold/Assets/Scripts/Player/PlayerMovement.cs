using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    public bool CanMove = true;

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

    [SerializeField]
    private float stopSmooth = 2.5f;

    [SerializeField]
    private Transform raycastFeet;

    [SerializeField]
    private float verticalStepDistance;

    private Rigidbody rb;
    private Animator animator;
    private Vector3 velocity = Vector3.zero;
    private int feetRayLayerMask;

    private void Start()
    {
        this.rb = GetComponent<Rigidbody>();
        this.animator = GetComponent<Animator>();
        this.feetRayLayerMask = 1 << LayerMask.NameToLayer(Constants.Layer_Ground);
    }

    private void FixedUpdate()
    {
        bool grounded = false;

        if (!this.CanMove)
        {
            return;
        }

        float horizontal = Input.GetAxis(Constants.Input_Horizontal) * Time.fixedDeltaTime * this.moveSpeed;
        float vertical = Input.GetAxis(Constants.Input_Vertical) * Time.fixedDeltaTime * this.moveSpeed;

        Vector3 moveVector = new Vector3(horizontal, 0f, vertical);
        Vector3 targetVector = moveVector + this.rb.position;
        Quaternion targetRotation = Quaternion.identity;

        if (moveVector != Vector3.zero)
        {
            targetRotation = Quaternion.LookRotation(moveVector, Vector3.up);
        }

        if (moveVector.sqrMagnitude > this.moveThreshold)
        {
            Vector3 smoothedPosition = Vector3.SmoothDamp(this.rb.position, targetVector, ref this.velocity, this.moveSmooth * Time.fixedDeltaTime);
            this.rb.rotation = Quaternion.Lerp(this.rb.rotation, targetRotation, this.moveSmooth * this.turnSpeed * Time.fixedDeltaTime);
            this.rb.position = smoothedPosition;

            if (Physics.Raycast(
                this.raycastFeet.position,
                this.raycastFeet.TransformDirection(Vector3.forward),
                out RaycastHit raycastHit,
                this.verticalStepDistance,
                this.feetRayLayerMask))
            {
                Debug.DrawRay(this.raycastFeet.position, this.raycastFeet.TransformDirection(Vector3.forward) * raycastHit.distance, Color.yellow);
                this.rb.position = new Vector3(this.rb.position.x, raycastHit.point.y, this.rb.position.z);
                grounded = true;
            }
        }
        else
        {
            this.velocity = Vector3.SmoothDamp(this.velocity, Vector3.zero, ref this.velocity, this.stopSmooth * Time.fixedDeltaTime);
        }
        
        this.animator.SetFloat(Constants.Anim_Speed, this.velocity.sqrMagnitude);

        if (!grounded)
        {
            this.animator.SetBool(Constants.Anim_Grounded, (this.rb.velocity.y < this.fallThreshold) ? false : true);
        }
    }
}
