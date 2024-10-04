using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;

using UnityEngine;
using UnityEngine.UIElements;

public class ObjectSelection : MonoBehaviour
{
    public GameObject pressZ;

    //public GameObject playerGhost;
    //public GameObject playerLamp;

    public Transform player1; 
    public Transform player2;
    public CameraFollow cameraFollow;

    private Transform currentPlayer;

    private void Start()
    {
        currentPlayer = player1;
        cameraFollow.SetTarget(currentPlayer);
    }


    private bool _CanTransformLamp = false;
    private bool _CanTransformGhost = false;

    private GameObject pressToDestroy;

    // Start is called before the first frame update
    //void Awake()
    //{
    //    this.gameObject.transform.position = playerGhost.gameObject.transform.position;
    //}

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Lamp") && collision.CompareTag("Player"))
        {
            GameObject press = Instantiate(pressZ, new Vector2(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y+1), quaternion.identity);
            _CanTransformLamp = true;
            pressToDestroy = press;
        }
        else if (collision.CompareTag("Ghost") && collision.CompareTag("Player"))
        {
            GameObject press2 = Instantiate(pressZ, new Vector2(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y+1), quaternion.identity);
            _CanTransformGhost = true;
            pressToDestroy = press2;
        }
        //else if (collision.CompareTag("Object3"))
        //{

        //}
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_CanTransformLamp == true)
        {
            if (Input.GetKey(KeyCode.W))
            {

                SwitchPlayer();
                //playerLamp.SetActive(true);
                //playerLamp.gameObject.transform.position = collision.gameObject.transform.position;
                //this.gameObject.SetActive(false);
                //_CanTransformLamp = false;
                //Destroy(pressToDestroy);
                //Destroy(collision.gameObject);
            }
        }
        if (_CanTransformGhost == true)
        {
            if (Input.GetKey(KeyCode.W))
            {
                SwitchPlayer();
                //playerGhost.SetActive(true);
                //playerGhost.gameObject.transform.position = collision.gameObject.transform.position;
                //this.gameObject.SetActive(false);
                //_CanTransformGhost = false;
                //Destroy(pressToDestroy);
                //Destroy(collision.gameObject);
            }
        }
    }


    private void SwitchPlayer()
    {
        if(currentPlayer == player1)
        {
            currentPlayer = player2;
        }
        else
        {
            currentPlayer = player1;
        }
        cameraFollow.SetTarget(currentPlayer);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _CanTransformLamp = false;
        _CanTransformGhost = false;
        Destroy(pressToDestroy);
    }
}
