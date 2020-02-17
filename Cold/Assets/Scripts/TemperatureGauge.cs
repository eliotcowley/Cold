using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

[RequireComponent(typeof(PlayerDie))]
public class TemperatureGauge : MonoBehaviour
{
    [SerializeField]
    private float temperatureMax = 100f;

    [SerializeField]
    private float temperatureDecreaseSpeedSeconds = 1f;

    [SerializeField]
    private Image temperatureFillImage;

    [SerializeField]
    private float lowTemperatureThreshold = 20f;

    [SerializeField]
    private Animator postProcessAnimator;

    private float temperature;
    private float timer = 0f;
    private PlayerDie playerDie;
    private bool lowTemp = false;

    private void Start()
    {
        this.temperature = this.temperatureMax;
        this.playerDie = GetComponent<PlayerDie>();
    }

    private void Update()
    {
        this.timer += Time.deltaTime;

        if (this.timer > this.temperatureDecreaseSpeedSeconds)
        {
            this.temperature--;
            this.timer = 0f;
        }

        this.temperatureFillImage.fillAmount = this.temperature / this.temperatureMax;

        if (this.temperature <= 0f)
        {
            this.playerDie.Die();
        }

        if (!this.lowTemp)
        {
            if (this.temperature <= this.lowTemperatureThreshold)
            {
                this.lowTemp = true;
                this.postProcessAnimator.SetTrigger(Constants.Anim_VignetteFadeIn);
            }
        }
    }
}
