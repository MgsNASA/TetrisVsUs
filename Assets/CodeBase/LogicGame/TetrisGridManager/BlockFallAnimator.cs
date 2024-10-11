using System.Collections;
using UnityEngine;

public class BlockFallAnimator : IStateClass
{
    private readonly float fallSpeed;
    private bool isPaused; // Флаг паузы
    private Coroutine currentCoroutine;

    public BlockFallAnimator( float fallSpeed )
    {
        this.fallSpeed = fallSpeed;
    }

    // Анимация падения блока
    public IEnumerator AnimateBlockFall( Transform block , int fallDistance )
    {
        if ( block == null || isPaused )
            yield break;

        Vector3 startPosition = block.position;
        Vector3 targetPosition = startPosition + Vector3.down * fallDistance;

        float elapsedTime = 0;
        float journeyTime = fallDistance / fallSpeed;

        while ( elapsedTime < journeyTime && !isPaused )
        {
            block.position = Vector3.Lerp ( startPosition , targetPosition , elapsedTime / journeyTime );
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Устанавливаем конечное положение
        block.position = targetPosition;
    }

    public void StartClass( )
    {
        isPaused = false; // Сброс флага паузы
    }

    // Метод для паузы анимации
    public void Pause( )
    {
        isPaused = true; // Устанавливаем флаг паузы
    }

    // Метод для возобновления анимации
    public void Resume( )
    {
        isPaused = false; // Снимаем флаг паузы
    }

    public void Restart( )
    {
        // Логика сброса анимации, если требуется.
        // Например, можно сбросить переменную isPaused:
        isPaused = false;
        // Если есть активная корутина, можно её остановить и запустить заново
        if ( currentCoroutine != null )
        {
            // Тут нужно получить ссылку на MonoBehaviour для остановки корутины
            // Можно сделать это, например, передавая его в конструктор
        }
    }

    public bool CanFallFurther( Transform block , Transform [ , ] grid , Vector3 gridFollowTargetPosition )
    {
        int roundedX = Mathf.RoundToInt ( block.position.x - gridFollowTargetPosition.x );
        int roundedY = Mathf.RoundToInt ( block.position.y - gridFollowTargetPosition.y );

        // Проверяем, что блок находится выше нижней границы сетки и под ним свободно
        if ( roundedY > 0 && grid [ roundedX , roundedY - 1 ] == null )
        {
            return true;
        }
        return false;
    }
}
