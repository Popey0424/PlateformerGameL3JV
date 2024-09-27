using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TurnOnLight : MonoBehaviour
{

    [SerializeField] private Light2D globalLight;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Electricity ON");
            globalLight.intensity = 1;
        }
    }
}
