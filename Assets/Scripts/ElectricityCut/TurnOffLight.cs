using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TurnOffLight : MonoBehaviour
{
    [SerializeField] private Light2D globalLight;
    //[SerializeField] private GameObject player;

    [Header("Sounds")]
    [SerializeField] private AudioSource lightOFF;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            lightOFF.Play();
            Debug.Log("Electricity OFF");
            globalLight.intensity = 0;
        }
    }
}


