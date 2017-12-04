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
        }
    }
    private void FixedUpdate()
    {
        if (rb.useGravity)
        {
            rb.velocity = transform.right * velo.x + new Vector3(0, rb.velocity.y, 0);
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

        WallRunning();
    }
    private void OnCollisionStay(Collision collision)
    {
        OnGround = true;

        Mesurement(collision);

        Debug.DrawLine(collision.contacts[0].point, transform.position, Color.green);
        Debug.DrawRay(transform.position, Normal, Color.yellow);
    }
    private void OnCollisionExit(Collision collision)
    {
        IsTouching--;
        if (OnGround && IsTouching <= 0)
        {
            OnGround = false;
            rb.useGravity = true;
            Normal = Vector3.zero;
            IsTouching = 0;
        }
    }
    private Vector3 MesureNormal(Transform tra,Collision collision)
    {
        Vector3 normal = Vector3.zero;
        Vector3 centerPos = Vector3.zero, UpPos = Vector3.zero, BackPos = Vector3.zero;
        RaycastHit hit;
        if (Physics.Raycast(tra.position, collision.contacts[0].point - transform.position, out hit))
        {
            centerPos = hit.point;
        }
        if (Physics.Raycast(tra.position - new Vector3(0.5f, 0, 0), collision.contacts[0].point - tra.position, out hit))
        {
            BackPos = hit.point;
        }
        if (Physics.Raycast(tra.position + new Vector3(0, 0.5f, 0), collision.contacts[0].point - tra.position, out hit))
        {
            UpPos = hit.point;
        }
        Vector3 dir1 = BackPos - centerPos;
        Vector3 dir2 = UpPos - centerPos;
        normal = Vector3.Cross(dir1, dir2).normalized;
        return normal;
    }
    private Vector3 MesureDirection(Transform tra, Collision collision)
    {
        Vector3 dir = Vector3.zero;
        Vector3 centerPos = Vector3.zero, BackPos = Vector3.zero, ForwardPos = Vector3.zero;
        RaycastHit hit;
        if (Physics.Raycast(tra.position, collision.contacts[0].point - tra.position, out hit))
        {
            centerPos = hit.point;
        }
        if (Physics.Raycast(tra.position - new Vector3(0.5f, 0, 0), collision.contacts[0].point - tra.position, out hit))
        {
            BackPos = hit.point;
        }

        if (Physics.Raycast(tra.position - new Vector3(-0.5f, 0, 0), collision.contacts[0].point - tra.position, out hit))
        {
            ForwardPos = hit.point;
        }
        Vector3 dir1 = centerPos - BackPos;
        Vector3 dir2 = ForwardPos - centerPos;
        dir = Vector3.Lerp(dir1, dir2, 0.5f);
        Debug.Log("Dir : " + Quaternion.FromToRotation(tra.rotation.eulerAngles, dir).eulerAngles);
        return dir;
    }
    void Mesurement(Collision collision)
    {
        Normal = MesureNormal(transform, collision);
        MeshDirection = collision.transform.rotation.eulerAngles;
        //MeshDirection = MesureDirection(transform,collision);
    }
    void WallRunning()
    {
        rb.velocity = transform.right * velo.x;
        rb.AddForce(Normal * 9.8f, ForceMode.Acceleration);
        rb.rotation = Quaternion.Euler(0, MeshDirection.y, 0);
    }
}
