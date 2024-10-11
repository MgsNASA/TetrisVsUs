public interface IAnimationController:ICharacterObserver
{
    void OnCharacterDataChanged( CharacterStats stats );
    void SetWalking( bool isWalking );
    void SetJumping( bool isJumping );
    void SetDeath( );
    void UpdateAnimations( bool isGrounded , float v );
}
