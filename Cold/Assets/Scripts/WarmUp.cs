using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarmUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Constants.Tag_Player))
        {

        }
    }
}
