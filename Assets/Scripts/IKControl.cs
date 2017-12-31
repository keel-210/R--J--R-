using UnityEngine;
using System;
using System.Collections;

public class IKControl : MonoBehaviour
{
    protected Animator animator;

    public bool ikActive = false;
    public Transform RightHandObj = null, LeftHandObj = null, RightFootObj = null, LeftFootObj = null;
    public bool WallOnRightside;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // IK を計算するためのコールバック
    void OnAnimatorIK()
    {
        if (animator)
        {
            // IK が有効ならば、位置と回転を直接設定します
            if (ikActive)
            {
                if (WallOnRightside)
                {
                    // 指定されている場合は、右手のターゲット位置と回転を設定します
                    if (RightHandObj != null)
                    {
                        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                        animator.SetIKPosition(AvatarIKGoal.RightHand, RightHandObj.position);
                        animator.SetIKRotation(AvatarIKGoal.RightHand, RightHandObj.rotation);
                    }
                }
                else
                {
                    if (LeftHandObj != null)
                    {
                        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
                        animator.SetIKPosition(AvatarIKGoal.LeftHand, LeftHandObj.position);
                        animator.SetIKRotation(AvatarIKGoal.LeftHand, LeftHandObj.rotation);
                    }
                }
                animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, animator.GetFloat("RightFootWeight"));
                //animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, animator.GetFloat("RightFootWeight"));
                animator.SetIKPosition(AvatarIKGoal.RightFoot, RightFootObj.position);
                animator.SetIKRotation(AvatarIKGoal.RightFoot, RightFootObj.rotation);
                animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, animator.GetFloat("LeftFootWeight"));
                //animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, animator.GetFloat("LeftFootWeight"));
                animator.SetIKPosition(AvatarIKGoal.LeftFoot, LeftFootObj.position);
                animator.SetIKRotation(AvatarIKGoal.LeftFoot, LeftFootObj.rotation);
            }
            //IK が有効でなければ、手と頭の位置と回転を元の位置に戻します
            else
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
                animator.SetLookAtWeight(0);
            }
        }
    }
}