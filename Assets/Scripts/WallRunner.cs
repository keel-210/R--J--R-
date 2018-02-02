using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WallRunner : MonoBehaviour
{
    Vector3 velo;
    float JumpPower;
    LayerMask mask;
    public bool OnGround;
    int IsTouching;
    Rigidbody rb;
    Vector3 Normal, MeshDirection;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        PlayerParamater PP = GetComponent<PlayerParamater>();
        velo = PP.WallVelo;
        JumpPower = PP.JumpPower;
        mask = PP.mask;
    }
    void Update()
    {
        if (Input.GetAxisRaw("Jump") > 0 && OnGround)
        {
            rb.AddForce(Vector3.up * JumpPower);
            WallRunRelease();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        OnGround = true;
        rb.useGravity = false;
        IsTouching++;

        Mesure(collision);
        WallRunning(collision);
    }
    private void OnCollisionStay(Collision collision)
    {
        OnGround = true;
        Mesure(collision);
        WallRunning(collision);
    }
    private void OnCollisionExit(Collision collision)
    {
        IsTouching--;
        if (OnGround && IsTouching <= 0)
        {
            WallRunRelease();
        }
    }

    void Mesure(Collision collision)
    {
        Normal = Mesurement.MesureNormal(transform, collision, mask);
        MeshDirection = Mesurement.MesureDirection(transform, collision, mask, -Normal);
    }
    void WallRunning(Collision collision)
    {
        transform.right = MeshDirection;
        rb.velocity = transform.right * velo.x * Input.GetAxis("Vertical");
        rb.AddForce(-Normal * 100, ForceMode.Acceleration);
        if (Normal != Vector3.up)
        {
            Vector3 upDir = Vector3.Cross(transform.right, Normal).normalized;
        }
        else
        {
            transform.right = collision.transform.right;
        }
    }
    void WallRunRelease()
    {
        rb.velocity = Vector3.zero;
        OnGround = false;
        rb.useGravity = true;
        Normal = Vector3.zero;
        IsTouching = 0;
    }
}
