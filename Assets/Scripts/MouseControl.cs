using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MouseControl : MonoBehaviour
{
    Vector3 AirVelo, FallVelo;
    Animator ani;
    IKControl UC_IK;
    Transform UC_Model;
    GameObject LandingEffect, JumpEffect;
    [SerializeField]
    bool EnableDoubleJump, EnableAirDash, IsAirDashing;

    Rigidbody rb;
    WallRunner wallr;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        wallr = GetComponent<WallRunner>();
        PlayerParamater PP = GetComponent<PlayerParamater>();
        AirVelo = PP.AirVelo;
        FallVelo = PP.FallVelo;
        ani = PP.ani;
        UC_IK = PP.UC_IK;
        UC_Model = PP.UC_Model;
        UC_Model.transform.localPosition = Vector3.Lerp(UC_Model.transform.localPosition, new Vector3(0, -0.5f, 0), 0.05f);
        UC_Model.transform.localRotation = Quaternion.Slerp(UC_Model.transform.localRotation, Quaternion.Euler(0, 90, 0), 0.05f);
    }

    void Update()
    {

    }
    private void FixedUpdate()
    {
        if (rb.useGravity)
        {
            InAirByMouse();
        }
        if (Input.GetAxisRaw("Vertical") < 0 && !wallr.OnGround)
        {
            Fall();
        }
        if (Input.GetAxisRaw("Vertical") > 0 && !wallr.OnGround && EnableDoubleJump)
        {
            DoubleJump();
        }
        if (Input.GetAxisRaw("Vertical") > 0 && !wallr.OnGround && EnableAirDash)
        {
            if (!IsAirDashing)
                StartCoroutine(AirDash(1f));
        }
        if (IsAirDashing)
            StartCoroutine(AirDash(1f));
        ani.SetBool("OnGround", wallr.OnGround);
    }
    void InAirByMouse()
    {
        rb.velocity = transform.right * AirVelo.x * Input.GetAxis("Vertical")
                        + new Vector3(0, rb.velocity.y, 0)
                            - transform.forward * AirVelo.z * Input.GetAxis("Horizontal");
    }
    void Fall()
    {
        rb.velocity = FallVelo;
    }
    void DoubleJump()
    {
        rb.velocity += new Vector3(0, 20f, 0);
    }
    IEnumerator AirDash(float time)
    {
        rb.velocity = transform.right * AirVelo.x * 2
                        - transform.forward * AirVelo.z;
        IsAirDashing = true;
        yield return new WaitForSeconds(time);
        IsAirDashing = false;
        yield break;
    }
}
