using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CameraFollow : MonoBehaviour
{
    /// <summary>
    /// The target to follow.
    /// </summary>
    [SerializeField]
    private Transform target;

    /// <summary>
    /// How quickly to snap to the target's position.
    /// </summary>
    [SerializeField]
    private float smoothSpeed = 10f;

    /// <summary>
    /// Where to place the camera relative to the target.
    /// </summary>
    [SerializeField]
    private Vector3 offset;

    private Vector3 velocity = Vector3.zero;
    private Rigidbody rb;

    private void Start()
    {
        this.rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 desiredPosition = this.target.position + this.offset;
        Vector3 smoothedPosition = Vector3.SmoothDamp(this.rb.position, desiredPosition, ref this.velocity, this.smoothSpeed * Time.fixedDeltaTime);
        this.rb.position = smoothedPosition;
    }
}
