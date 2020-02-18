using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(TemperatureGauge))]
public class PlayerDie : MonoBehaviour
{
    [SerializeField]
    private float secondsTillFade;

    private Animator playerAnimator;
    private PlayerMovement playerMovement;
    private TemperatureGauge temperatureGauge;

    private void Start()
    {
        this.playerAnimator = GetComponent<Animator>();
        this.playerMovement = GetComponent<PlayerMovement>();
        this.temperatureGauge = GetComponent<TemperatureGauge>();
    }

    public IEnumerator Die()
    {
        this.playerAnimator.SetBool(Constants.Anim_PlayerDead, true);
        this.playerMovement.CanMove = false;

        yield return new WaitForSeconds(this.secondsTillFade);

        GameManager.Instance.FadeInOut();

        yield return new WaitForSeconds(Constants.SecondsTillFadeOut);

        if (GameManager.Instance.LastCheckpoint == null)
        {
            this.gameObject.transform.position = GameManager.Instance.StartingPosition;
            this.gameObject.transform.rotation = GameManager.Instance.StartingRotation;
        }
        else
        {
            this.gameObject.transform.position = GameManager.Instance.LastCheckpoint.CheckpointLocation;
            this.gameObject.transform.rotation = GameManager.Instance.LastCheckpoint.CheckpointRotation;
        }

        this.playerAnimator.SetBool(Constants.Anim_PlayerDead, false);
        this.playerMovement.CanMove = true;
        
        this.temperatureGauge.ResetTemperature();
        this.temperatureGauge.HasDied = false;
    }
}
