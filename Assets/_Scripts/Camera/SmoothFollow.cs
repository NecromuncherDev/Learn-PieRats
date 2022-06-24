using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    [SerializeField] internal Transform target;
    [SerializeField, Range(0f,5f)] private float dampening;
    
    private Vector2 targetPos, transformPos, newPos;
    private Vector2 velocity = Vector2.zero;

    private void LateUpdate()
    {
        if (target)
        {
            targetPos = target.position;
            transformPos = transform.position;

            newPos = Vector2.SmoothDamp(transformPos, targetPos, ref velocity, dampening);
            transform.Translate(newPos - transformPos);
        }
    }
}
