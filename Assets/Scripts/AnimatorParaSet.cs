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
        ani.SetFloat("MoveX", Input.GetAxis("Vertical"));
        ani.SetFloat("MoveY", Input.GetAxis("Horizontal"));
        ani.SetFloat("AbsMoveX", Mathf.Abs(Input.GetAxis("Vertical")));
        ani.SetFloat("AbsMoveY", Mathf.Abs(Input.GetAxis("Horizontal")));
    }
}
