using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [HideInInspector]
    public Vector3 CheckpointLocation;

    [HideInInspector]
    public Quaternion CheckpointRotation;

    [SerializeField]
    private Animator checkpointTextAnimator;

    [SerializeField]
    private Transform checkpointLocationTransform;

    private void Start()
    {
        this.CheckpointLocation = this.checkpointLocationTransform.position;
        this.CheckpointRotation = this.checkpointLocationTransform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Constants.Tag_Player))
        {
            if (GameManager.Instance.LastCheckpoint != this)
            {
                GameManager.Instance.LastCheckpoint = this;
                this.checkpointTextAnimator.SetTrigger(Constants.Anim_TextFade);
            }
        }
    }
}
