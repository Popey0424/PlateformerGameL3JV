using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float followSpeed = 1.0f;
    [SerializeField] private float xOffset = 1f;
    [SerializeField] private float yOffset = 1f;
    public Transform target;
    public Transform target2;

    private void Update()
    {
        Vector3 newPos = new Vector3(target.position.x + xOffset, target.position.y + yOffset, -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, followSpeed * Time.deltaTime);

        Vector3 newPos2 = new Vector3(target2.position.x + xOffset, target2.position.y + yOffset, -10f);
        transform.position = Vector3.Slerp(transform.position, newPos2, followSpeed * Time.deltaTime);
    }
}
