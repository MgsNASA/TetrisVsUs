using System.Collections;
using UnityEngine;

public class BlockMover : IBlockMover
{
    private readonly ITetrisGridManager gridManager;

    public BlockMover( ITetrisGridManager gridManager )
    {
        this.gridManager = gridManager;
    }

    // Перемещение на один шаг с задержкой
    public IEnumerator Move( Vector3 direction , Transform blockTransform )
    {
        Vector3 startPosition = blockTransform.position;
        Vector3 targetPosition = startPosition + direction;

        // Проверка допустимости перемещения
        if ( gridManager.ValidMove ( blockTransform, direction ) )
        {
            // Устанавливаем позицию на новую точку сразу
            blockTransform.position = targetPosition;
        }
        else
        {
            // Останавливаем блок, если движение недопустимо
            blockTransform.position = startPosition;
        }

        // Задержка перед следующим шагом
        yield return new WaitForSeconds ( 0.1f ); // Ждём 1 секунду, чтобы блок двигался не слишком быстро
    }

    public bool ValidMove( Transform tetrisBlock,Vector3 direction )
    {
        return gridManager.ValidMove ( tetrisBlock , direction );
    }
}
