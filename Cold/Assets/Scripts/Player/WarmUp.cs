using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarmUp : MonoBehaviour
{
    [SerializeField]
    private TemperatureGauge temperatureGauge;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Constants.Tag_Player))
        {
            this.temperatureGauge.NearFire = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(Constants.Tag_Player))
        {
            this.temperatureGauge.NearFire = false;
        }
    }
}
