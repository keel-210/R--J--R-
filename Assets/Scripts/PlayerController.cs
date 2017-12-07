using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    Vector3 velo;
    [SerializeField]
    float JumpPower;
    [SerializeField]
    LayerMask mask;

    bool OnGround;
    int IsTouching;
    Rigidbody rb;
    Vector3 Normal, MeshDirection;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Debug.DrawRay(transform.position, transform.right * 2);
        if (Input.GetAxisRaw("Jump") > 0 && OnGround)
        {
            rb.AddForce(Vector3.up * JumpPower);
            WallRunRelease();
        }
    }
    private void FixedUpdate()
    {
        if (rb.useGravity)
        {
            rb.velocity = transform.right * velo.x
                + new Vector3(0, rb.velocity.y, 0)
                - transform.forward * velo.z * Input.GetAxis("Horizontal");
        }
        else
        {
            WallRunning();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        OnGround = true;
        rb.useGravity = false;
        rb.velocity = new Vector3(0, 0, 0);
        IsTouching++;

        Mesurement(collision);
        Debug.Log("run" + Normal);
        WallRunning();
    }
    private void OnCollisionStay(Collision collision)
    {
        OnGround = true;

        Mesurement(collision);

        WallRunning();
        Debug.DrawLine(collision.contacts[0].point, transform.position, Color.green);
        Debug.DrawRay(transform.position, Normal, Color.yellow);
    }
    private void OnCollisionExit(Collision collision)
    {
        IsTouching--;
        if (OnGround && IsTouching <= 0)
        {
            WallRunRelease();
        }
    }
    private Vector3 MesureNormal(Transform tra, Collision collision)
    {
        Vector3 normal = Vector3.up;
        Vector3 centerPos = Vector3.zero, UpPos = Vector3.zero, BackPos = Vector3.zero;
        Vector3 contact = collision.contacts[collision.contacts.Length - 1].point;
        RaycastHit hit;
        if (Physics.Raycast(tra.position, contact - transform.position, out hit, 5f, mask))
        {
            centerPos = hit.point;
            if (Physics.Raycast(tra.position - tra.right * 0.5f, contact - tra.position, out hit, 5f, mask))
            {
                BackPos = hit.point;
                if (Physics.Raycast(tra.position + tra.up * 0.5f, contact - tra.position, out hit, 5f, mask))
                {
                    UpPos = hit.point;

                    Vector3 dir1 = BackPos - centerPos;
                    Vector3 dir2 = UpPos - centerPos;
                    normal = Vector3.Cross(dir2, dir1).normalized;
                }
            }
        }
        if ((normal == Vector3.zero) || (normal == Vector3.down))
        {
            normal = Vector3.up;
        }
        return normal;
    }
    private Vector3 MesureDirection(Transform tra, Collision collision)
    {
        Vector3 dir = tra.right;
        Vector3 centerPos = Vector3.zero, BackPos = Vector3.zero, ForwardPos = Vector3.zero;
        Vector3 contact = collision.contacts[collision.contacts.Length - 1].point;
        RaycastHit hit;
        if (Physics.Raycast(tra.position, contact - tra.position, out hit, 5f, mask))
        {
            centerPos = hit.point;

            if (Physics.Raycast(tra.position - tra.right * 0.5f, contact - tra.position, out hit, 5f, mask))
            {
                BackPos = hit.point;

                if (Physics.Raycast(tra.position + tra.right * 0.5f, contact - tra.position, out hit, 5f, mask))
                {
                    ForwardPos = hit.point;
                    Vector3 dir1 = centerPos - BackPos;
                    Vector3 dir2 = ForwardPos - centerPos;
                    dir = ((dir1 + dir2) * 0.5f).normalized;
                    dir = new Vector3(dir.x, 0, dir.z).normalized;

                    Debug.DrawRay(tra.position, contact - tra.position, Color.green);
                    Debug.DrawRay(tra.position - new Vector3(0.5f, 0, 0), contact - tra.position, Color.green);
                    Debug.DrawRay(tra.position + new Vector3(0.5f, 0, 0), contact - tra.position, Color.green);

                    Debug.DrawRay(transform.position, dir * 5, Color.red);
                }
            }
        }
        return dir;
    }
    void Mesurement(Collision collision)
    {
        Normal = MesureNormal(transform, collision);
        MeshDirection = MesureDirection(transform, collision);
    }
    void WallRunning()
    {
        transform.right = MeshDirection;
        rb.velocity = transform.right * velo.x;
        rb.AddForce(-Normal * 100, ForceMode.Acceleration);
    }
    void WallRunRelease()
    {
        OnGround = false;
        rb.useGravity = true;
        Normal = Vector3.zero;
        IsTouching = 0;
        Debug.Log("Release");
    }
}
