internal interface IAnimationController:ICharacterObserver
{
    void SetWalking( bool isWalking );
    void SetJumping( bool isJumping );
    void SetDeath( );
}