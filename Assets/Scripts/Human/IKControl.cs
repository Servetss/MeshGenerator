using UnityEngine;

public class IKControl : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public Transform rightFootObj = null;

    public Transform leftFootObj = null;
    
    public Transform rightFootLookObj = null;

    public Transform leftFootLookObj = null;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnAnimatorIK()
    {
        if (animator)
        {
            if (rightFootObj != null)
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);
                animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1);
                animator.SetIKPosition(AvatarIKGoal.RightFoot, rightFootLookObj.position);
                animator.SetIKRotation(AvatarIKGoal.RightFoot, rightFootLookObj.rotation);
            }

            if (leftFootObj != null)
            {
                animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1);
                animator.SetIKPosition(AvatarIKGoal.LeftFoot, leftFootLookObj.position);
                animator.SetIKRotation(AvatarIKGoal.LeftFoot, leftFootLookObj.rotation);
            }
        }
        else
        {
            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 0);
            animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 0);
            animator.SetLookAtWeight(0);

            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 0);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 0);
            animator.SetLookAtWeight(0);
        }
    }
}
