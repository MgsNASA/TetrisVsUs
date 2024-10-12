using UnityEngine;

public class AnimationController : MonoBehaviour, IAnimationController
{
    private const string _isWalking = "isWalking";
    private const string _isJumping = "isJumping";
    private const string _isDying = "Die";

    public Animator animator;
    private GameProcessController gameProcessController;

    private bool canWalk;
    private bool canJump;
    private bool canDie;
    private bool isCurrentlyJumping;
    private bool isCurrentlyWalking;
    private bool isDying;

    private void Start( )
    {
        gameProcessController = FindObjectOfType<GameProcessController> ();
    }

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
        if ( canDie && !isDying )
        {
            animator.SetTrigger ( _isDying );
            isDying = true; // Устанавливаем флаг для состояния смерти
            EndGame ();
        }
    }



    private void EndGame( )
    {
        if ( gameProcessController != null )
        {
            gameProcessController.GameOver (); // Вызов метода окончания игры
        }
    }

    public void OnCharacterDataChanged( CharacterStats stats )
    {
        canWalk = stats.canWalk;
        canJump = stats.canJump;
        canDie = stats.canDie;
    }

    void IAnimationController.UpdateAnimations( bool isGrounded , float horizontalInput )
    {
        SetWalking ( horizontalInput != 0 && isGrounded );
        SetJumping ( !isGrounded );
    }
}
