using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCameraChaser : MonoBehaviour
{
    [SerializeField]
    Transform target;
    [SerializeField]
    Vector3 offset;
    [SerializeField]
    MouseDirection dir;
    Vector3 targetposCash;
    [SerializeField]
    MouseWallRunner wallRunner;
    void LateUpdate()
    {
        Vector3 targetpos = transform.forward * offset.x + Vector3.up * offset.y + transform.right * offset.z;
        targetposCash = Vector3.Lerp(targetposCash, targetpos, 0.1f);
        transform.position = target.position + targetposCash;
        Quaternion targetLerpRot = Quaternion.Lerp(target.localRotation, transform.localRotation, 0.1f);
        if (wallRunner.Normal == Vector3.up)
        {
            transform.localRotation = targetLerpRot * Quaternion.Euler(0, 90, 0);
        }
        else
        {
            transform.localRotation = targetLerpRot * dir.GetDir() * Quaternion.Euler(0, 90, 0);
        }
    }
}
