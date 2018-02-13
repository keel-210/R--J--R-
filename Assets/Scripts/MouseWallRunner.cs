using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MouseWallRunner : MonoBehaviour
{
    Vector3 velo;
    float JumpPower;
    LayerMask mask;
    public bool OnGround;
    int IsTouching;
    Rigidbody rb;
    public Vector3 Normal, MeshDirection;
    [SerializeField]
    MouseDirection mouseDir;
    PlayerParamater PP;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        PP = GetComponent<PlayerParamater>();
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
        if (Normal == Vector3.up)
        {
            transform.right = collision.transform.right;
            transform.localRotation *= mouseDir.GetXDir();
            Vector3 LerpedRight = new Vector3(transform.right.x, 0, transform.right.z).normalized;
            Vector3 LerpedForward = new Vector3(transform.forward.x, 0, transform.forward.z);
            Vector3 ToVelo = LerpedRight * velo.x * Input.GetAxis("Vertical")
                                - LerpedForward * velo.z * Input.GetAxis("Horizontal");
            rb.velocity = Vector3.Lerp(rb.velocity, ToVelo, 0.25f);
            PP.IsRunningPlane = true;
        }
        else
        {
            transform.right = MeshDirection;
            Vector3 ToVelo = transform.right * velo.x * Input.GetAxis("Vertical");
            rb.velocity = Vector3.Lerp(rb.velocity, ToVelo, 0.25f);
            PP.IsRunningPlane = false;
        }
        rb.AddForce(-Normal * 100, ForceMode.Acceleration);
    }
    void WallRunRelease()
    {
        OnGround = false;
        rb.useGravity = true;
        IsTouching = 0;
        PP.IsRunningPlane = false;
    }
}
