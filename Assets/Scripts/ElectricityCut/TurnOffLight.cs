using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TurnOffLight : MonoBehaviour
{
    [SerializeField] private Light2D globalLight;
    //[SerializeField] private GameObject player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Electricity OFF");
            globalLight.intensity = 0;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Electricity ON");
            globalLight.intensity = 1;
        }
    }
}


