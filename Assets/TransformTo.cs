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


    [Header("AudioSources")]
    [SerializeField] private AudioSource transformToSound;


    [Header("FX Prefab")]
    [SerializeField] private GameObject fxPrefab;

    [Header("Camera")]
    [SerializeField] private  CameraFollow camFollow;


    private void Start()
    {
        camFollow = Camera.main.GetComponent<CameraFollow>();
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && isInTrigger == true)
        {
            SwitchTo();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameObject press = Instantiate(pressZ, new Vector2(this.gameObject.transform.position.x + 2, this.gameObject.transform.position.y + 1), quaternion.identity);
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
        transformToSound.Play();

       
        basePlayer.SetActive(false);
        Instantiate(fxPrefab, transform.position, transform.rotation);

        

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
