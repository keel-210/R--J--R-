using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDirection : MonoBehaviour
{
    [SerializeField, Range(0.1f, 100)]
    float sensitivityX, sensitivityY;
    float rotAverageX = 0F;
    float rotationX = 0F;
    float rotationY = 0F;

    public float minimumX = -360F;
    public float maximumX = 360F;
    public float minimumY = -60F;
    public float maximumY = 60F;
    float rotAverageY = 0F;
    Quaternion yQuaternion, xQuaternion;
    public float frameCounter = 20;
    public Quaternion GetDir()
    {
        rotAverageY = 0f;
        rotAverageX = 0f;

        rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
        rotationX += Input.GetAxis("Mouse X") * sensitivityX;

        rotAverageY = rotationY;
        rotAverageX = rotationX;

        yQuaternion = Quaternion.AngleAxis(rotAverageY, Vector3.forward);
        xQuaternion = Quaternion.AngleAxis(rotAverageX, Vector3.up);
        return xQuaternion * yQuaternion;
    }
    public Quaternion GetXDir()
    {
        rotAverageX = 0f;

        rotationX += Input.GetAxis("Mouse X") * sensitivityX;

        rotAverageX = rotationX;

        xQuaternion = Quaternion.AngleAxis(rotAverageX, Vector3.up);
        return xQuaternion;
    }
    public Quaternion GetYDir()
    {
        rotAverageY = 0f;

        rotationY += Input.GetAxis("Mouse Y") * sensitivityY;

        rotAverageY = rotationY;

        yQuaternion = Quaternion.AngleAxis(rotAverageY, Vector3.forward);
        return yQuaternion;
    }
}