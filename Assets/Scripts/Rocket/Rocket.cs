using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D _rb;
    public bool _isActivated = false;

    public CameraShake CamShake;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        MoveInDirection();
    }

    void MoveInDirection()
    {
        if (_isActivated == true)
        {
            Vector2 direction = transform.up;
            _rb.velocity = direction * speed;
            Destroy(this.gameObject, 10);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();


            if (playerController != null)
            {
                StartCoroutine(CamShake.Shake(.15000f, .4000f));
                playerController.DieAndRespawn();
            }
            Destroy(this.gameObject);
        }
    }
}
