using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorParaSet : MonoBehaviour
{
    Animator ani;
    void Start()
    {
        ani = GetComponent<Animator>();
    }

    void Update()
    {
        ani.SetFloat("Forward", Input.GetAxis("Vertical"));
    }
}
