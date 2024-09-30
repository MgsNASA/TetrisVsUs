using UnityEngine;

public class AnimationController : MonoBehaviour, IAnimationController
{
    public Animator animator;
    private bool canWalk;
    private bool canJump;
    private bool canDie;

    public void OnCharacterDataChanged( CharacterStats stats )
    {
        canWalk = stats.canWalk;
        canJump = stats.canJump;
        canDie = stats.canDie;

    }

    public void SetWalking( bool isWalking )
    {
        if ( canWalk )
        {
            animator.SetBool ( "isWalking" , isWalking );
        }
    }

    public void SetJumping( bool isJumping )
    {
        if ( canJump )
        {
            animator.SetBool ( "isJumping" , isJumping );
        }
    }

    public void SetDeath( )
    {
        if ( canDie )
        {
            animator.SetTrigger ( "Die" );
        }
    }
}
