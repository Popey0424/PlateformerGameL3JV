using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Unity.Mathematics;


public class TransformTo : MonoBehaviour
{
    [SerializeField] private GameObject basePlayer;

    [SerializeField] private Transform t_BasePlayer;

    [SerializeField] private GameObject transformTo;
    [SerializeField] private GameObject pressZ;

    private GameObject pressToDestroy;

    private bool isInTrigger = false;
    [SerializeField] private  CameraFollow camFollow;


    private void Start()
    {
        camFollow = Camera.main.GetComponent<CameraFollow>();
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.W) && isInTrigger == true)
        {
            SwitchTo();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameObject press = Instantiate(pressZ, new Vector2(collision.gameObject.transform.position.x, this.gameObject.transform.position.y + 1), quaternion.identity);
            pressToDestroy = press;
            isInTrigger = true;
            //SwitchTo();
        }
    }

    private void SwitchTo()
    {

        Debug.Log("Switching character...");

        transformTo.SetActive(true);
        transformTo.transform.position = transform.position;


       
        basePlayer.SetActive(false);

        camFollow.SetTarget(transformTo.transform);

        Debug.Log("Camera should now follow: " + transformTo.name);
        Destroy(gameObject);




    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isInTrigger = false;
        Destroy(pressToDestroy);
    }





}
