using UnityEngine;

public class TetrisGridManager : MonoBehaviour, ITetrisGridManager
{
    public static int Width = 10;  // Ширина сетки
    public static int Height = 52;  // Высота сетки

    private IGridManager _gridOperations;  // Интерфейс для управления сеткой
    private BlockFallAnimator fallAnimator;  // Аниматор падения блока

    private Vector2 gridOffset = new Vector2 ( 1 , 1 );

    void Awake( )
    {
        _gridOperations = new GridOperations ( Width , Height , gridOffset );
        fallAnimator = new BlockFallAnimator ( 2.0f );
    }

    public void AddToGrid( Transform tetrisBlock )
    {
        if ( _gridOperations == null )
        {
            Debug.LogError ( "gridManager is null when trying to add to grid." );
            return;
        }
        _gridOperations.AddToGrid ( tetrisBlock );
    }

    public bool ValidMove( Transform tetrisBlock ,Vector3 direction )
    {
        if ( _gridOperations == null )
        {
            Debug.LogError ( "gridManager is null" );
            return false;
        }
        return _gridOperations.ValidMove ( tetrisBlock, direction );
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

    // Вызываем отрисовку сетки через GridOperations
    void OnDrawGizmos( )
    {
        if ( _gridOperations != null )
        {
         
            ( _gridOperations as GridOperations )?.DrawGrid ( gridOffset );
        }
    }
}
