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
    [SerializeField]
    Animator ani;
    [SerializeField]
    IKControl iK;
    [SerializeField]
    Transform ArmIKPos;

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
        ani.SetBool("OnGround", OnGround);
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

        Mesure(collision);
        Debug.Log("run" + Normal);
        WallRunning();
    }
    private void OnCollisionStay(Collision collision)
    {
        OnGround = true;
        Mesure(collision);

        WallRunning();

        ArmIKPos.position = Mesurement.RayContactPoint(transform.position + new Vector3(0, 0.65f, 0), -Normal, mask);

        Debug.DrawLine(collision.contacts[0].point, transform.position, Color.green);
        Debug.DrawRay(transform.position, Normal, Color.yellow);
        Debug.DrawRay(transform.position + new Vector3(0, 0.65f, 0), -Normal, Color.magenta);
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
        MeshDirection = Mesurement.MesureDirection(transform, collision, mask);
    }
    void WallRunning()
    {
        transform.right = MeshDirection;
        rb.velocity = transform.right * velo.x;
        rb.AddForce(-Normal * 100, ForceMode.Acceleration);
        if (Normal != Vector3.up)
        {
            iK.ikActive = true;
        }
    }
    void WallRunRelease()
    {
        OnGround = false;
        rb.useGravity = true;
        iK.ikActive = false;
        Normal = Vector3.zero;
        IsTouching = 0;
        Debug.Log("Release");
    }
}
