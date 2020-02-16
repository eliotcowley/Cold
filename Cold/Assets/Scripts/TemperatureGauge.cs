using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TemperatureGauge : MonoBehaviour
{
    [SerializeField]
    private float temperatureMax = 100f;

    [SerializeField]
    private float temperatureDecreaseSpeedSeconds = 1f;

    [SerializeField]
    private Image temperatureFillImage;

    private float temperature;
    private float timer = 0f;

    private void Start()
    {
        this.temperature = this.temperatureMax;
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
    }
}
