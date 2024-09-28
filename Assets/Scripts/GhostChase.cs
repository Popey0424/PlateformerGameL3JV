using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostChase : MonoBehaviour
{
    [Header("Chase Settigns")]
    [SerializeField] private float ghostSpeed = 1f;
    [SerializeField] private float speedIncrease = 0.5f;

    [SerializeField] private bool isRising = true;

    private void Update()
    {
        if (isRising)
        {
            transform.Translate(Vector2.up * ghostSpeed * Time.deltaTime);
        }
    }

    public void IncreaseSpeed()
    {
        ghostSpeed += speedIncrease;
        Debug.Log("Ca va plus vite!!!" + ghostSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            Debug.Log("Toucher par la hordre");
        }
    }
}
