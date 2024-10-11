using UnityEngine;

internal interface ICharacterMover : ICharacterObserver
{
    void Move( Vector3 direction , Transform transform );
    bool IsFacingRight
    {
        get;
    }
    void Flip( Transform transform );
    void PlayDust( );
    void StopDust( );
  //  void AllowMovement( bool allow ); // Добавляем метод для управления движением
}
