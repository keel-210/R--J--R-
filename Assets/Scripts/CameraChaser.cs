using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChaser : MonoBehaviour
{
    [SerializeField]
    Transform target;
    [SerializeField]
    Vector3 offset;
    void LateUpdate()
    {
        transform.position = target.position + target.right * offset.x + target.up * offset.y;
        transform.rotation = Quaternion.Slerp(transform.rotation,
        Quaternion.LookRotation(target.position - transform.position + target.transform.up), 0.05f);
    }
}
