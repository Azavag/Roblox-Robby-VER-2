using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    [SerializeField] Animator animator;

    public void PlayJumpAnimation(bool jumpState)
    {
        animator.SetBool("isJumping", jumpState);
    }
    public void PlayFallAnimation(bool fallState)
    {
        animator.SetBool("isFalling", fallState);
    }
    public void PlayRunAnimation(float speedValue)
    {
        animator.SetFloat("speed", speedValue);
    } 
  
    public void WinAnimation()
    {
        animator.SetTrigger("win");
    }

    public void DanceAnimation(bool danceState)
    {
        animator.SetBool("isDance", danceState);
    }
    public void PassBallAnimation()
    {
        animator.SetTrigger("pass");
    }

}
