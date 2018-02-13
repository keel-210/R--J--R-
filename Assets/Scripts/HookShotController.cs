using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookShotController : MonoBehaviour
{
    [SerializeField]
    Vector3 velo;
    HookShooter hookShooter;
    void Start()
    {
        hookShooter = FindObjectOfType<HookShooter>();
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = transform.right * velo.x +
                        transform.up * velo.y +
                        transform.forward * velo.z;
    }

    void OnCollisionEnter(Collision collision)
    {
        hookShooter.HookHit(collision.contacts[0].point);
    }
}
