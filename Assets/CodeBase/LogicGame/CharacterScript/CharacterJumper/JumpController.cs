using UnityEngine;
using System.Collections;

public class JumpController : MonoBehaviour, ICharacterJumper
{
    [SerializeField] private float jumpForce;
    [SerializeField] private int maxJumpCount = 2; // Максимум два прыжка
    private int jumpCount = 0; // Количество выполненных прыжков
    private float jumpCooldown;
    private bool _canJump = true;
    private Rigidbody2D rb;
    private CollisionChecker collisionChecker;

    public float JumpForce => jumpForce;

    // Свойство для доступа к canJump
    public bool CanJump
    {
        get => _canJump;
        set => _canJump = value;
    }

    private void Start( )
    {
        rb = GetComponent<Rigidbody2D> ();
        collisionChecker = GetComponent<CollisionChecker> ();
    }

    public void OnCharacterDataChanged( CharacterStats stats )
    {
        jumpCooldown = stats.jumpCooldown;
    }

    public void Jump( )
    {
        if ( CanJump && jumpCount < maxJumpCount )
        {
            rb.velocity = new Vector2 ( rb.velocity.x , 0 ); // Сброс вертикальной скорости перед прыжком
            rb.AddForce ( Vector2.up * jumpForce , ForceMode2D.Impulse );
            jumpCount++;

            if ( jumpCount >= maxJumpCount )
            {
                CanJump = false; // Отключить прыжки после достижения максимума
                StartCoroutine ( JumpCooldownRoutine () );
            }
        }
    }

    private IEnumerator JumpCooldownRoutine( )
    {
        yield return new WaitForSeconds ( jumpCooldown );
        CanJump = true;
    }

    public void ResetJumpCount( )
    {
        jumpCount = 0; // Сброс прыжков при касании земли
        CanJump = true; // Разрешить прыжок после касания земли
    }
}
