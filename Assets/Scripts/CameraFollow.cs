using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float smoothspeed = 0.125f;

    private void FixedUpdate()
    {
        Vector2 desiredPosition = (Vector2)target.position;
        Vector2 smoothedPosition = Vector3.Lerp((Vector2)transform.position, desiredPosition, smoothspeed);

        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, -10);
    }
}
