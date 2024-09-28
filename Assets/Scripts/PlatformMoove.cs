using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

public class PlatformMoove : MonoBehaviour
{
    [Header("Init Settings")]
    public int startPoint;

    [Header("Movement Settings")]
    public float mooveSpeed;
    public Transform[] points;
    public bool isCicle;

    private bool goBack = false;
    private int i;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = points[startPoint].position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            if (isCicle == true)
            {
                i++;
                if (i == points.Length)
                {
                    i = 0;
                }
            }
            else
            {
                if (goBack == false)
                {
                    i++;
                    if (i == points.Length - 1)
                    {
                        goBack = true;
                    }
                }
                else
                {
                    i--;
                    if (i == startPoint)
                    {
                        goBack = false;
                    }
                }
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, points[i].position, mooveSpeed * Time.deltaTime);   
    }
}
