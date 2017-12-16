using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField]
    Vector3 StartPos, EndPos;
    [SerializeField]
    float time;

    Vector3 velo;
    float Timer;
    void Start()
    {
        transform.position = StartPos;
        velo = (EndPos - StartPos) / time;
    }

    void Update()
    {
        Timer += Time.deltaTime;
        if (Timer < time)
        {
            transform.position += velo * Time.deltaTime;
        }
        else
        {
            transform.position = EndPos;
        }
    }
}
