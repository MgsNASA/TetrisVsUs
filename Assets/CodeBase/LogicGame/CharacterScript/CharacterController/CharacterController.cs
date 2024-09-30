using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public CharacterStats characterStats;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask platformLayer;

    private List<ICharacterObserver> observers = new List<ICharacterObserver> ();
    private ICharacterMover characterMover;
    private ICharacterJumper characterJumper;
    private ICrushDetector crushDetector;
    private IAnimationController animationController;

    private bool isGrounded;
    private bool isCrushed;

    private void Start( )
    {
        characterMover = GetComponent<ICharacterMover> ();
        if ( characterMover == null )
            Debug.LogError ( "ICharacterMover is not attached!" );

        characterJumper = GetComponent<ICharacterJumper> ();
        if ( characterJumper == null )
            Debug.LogError ( "ICharacterJumper is not attached!" );

        crushDetector = GetComponent<ICrushDetector> ();
        if ( crushDetector == null )
            Debug.LogError ( "ICrushDetector is not attached!" );

        animationController = GetComponent<IAnimationController> ();
        if ( animationController == null )
            Debug.LogError ( "IAnimationController is not attached!" );

        RegisterObservers ();
    }


    private void RegisterObservers( )
    {
        observers.Add ( characterMover );
        observers.Add ( characterJumper );
        observers.Add( crushDetector );
        observers .Add ( animationController );

        foreach ( var observer in observers )
        {
            observer.OnCharacterDataChanged ( characterStats );
        }
    }

    private void Update( )
    {
        if ( crushDetector.CheckIfCrushed ( transform ) )
        {
            if ( !isCrushed )
            {
                isCrushed = true;
                animationController.SetDeath ();
            }
            return;
        }

        isGrounded = Physics2D.OverlapCircle ( groundCheck.position , groundCheckRadius , platformLayer );
        animationController.SetJumping ( !isGrounded );

        if ( isCrushed )
            return;

        HandleMovement ();
        HandleJumping ();
        animationController.SetWalking ( Mathf.Abs ( Input.GetAxis ( "Horizontal" ) ) > 0 );
    }

    private void HandleMovement( )
    {
        float moveInput = Input.GetAxis ( "Horizontal" );
        Vector3 moveDirection = new Vector3 ( moveInput , 0 , 0 );
        characterMover.Move ( moveDirection , transform );

        if ( moveInput > 0 && !characterMover.IsFacingRight )
        {
            characterMover.Flip ( transform );
            if ( isGrounded )
                characterMover.PlayDust ();
        }
        else if ( moveInput < 0 && characterMover.IsFacingRight )
        {
            characterMover.Flip ( transform );
            if ( isGrounded )
                characterMover.PlayDust ();
        }

        if ( !isGrounded )
            characterMover.StopDust ();
    }

    private void HandleJumping( )
    {
        if ( Input.GetButtonDown ( "Jump" ) && isGrounded )
        {
            characterJumper.Jump ( isGrounded );
            characterMover.PlayDust ();
        }
    }



}
