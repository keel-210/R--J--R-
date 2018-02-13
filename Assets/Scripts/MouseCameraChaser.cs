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
    [SerializeField]
    float MinimamRotLimit;
    Quaternion targetLerpRot = new Quaternion(0, 0, 0, 1);
    void LateUpdate()
    {
        Vector3 targetpos = transform.forward * offset.x + Vector3.up * offset.y + transform.right * offset.z;
        targetposCash = Vector3.Lerp(targetposCash, targetpos, 0.1f);
        transform.position = target.position + targetposCash;
        if (wallRunner.Normal == Vector3.up)
        {
            targetLerpRot = Quaternion.Lerp(target.localRotation, transform.localRotation, 0.05f);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetLerpRot * dir.GetYDir() * Quaternion.Euler(0, 90, 0), 0.1f);
        }
        else
        {
            if ((target.localRotation.eulerAngles - transform.localRotation.eulerAngles).y > MinimamRotLimit)
            {
                targetLerpRot = Quaternion.Lerp(target.localRotation, transform.localRotation, 0.05f);
            }
            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetLerpRot * dir.GetDir() * Quaternion.Euler(0, 90, 0), 0.1f);
        }
    }
}
