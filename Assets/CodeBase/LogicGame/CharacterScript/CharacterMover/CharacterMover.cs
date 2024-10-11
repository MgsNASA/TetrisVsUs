using UnityEngine;

public class CharacterMover : MonoBehaviour, ICharacterMover
{
    private float moveSpeed;
    [SerializeField]
    private float minX;
    [SerializeField]
    private float maxX;

    // Добавляем переменные для направления и пыли
    public ParticleSystem dust;

    // Эта переменная теперь будет использоваться для хранения направления
    public bool isFacingRight = true;

    // Реализация свойства интерфейса
    public bool IsFacingRight
    {
        get
        {
            return isFacingRight;
        }
    }

    public void OnCharacterDataChanged( CharacterStats stats )
    {
        moveSpeed = stats.moveSpeed;
        minX = stats.minX;
        maxX = stats.maxX;
    }

    public void Move( Vector3 direction , Transform transform )
    {
        Vector3 newPosition = transform.position + ( direction * moveSpeed * Time.deltaTime );
        newPosition.x = Mathf.Clamp ( newPosition.x , minX , maxX );
        transform.position = newPosition;
    }

    // Метод для переворота персонажа
    public void Flip( Transform transform )
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    // Методы для управления пылью
    public void PlayDust( )
    {
        if ( dust != null )
        {
            dust.Play ();
        }
    }

    public void StopDust( )
    {
        if ( dust != null )
        {
            dust.Stop ();
        }
    }
}
