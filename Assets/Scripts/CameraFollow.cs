using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float followSpeed = 1.0f;
    [SerializeField] private float xOffset = 1f;
    [SerializeField] private float yOffset = 1f;
    public Transform target;





    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        Debug.Log("New camera target: " + target.name);
    }

    private void Update()
    {


        if (target != null)
        {
            Vector3 newPos = new Vector3 (target.position.x + xOffset, target.position.y + yOffset, -10f);
            transform.position = Vector3.Slerp(transform.position, newPos, followSpeed * Time.deltaTime);
        }

    }
}
