using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{

    public Animator animator;
    int horizontal;
    int vertical;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        horizontal = Animator.StringToHash("horizontalSpeed");
        vertical = Animator.StringToHash("verticalSpeed");

    }

    public void UpdateAnimator(float horizontalMovement, float verticalMovement) 
    {
        float snappedHorizontal;
        float snappedVertical;

        #region horizontal
        if ( horizontalMovement > 0 && horizontalMovement < 0.55f )
        {
            snappedHorizontal = 0.5f;
        }
        else if ( horizontalMovement < 0 && horizontalMovement > -0.55f )
        {
            snappedHorizontal = -0.5f;
        }

        else if ( horizontalMovement < -0.55f)
        {
            snappedHorizontal = -1f;
        }
        else if (horizontalMovement > 0.55f)
        {
            snappedHorizontal = 1f;
        }
        else
        {
            snappedHorizontal = 0f;
        }
        #endregion horizontal
        #region vertical
        if (verticalMovement > 0 && verticalMovement < 0.55f)
        {
            snappedVertical = 0.5f;
        }
        else if (verticalMovement < 0 && verticalMovement > -0.55f)
        {
            snappedVertical = -0.5f;
        }

        else if (verticalMovement < -0.55f)
        {
            snappedVertical = -1f;
        }
        else if (verticalMovement > 0.55f)
        {
            snappedVertical = 1f;
        }
        else
        {
            snappedVertical = 0f;
        }
        #endregion vertical

        animator.SetFloat(horizontal, snappedHorizontal, 0.1f, Time.deltaTime);
        animator.SetFloat(vertical, snappedVertical, 0.1f, Time.deltaTime);

    }

    public void PlayTargetAnimation(string targetAnimation, bool isInteracting)
    {
        animator.SetBool("isInteracting", isInteracting);
        animator.CrossFade(targetAnimation, 0.2f);
    }
}
