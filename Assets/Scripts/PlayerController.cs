using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    Vector3 velo, AirVelo;
    [SerializeField]
    float JumpPower;
    [SerializeField]
    LayerMask mask;
    [SerializeField]
    Animator ani;
    [SerializeField]
    IKControl UC_IK;
    [SerializeField]
    Transform UC_Model;
    [SerializeField]
    GameObject LandingEffect, JumpEffect;
    [SerializeField]
    bool EnableDoubleJump, EnableAirDash, EnableFall;

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
        if (Input.GetAxisRaw("Vertical") < 0 && !OnGround && EnableFall)
        {

        }
        if (Input.GetAxisRaw("Vertical") > 0 && !OnGround && EnableDoubleJump)
        {

        }
        if (Input.GetAxisRaw("Vertical") > 0 && !OnGround && EnableAirDash)
        {

        }

        ani.SetBool("OnGround", OnGround);
    }
    private void FixedUpdate()
    {
        if (rb.useGravity)
        {
            rb.velocity = transform.right * AirVelo.x
                + new Vector3(0, rb.velocity.y, 0)
                - transform.forward * AirVelo.z * Input.GetAxis("Horizontal");
            UC_Model.transform.localPosition = Vector3.Lerp(UC_Model.transform.localPosition, new Vector3(0, -0.5f, 0), 0.05f);
            UC_Model.transform.localRotation = Quaternion.Slerp(UC_Model.transform.localRotation, Quaternion.Euler(0, 90, 0), 0.05f);
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
        if (LandingEffect)
            Instantiate(LandingEffect, transform.position, transform.rotation);
    }
    private void OnCollisionStay(Collision collision)
    {
        OnGround = true;
        Mesure(collision);
        Debug.Log(MeshDirection);
        WallRunning();

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
        MeshDirection = Mesurement.MesureDirection(transform, collision, mask, -Normal);
    }
    void WallRunning()
    {
        transform.right = MeshDirection;
        rb.velocity = transform.right * velo.x;
        rb.AddForce(-Normal * 100, ForceMode.Acceleration);
        if (Normal != Vector3.up)
        {
            UC_IK.ikActive = false;
            Vector3 upDir = Vector3.Cross(transform.right, Normal).normalized;
            if (Vector3.Cross(Normal, transform.right).y >= 0)
            {
                UC_IK.RightHandObj.position = Mesurement.RayContactPoint(transform.position + new Vector3(0, 0.65f, 0), -Normal, mask);
                UC_IK.WallOnRightside = true;
                UC_Model.transform.localPosition = new Vector3(0, -0.5f, -0.5f);
                UC_Model.transform.localRotation = Quaternion.Euler(0, 90, Mathf.Abs(Normal.z * 25));
                UC_IK.RightFootObj.position = Mesurement.RayContactPoint(transform.position + new Vector3(0, -0.15f, 0), -Normal, mask);
                UC_IK.LeftFootObj.position = Mesurement.RayContactPoint(transform.position + new Vector3(0, -0.15f, 0), -Normal, mask);
                Debug.DrawLine(UC_IK.RightFootObj.position, UC_IK.RightFootObj.position + Vector3.up, Color.blue);
            }
            else
            {
                UC_IK.LeftHandObj.position = Mesurement.RayContactPoint(transform.position + new Vector3(0, 0.65f, 0), -Normal, mask);
                UC_IK.WallOnRightside = false;
                UC_Model.transform.localPosition = new Vector3(0, -0.5f, 0.5f);
                UC_Model.transform.localRotation = Quaternion.Euler(0, 90, -Mathf.Abs(Normal.z * 25));
                UC_IK.RightFootObj.position = Mesurement.RayContactPoint(transform.position + new Vector3(0, -0.25f, 0), -Normal, mask);
                UC_IK.LeftFootObj.position = Mesurement.RayContactPoint(transform.position + new Vector3(0, -0.25f, 0), -Normal, mask);
            }
        }
        else
        {
            UC_Model.transform.localPosition = new Vector3(0, -0.5f, 0);
            UC_Model.transform.rotation = Quaternion.Euler(0, 90, 0);
        }
    }
    void WallRunRelease()
    {
        rb.velocity = Vector3.zero;
        OnGround = false;
        rb.useGravity = true;
        UC_IK.ikActive = false;
        Normal = Vector3.zero;
        IsTouching = 0;
        Debug.Log("Release");
        if (JumpEffect)
            Instantiate(JumpEffect, transform.position, transform.rotation);
    }
}
