using System.Collections;
using UnityEngine;

public class CharacterJumper : MonoBehaviour, ICharacterJumper
{
    private float jumpForce;
    private float jumpCooldown;
    private bool canJump = true;
    private Rigidbody2D rb;

    private void Start( )
    {
        rb = GetComponent<Rigidbody2D> ();
    }

    public void OnCharacterDataChanged( CharacterStats stats )
    {
        jumpForce = stats.jumpForce;
        jumpCooldown = stats.jumpCooldown;
    }

    public void Jump( bool isGrounded )
    {
        if ( isGrounded && canJump )
        {
            rb.AddForce ( Vector2.up * jumpForce );
            StartCoroutine ( JumpCooldownRoutine () );
        }
    }

    private IEnumerator JumpCooldownRoutine( )
    {
        canJump = false;
        yield return new WaitForSeconds ( jumpCooldown );
        canJump = true;
    }
}
