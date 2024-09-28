using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public GhostChase Chase;
    private bool isActivated = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GameManager.Instance.SaveCheckpoint(transform.position);
            

            if(Chase != null)
            {
                Chase.IncreaseSpeed();
            }
            Debug.Log("Checkpoint atteint ! Vitesse de la lave augment�e.");
            isActivated = true;
            GetComponent<Collider2D>().enabled = false;
        }
    }
}
