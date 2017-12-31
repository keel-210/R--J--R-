using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField]
    Vector3 EndPos;
    [SerializeField]
    float Movingtime;

    Vector3 velo;
    float Timer = 0;
    public bool IsActivate = false;
    void Start()
    {
        velo = (EndPos - transform.position) / Movingtime;
    }

    void Update()
    {
        if (IsActivate)
        {
            Timer += Time.deltaTime;
            if (Timer < Movingtime)
            {
                transform.position += velo * Time.deltaTime;
            }
            else
            {
                transform.position = EndPos;
            }
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(100, 100, 100));
        Gizmos.DrawWireCube(EndPos, new Vector3(100, 100, 100));
        Gizmos.DrawLine(transform.position, EndPos);
    }
}

