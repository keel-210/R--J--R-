using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookShooter : MonoBehaviour
{

    bool IsHooked;
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetAxis("Fire2") > 0)
        {
            Hook();
        }
        else if (IsHooked)
        {
            HookRelease();
        }
    }
    void Hook()
    {

    }
    void HookRelease()
    {

    }
}
