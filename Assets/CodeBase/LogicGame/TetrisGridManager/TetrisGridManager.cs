using UnityEngine;

public class TetrisGridManager : MonoBehaviour, ITetrisGridManager, IStateClass
{
    public static int Width = 10;  // Ширина сетки
    public static int Height = 280;  // Высота сетки

    private IGridManager _gridOperations;  // Интерфейс для управления сеткой
    private BlockFallAnimator fallAnimator;  // Аниматор падения блока

    private Vector2 gridOffset = new Vector2 ( 1 , 0 );
    private bool isPaused;  // Флаг для отслеживания состояния паузы

    void Awake( )
    {
        _gridOperations = new GridOperations ( Width , Height , gridOffset );
        fallAnimator = new BlockFallAnimator ( 2.0f );
        isPaused = false;  // Изначально не в состоянии паузы
    }

    public void AddToGrid( GameObject tetrisBlock ) // Изменили Transform на GameObject
    {
        if ( _gridOperations == null )
        {
            Debug.LogError ( "gridManager is null when trying to add to grid." );
            return;
        }
        _gridOperations.AddToGrid ( tetrisBlock );
    }

    public bool ValidMove( Transform tetrisBlock , Vector3 direction )
    {
        if ( _gridOperations == null )
        {
            Debug.LogError ( "gridManager is null" );
            return false;
        }
        return _gridOperations.ValidMove ( tetrisBlock , direction );
    }

    public void CheckForLines( )
    {
        if ( _gridOperations == null )
        {
            Debug.LogError ( "gridManager is null when checking for lines." );
            return;
        }
        _gridOperations.CheckForLines ();
    }

    public int [ ] GetColumnBlockCounts( )
    {
        return _gridOperations.GetColumnBlockCounts ();
    }

    void OnDrawGizmos( )
    {
        if ( _gridOperations != null )
        {
            ( _gridOperations as GridOperations )?.DrawGrid ( gridOffset );
        }
    }

    public void StartClass( )
    {
        // Логика для начала работы менеджера
        // Здесь можно добавить инициализацию или сброс состояния
        isPaused = false;  // Убедитесь, что менеджер не в состоянии паузы
        Debug.Log ( "TetrisGridManager has started." );
    }

    public void Pause( )
    {
        // Логика для паузы
        isPaused = true;  // Устанавливаем флаг паузы
        Debug.Log ( "TetrisGridManager is paused." );
    }

    public void Resume( )
    {
        // Логика для возобновления
        isPaused = false;  // Сбрасываем флаг паузы
        Debug.Log ( "TetrisGridManager has resumed." );
    }

    public void Restart( )
    {
        _gridOperations.ResetGrid ();
        // Логика для перезапуска
        _gridOperations = new GridOperations ( Width , Height , gridOffset ); // Сброс управления сеткой
        fallAnimator = new BlockFallAnimator ( 2.0f ); // Сброс аниматора
        isPaused = false;  // Убедитесь, что менеджер не в состоянии паузы
        Debug.Log ( "TetrisGridManager has restarted." );
    }

    public bool IsPaused( )
    {
        return isPaused;  // Метод для проверки состояния паузы
    }

    public void OnApplicationQuit( )
    {
        if ( _gridOperations != null )
        {
            _gridOperations.ResetGrid (); // Сброс сетки
        }
        Debug.Log ( "Application is quitting. Grid has been reset." );
    }
}
