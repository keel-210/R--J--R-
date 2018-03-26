using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDisableInstantiate : MonoBehaviour
{
    [SerializeField] GameObject Obj;
    void OnDisable()
    {
        if (Obj)
        {
            Instantiate(Obj, transform.position, transform.rotation);
        }
    }
}
