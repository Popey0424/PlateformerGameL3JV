using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeSprite : MonoBehaviour
{
    public CameraShake CamShake;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Toucher");
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();

            
            if (playerController != null)
            {

                StartCoroutine(CamShake.Shake(.15000f, .4000f));
                playerController.DieAndRespawn(); 
            }
        }
    }
}
