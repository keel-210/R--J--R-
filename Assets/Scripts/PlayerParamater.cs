using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParamater : MonoBehaviour
{
    public Vector3 WallVelo, AirVelo, FallVelo;
    public Animator ani;
    public IKControl UC_IK;
    public Transform UC_Model;
    public GameObject LandingEffect, JumpEffect;
    public float JumpPower;
    public LayerMask mask;
    public bool IsRunningPlane;
}
