using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

[RequireComponent(typeof(PlayerDie))]
public class TemperatureGauge : MonoBehaviour
{
    [HideInInspector]
    public bool NearFire = false;

    [HideInInspector]
    public bool HasDied = false;

    [SerializeField]
    private float temperatureMax = 100f;

    [SerializeField]
    private float temperatureDecreaseSpeedSeconds = 1f;

    [SerializeField]
    private float temperatureIncreaseSpeedSeconds = 0.1f;

    [SerializeField]
    private Image temperatureFillImage;

    [SerializeField]
    private float lowTemperatureThreshold = 20f;

    [SerializeField]
    private Animator postProcessAnimator;

    private float temperature;
    private float coldTimer = 0f;
    private PlayerDie playerDie;
    private bool lowTemp = false;
    private float warmTimer = 0f;

    private void Start()
    {
        this.temperature = this.temperatureMax;
        this.playerDie = GetComponent<PlayerDie>();
    }

    private void Update()
    {
        if (!this.NearFire)
        {
            this.coldTimer += Time.deltaTime;

            if (this.coldTimer > this.temperatureDecreaseSpeedSeconds)
            {
                this.temperature--;
                this.coldTimer = 0f;
            }
        }
        else if (this.temperature < this.temperatureMax)
        {
            this.warmTimer += Time.deltaTime;

            if (this.warmTimer > this.temperatureIncreaseSpeedSeconds)
            {
                this.temperature++;
                this.warmTimer = 0f;
            }
        }

        this.temperatureFillImage.fillAmount = this.temperature / this.temperatureMax;

        if ((this.temperature <= 0f) && (!this.HasDied))
        {
            StartCoroutine(this.playerDie.Die());
            this.HasDied = true;
        }

        if (!this.lowTemp)
        {
            if (this.temperature <= this.lowTemperatureThreshold)
            {
                this.lowTemp = true;
                this.postProcessAnimator.SetBool(Constants.Anim_TemperatureLow, true);
            }
        }
        else
        {
            if (this.temperature > this.lowTemperatureThreshold)
            {
                this.lowTemp = false;
                this.postProcessAnimator.SetBool(Constants.Anim_TemperatureLow, false);
            }
        }
    }

    public void ResetTemperature()
    {
        this.temperature = this.temperatureMax;
    }
}
