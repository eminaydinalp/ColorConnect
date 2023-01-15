using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : RMonoBehaviour
{
    public Animator Animator;

    private PlayerController playerController => PlayerController.Instance;
    private PlayerState playerState => playerController.CurrentPlayerState.Value;
    private Dictionary<string, string> animationNames;

    void Start()
    {
        GetComponentAndAssign(ref Animator);
        animationNames = ReflectionHelpers<object>.GetPropertyNamesWithValues(typeof(Animations));
    }

    private void PlayerStateChanged()
    {
        if (Animator == null)
            return;

        switch (playerState)
        {
            case PlayerState.Idle:
                SetAnimationTrigger(Animations.IDLE, false);
                break;
            case PlayerState.Running:
                SetAnimationTrigger(Animations.RUN, false);
                break;
            case PlayerState.Jumping:
                SetAnimationTrigger(Animations.JUMP, false);
                break;
            default:
                break;
        }
    }

    void SetAnimationTrigger(string animationProp, bool resetOther = false)
    {
        if (!resetOther)
        {
            Animator.SetTrigger(animationProp);
        }
        else
        {
            foreach (var item in animationNames)
            {
                if (item.Key.Equals(animationProp))
                {
                    Animator.SetTrigger(animationProp);
                }
                else
                {
                    Animator.ResetTrigger(item.Value);
                }
            }
        }
    }

    private void OnEnable()
    {
        if (playerController)
            playerController.CurrentPlayerState.OnValueChange += PlayerStateChanged;
    }



    private void OnDisable()
    {
        if (playerController)
            playerController.CurrentPlayerState.OnValueChange -= PlayerStateChanged;
    }
}
