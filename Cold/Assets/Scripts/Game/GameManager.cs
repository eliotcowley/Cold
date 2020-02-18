using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [HideInInspector]
    public Checkpoint LastCheckpoint;

    [HideInInspector]
    public Vector3 StartingPosition;

    [HideInInspector]
    public Quaternion StartingRotation;

    [SerializeField]
    private Animator blackScreenAnimator;

    private Transform playerTransform;

    private void Start()
    {
        if (Instance != null)
        {
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this);

        this.playerTransform = GameObject.FindGameObjectWithTag(Constants.Tag_Player).transform;
        this.StartingPosition = this.playerTransform.position;
        this.StartingRotation = this.playerTransform.rotation;
    }

    public void FadeInOut()
    {
        this.blackScreenAnimator.SetTrigger(Constants.Anim_ImageFade);
    }
}
