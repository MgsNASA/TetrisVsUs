using UnityEngine;
using System.Collections.Generic;

public class CharacterController : MonoBehaviour
{
    public CharacterStats characterStats;
    private ICharacterMover characterMover;
    private ICharacterJumper characterJumper;
    private ICrushDetector crushDetector;
    private IAnimationController animationController;
    private CollisionChecker collisionChecker;

    private List<ICharacterObserver> observers = new List<ICharacterObserver> ();
    private bool wasGrounded = true;
    private bool isGrounded;
    private bool isCrushed;
    
    private void Start( )
    {
        characterMover = GetComponent<ICharacterMover> ();
        characterJumper = GetComponent<ICharacterJumper> ();
        crushDetector = GetComponent<ICrushDetector> ();
        animationController = GetComponent<IAnimationController> ();
        collisionChecker = GetComponent<CollisionChecker> ();

        RegisterObservers ();
    }

    private void RegisterObservers( )
    {
        observers.Add ( characterMover );
        observers.Add ( characterJumper );
        observers.Add ( crushDetector );
        observers.Add ( animationController );

        foreach ( var observer in observers )
        {
            observer.OnCharacterDataChanged ( characterStats );
        }
    }

    private void UpdateAnimations( )
    {
        // Если персонаж больше не на земле, проигрываем анимацию прыжка
        if ( !isGrounded && wasGrounded )
        {
            animationController.SetJumping ( true );
            wasGrounded = false;
        }
        // Если персонаж приземлился, выключаем анимацию прыжка
        else if ( isGrounded && !wasGrounded )
        {
            animationController.SetJumping ( false );
            wasGrounded = true;
            characterJumper.ResetJumpCount (); // Сбросить счетчик прыжков при касании земли
        }

        // Управляем анимацией ходьбы
        float moveInput = Input.GetAxis ( "Horizontal" );
        animationController.SetWalking ( Mathf.Abs ( moveInput ) > 0 && isGrounded );
    }

    private void Update( )
    {
        if ( crushDetector.CheckIfCrushed ( transform ) )
        {
            if ( !isCrushed )
            {
                isCrushed = true;
                animationController.SetDeath (); // Проигрываем анимацию смерти
            }
            return;
        }

        isGrounded = collisionChecker.IsGrounded ( transform );

        if ( isCrushed )
            return;

        // Обновляем анимации с учетом состояний
        UpdateAnimations ();

        // Логика движения и прыжка
        HandleMovement ();
        HandleJumping ();
    }

    private void HandleMovement( )
    {
        float moveInput = Input.GetAxis ( "Horizontal" );
        Vector3 moveDirection = new Vector3 ( moveInput , 0 , 0 );

        // Движение персонажа
        characterMover.Move ( moveDirection , transform );

        // Проверяем, в какую сторону смотрит персонаж и переключаем направление
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

        // Останавливаем пыль, если персонаж в воздухе
        if ( !isGrounded )
        {
            characterMover.StopDust ();
        }
    }

    private void HandleJumping( )
    {
        // Прыжок по нажатию клавиши
        if ( Input.GetButtonDown ( "Jump" ) )
        {
            // Проверка, может ли персонаж прыгать (двойной прыжок в воздухе)
            if ( characterJumper.CanJump )
            {
                characterJumper.Jump ();
                animationController.SetJumping ( true );

                // Проигрываем пыль при прыжке, если персонаж касается земли
                if ( isGrounded )
                {
                    characterMover.PlayDust ();
                }
            }
        }
    }

}
