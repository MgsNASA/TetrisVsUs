using System.Collections;
using UnityEngine;

public class TetrisBlock : MonoBehaviour
{
    public Vector3 RotationPoint
    {
        get; private set;
    }
    private float fallTime = 0; // Время между падениями

    public event System.Action OnBlockStopped;
    public event System.Action OnNewTetrominoRequested;

    private IBlockMover blockMover;
    private IBlockRotator blockRotator;

    // Конструктор, который принимает зависимости


    void Awake( )
    {
        ITetrisGridManager gridManager = FindObjectOfType<TetrisGridManager> ();

        blockMover = new BlockMover ( gridManager );
        blockRotator = new BlockRotator ( gridManager );

    }

    void Start( )
    {
        CalculateRotationPoint ();
        StartCoroutine ( FallRoutine () ); // Запуск корутины для падения
    }


    private IEnumerator FallRoutine( )
    {
        while ( true )
        {
            yield return new WaitForSeconds ( fallTime ); // Ожидание перед следующим падением

            // Проверка допустимости движения перед перемещением
            if ( blockMover.ValidMove ( transform , Vector3.down ) )
            {
                // Если движение допустимо, перемещаем блок вниз
                yield return StartCoroutine ( blockMover.Move ( Vector3.down , transform ) );
            }
            else
            {
                // Если движение недопустимо, блок останавливается
                BlockStopped ();

                // Добавляем блок в сетку после остановки
                ITetrisGridManager gridManager = FindObjectOfType<TetrisGridManager> ();
                gridManager.AddToGrid ( gameObject );

                Spawner.Instance.NewTetromino ();
                yield break; // Останавливаем падение
            }
        }
    }




    private void BlockStopped( )
    {
        OnBlockStopped?.Invoke ();
        OnNewTetrominoRequested?.Invoke (); // Запрос на создание нового тетромино
        this.enabled = false; // Отключаем компонент после остановки
    }

    private void CalculateRotationPoint( )
    {
        Vector3 totalPosition = Vector3.zero;
        int childCount = 0;

        foreach ( Transform child in transform )
        {
            totalPosition += child.localPosition;
            childCount++;
        }

        if ( childCount > 0 )
        {
            RotationPoint = totalPosition / childCount;
        }
    }
}
