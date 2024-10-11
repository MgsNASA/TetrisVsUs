public interface ICharacterJumper : ICharacterObserver
{
    // Свойство, которое задает силу прыжка
    float JumpForce
    {
        get;
    }
    bool CanJump
    {
        get;
    }

    void ResetJumpCount( );
    // Метод для выполнения прыжка
    void Jump( );

    // Метод вызывается при изменении данных персонажа
    new void OnCharacterDataChanged( CharacterStats stats );
}
