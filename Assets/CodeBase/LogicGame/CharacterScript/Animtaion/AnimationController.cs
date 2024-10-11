using UnityEngine;
public class AnimationController : MonoBehaviour, IAnimationController
{
    private const string _isWalking = "isWalking";
    private const string _isJumping = "isJumping";
    private const string _isDying = "Die";

    public Animator animator;

    private bool canWalk;
    private bool canJump;
    private bool canDie;
    private bool isCurrentlyJumping;
    private bool isCurrentlyWalking;



    public void SetJumping( bool isJumping )
    {
        if ( canJump && isCurrentlyJumping != isJumping )
        {
            animator.SetBool ( _isJumping , isJumping );
            isCurrentlyJumping = isJumping;
        }
    }

    public void SetWalking( bool isWalking )
    {
        if ( canWalk && isCurrentlyWalking != isWalking )
        {
            animator.SetBool ( _isWalking , isWalking );
            isCurrentlyWalking = isWalking;
        }
    }

    public void SetDeath( )
    {
        
            animator.SetTrigger ( _isDying );
        
    }

    public void OnCharacterDataChanged( CharacterStats stats )
    {
        canWalk = stats.canWalk;
        canJump = stats.canJump;
        canDie = stats.canDie;
    }

    void IAnimationController.UpdateAnimations( bool isGrounded , float horizontalInput )
    {
        // Управляем анимацией ходьбы
        SetWalking ( horizontalInput != 0 && isGrounded );

        // Управляем анимацией прыжка
        SetJumping ( !isGrounded );
    }
}
