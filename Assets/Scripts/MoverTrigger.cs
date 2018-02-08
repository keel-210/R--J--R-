using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverTrigger : MonoBehaviour
{
    [SerializeField]
    float FireTime;
    Mover mover;
    void Start()
    {
        mover = transform.GetComponentInParent<Mover>();
    }
    void OnTriggerEnter()
    {
        StartCoroutine(this.DelayMethod(FireTime, () =>
         { mover.IsActivate = true; }
        ));
    }
}
