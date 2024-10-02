using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public GameObject StartObject;
    public GameObject EndObject;
    private Vector2 InitialScale;
    public bool isLaserOn;
    public float timeSwitchOn;
    public float timeSwitchOff;
    private float timer = 0;
    private BoxCollider2D _bc2D;


    [Header("CameraShake")]
    public CameraShake CamShake;

  
    void Start()
    {
        _bc2D = GetComponent<BoxCollider2D>();
        InitialScale = transform.localScale;
        UpdateTransformForScale();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeSwitchOn && isLaserOn == false)
        {
            isLaserOn = true;
            timer = 0;
        }
        if (timer >= timeSwitchOff && isLaserOn == true)
        {
            isLaserOn = false;
            timer = 0;
        }

        if (isLaserOn)
        {
            _bc2D.enabled = true;
            UpdateTransformForScale();
        }
        else
        {
            transform.localScale = new Vector3(0f, transform.localScale.y, 1);
            _bc2D.enabled = false;
        }
    }


    void UpdateTransformForScale()
    {
        float distance = Vector3.Distance(StartObject.transform.position, EndObject.transform.position);
        transform.localScale = new Vector2(InitialScale.x, distance);

        Vector2 middlePoint = (StartObject.transform.position + EndObject.transform.position) / 2f;
        transform.position = middlePoint;
        
        Vector2 rotationDirection = (EndObject.transform.position - StartObject.transform.position);
        transform.up = rotationDirection;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();


            if (playerController != null)
            {
                StartCoroutine(CamShake.Shake(.15000f, 4000f));
                playerController.DieAndRespawn();
            }
        }
    }
}
