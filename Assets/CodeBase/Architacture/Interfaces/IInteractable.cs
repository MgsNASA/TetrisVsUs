using UnityEngine;

public interface IInteractable
{
    void Interact( ); // Основное взаимодействие
}
public interface IPushable : IInteractable
{
    void Push( Vector2 direction , float force ); // Взаимодействие "толкнуть"
}

public interface IDestructible : IInteractable
{
    void Destroy( ); // Взаимодействие "уничтожить"
}