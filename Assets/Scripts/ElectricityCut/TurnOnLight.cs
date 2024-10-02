using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TurnOnLight : MonoBehaviour
{

    [SerializeField] private Light2D globalLight;

    [Header("Sounds")]
    [SerializeField] private AudioSource lightOn;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            lightOn.Play();
            Debug.Log("Electricity ON");
            globalLight.intensity = 1;
        }
    }
}
