using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    [SerializeField]
    GameObject bullet, bulletEffect;
    [SerializeField]
    float ShootTime;
    float Timer;
    public int RemainBulletNum;
    [SerializeField]
    MouseDirection dir;
    [SerializeField]
    Vector3 velo;
    [SerializeField]
    MouseWallRunner wallRunner;
    void Update()
    {
        if (Input.GetAxis("Fire1") > 0)
        {
            Timer += Time.deltaTime;
            if (Timer > ShootTime && RemainBulletNum > 0)
            {
                if (wallRunner.Normal != Vector3.up)
                {
                    GameObject bul = Instantiate(bullet, transform.position, transform.rotation * dir.GetDir());
                    bul.GetComponent<Rigidbody>().velocity = bul.transform.right * velo.x
                                                                + bul.transform.up * velo.y
                                                                + bul.transform.forward * velo.z;
                    //RemainBulletNum--;
                }
                else
                {
                    GameObject bul = Instantiate(bullet, transform.position, transform.rotation);
                    bul.GetComponent<Rigidbody>().velocity = bul.transform.right * velo.x
                                                                + bul.transform.up * velo.y
                                                                + bul.transform.forward * velo.z;
                    //RemainBulletNum--;
                }
            }
        }
    }
}
