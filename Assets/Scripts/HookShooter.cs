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
    GameObject HookShot;
    public bool IsHooked, IsShotHook;
    Vector3 HookedPoint, PullingVelo;
    LineRenderer line;
    MouseDirection dir;
    void Start()
    {
        dir = FindObjectOfType<MouseDirection>();
        line = GetComponent<LineRenderer>();
        line.enabled = false;
    }
    void Update()
    {
        if (Input.GetAxis("Fire2") > 0)
        {
            if (rigidbody.useGravity)
            {
                if (!IsShotHook)
                {
                    Hook();
                }
                if (IsHooked)
                {
                    Pull();
                }
            }
            else
            {
                HookRelease();
            }
        }
        else if (IsHooked)
        {
            HookRelease();
        }
    }
    void Hook()
    {
        IsShotHook = true;
        GameObject obj = Instantiate(HookShot, transform.position, Quaternion.identity);
        obj.transform.right = Camera.main.ScreenPointToRay(Input.mousePosition).direction;
        StartCoroutine(this.DelayMethod(3f, () =>
        {
            if (!IsHooked)
                IsShotHook = false;
        }));
    }
    public void HookHit(Vector3 HitPoint)
    {
        IsHooked = true;
        HookedPoint = HitPoint;
        line.SetPosition(0, transform.position);
        line.SetPosition(1, HookedPoint);
        line.enabled = true;
    }
    void Pull()
    {
        if (HookedPoint != Vector3.zero)
        {
            PullingVelo = (HookedPoint - transform.position).normalized;
            rigidbody.AddForce(PullingVelo * PullingSpeed);
            line.SetPosition(0, transform.position);
            Debug.DrawLine(transform.position, HookedPoint, Color.blue);
        }
    }
    void HookRelease()
    {
        IsHooked = false;
        IsShotHook = false;
        line.enabled = false;
        HookedPoint = Vector3.zero;
    }
}
