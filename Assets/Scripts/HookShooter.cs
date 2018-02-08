using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookShooter : MonoBehaviour
{
    [SerializeField]
    Rigidbody rigidbody;
    float PullingSpeed;
    bool IsHooked;
    Vector3 HookedPoint, PullingVelo;

    void Update()
    {
        if (Input.GetAxis("Fire2") > 0)
        {
            if (!IsHooked)
            {
                Hook();
            }
            else
            {
                Pull();
            }
        }
        else if (IsHooked)
        {
            HookRelease();
        }
    }
    void Hook()
    {
        IsHooked = true;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit, 30f))
        {
            HookedPoint = hit.point;
            PullingVelo = (HookedPoint - transform.position).normalized;
        }
    }
    void Pull()
    {
        rigidbody.AddForce(PullingVelo * PullingSpeed);
    }
    void HookRelease()
    {
        IsHooked = false;
    }
}
