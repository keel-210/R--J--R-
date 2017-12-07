using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Transform target;
    [SerializeField]
    Vector3 offset;
    void Start()
    {

    }

    void LateUpdate()
    {
        transform.position = target.position + target.right * offset.x + target.up * offset.y;
    }
}
