using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformOut : MonoBehaviour
{
    //private Rigidbody2D _rb2D;
    private BoxCollider2D _collider2D;
    public float _timerOut = 0f;
    public bool _canFall = false;

    public float _timeBeforeFall;

    // Start is called before the first frame update
    void Start()
    {
        //_rb2D = this.GetComponent<Rigidbody2D>();
        _collider2D = this.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleFall();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            _canFall = true;
        }
    }

    private void HandleFall()
    {
        if (_canFall)
        {
            _timerOut += Time.deltaTime;

            if (_timerOut >= _timeBeforeFall)
            {
                this.gameObject.AddComponent<Rigidbody2D>();
                Rigidbody2D _rb2D = GetComponent<Rigidbody2D>();
                _rb2D.constraints = RigidbodyConstraints2D.FreezePositionX;
                _collider2D.enabled = false;
                Destroy(this.gameObject, 5) ;
            }
        }
    }
}
