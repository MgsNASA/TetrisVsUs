using System.Collections;
using UnityEngine;

public class BlockFallAnimator
{
    private readonly float fallSpeed;

    public BlockFallAnimator( float fallSpeed )
    {
        this.fallSpeed = fallSpeed;
    }

    // Анимация падения блока
    public IEnumerator AnimateBlockFall( Transform block , int fallDistance )
    {
        if ( block == null )
            yield break;

        Vector3 startPosition = block.position;
        Vector3 targetPosition = startPosition + Vector3.down * fallDistance;

        float elapsedTime = 0;
        float journeyTime = fallDistance / fallSpeed;

        while ( elapsedTime < journeyTime )
        {
            block.position = Vector3.Lerp ( startPosition , targetPosition , elapsedTime / journeyTime );
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Устанавливаем конечное положение
        block.position = targetPosition;
    }

    // Проверка возможности дальнейшего падения
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
