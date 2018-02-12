using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookShooter : MonoBehaviour
{
    [SerializeField]
    Rigidbody rigidbody;
    [SerializeField]
    float PullingSpeed;
    [SerializeField]
    LayerMask mask;
    public bool IsHooked;
    Vector3 HookedPoint, PullingVelo;

    void Update()
    {
        if (Input.GetAxis("Fire2") > 0)
        {
            if (rigidbody.useGravity)
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
        if (Physics.Raycast(ray, out hit, 500f, mask))
        {
            HookedPoint = hit.point;
        }
        else
        {
            IsHooked = false;
        }
    }
    void Pull()
    {
        if (HookedPoint != Vector3.zero)
        {
            PullingVelo = (HookedPoint - transform.position).normalized;
            rigidbody.AddForce(PullingVelo * PullingSpeed);
            Debug.DrawLine(transform.position, HookedPoint, Color.blue);
        }
    }
    void HookRelease()
    {
        IsHooked = false;
        HookedPoint = Vector3.zero;
    }
}
