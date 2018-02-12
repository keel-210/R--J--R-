using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour
{
    [SerializeField]
    float DestroyTime;
    void Start()
    {
        StartCoroutine(Destroy(DestroyTime, this.gameObject));
    }
    IEnumerator Destroy(float time, GameObject obj)
    {
        yield return new WaitForSeconds(time);
        Destroy(obj);
    }
}
