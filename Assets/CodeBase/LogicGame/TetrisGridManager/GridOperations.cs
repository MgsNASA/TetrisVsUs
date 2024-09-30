using UnityEngine;

public class GridOperations : IGridManager
{
    private Transform [ , ] grid;
    private int width;
    private int height;
    private BlockFallAnimator fallAnimator;

    public GridOperations( int width , int height )
    {
        this.width = width;
        this.height = height;
        grid = new Transform [ width , height ];
       
    }

    // Добавление блока в сетку
    public void AddToGrid( Transform tetrisBlock )
    {
        foreach ( Transform child in tetrisBlock )
        {
            int roundedX = Mathf.RoundToInt ( child.position.x );
            int roundedY = Mathf.RoundToInt ( child.position.y );

            if ( IsWithinBounds ( roundedX , roundedY ) )
            {
                grid [ roundedX , roundedY ] = child;
            }
        }

        CheckForLines ();
    }

    public bool ValidMove( Transform tetrisBlock )
    {
        foreach ( Transform child in tetrisBlock )
        {
            int roundedX = Mathf.RoundToInt ( child.position.x );
            int roundedY = Mathf.RoundToInt ( child.position.y );

            // Проверка на выход за границы сетки
            if ( !IsWithinBounds ( roundedX , roundedY ) )
            {
                return false;
            }

            // Проверка, не занята ли эта клетка другим блоком
            if ( grid [ roundedX , roundedY ] != null )
            {
                return false;
            }
        }
        return true;
    }


    // Проверка на полные линии
    public void CheckForLines( )
    {
        for ( int y = 0; y < height; y++ )
        {
            if ( IsLineFull ( y ) )
            {
                DeleteLine ( y );
                MoveLinesDown ( y );
                y--; // Проверяем ту же линию повторно после сдвига
            }
        }
    }

    // Проверка, что линия полная
    private bool IsLineFull( int y )
    {
        for ( int x = 0; x < width; x++ )
        {
            if ( grid [ x , y ] == null )
            {
                return false;
            }
        }
        return true;
    }

    // Удаление полной линии
    private void DeleteLine( int y )
    {
        for ( int x = 0; x < width; x++ )
        {
            if ( grid [ x , y ] != null )
            {
                GameObject.Destroy ( grid [ x , y ].gameObject );
                grid [ x , y ] = null;
            }
        }
    }

    // Сдвиг линий вниз после удаления
    private void MoveLinesDown( int deletedRow )
    {
        for ( int y = deletedRow + 1; y < height; y++ )
        {
            for ( int x = 0; x < width; x++ )
            {
                if ( grid [ x , y ] != null )
                {
                    int fallDistance = 0;

                    // Рассчитываем расстояние падения
                    while ( y - fallDistance - 1 >= 0 && grid [ x , y - fallDistance - 1 ] == null )
                    {
                        fallDistance++;
                    }

                    if ( fallDistance > 0 )
                    {
                        Transform block = grid [ x , y ];
                        grid [ x , y - fallDistance ] = block;
                        grid [ x , y ] = null;

                        BlockFall ( block , fallDistance );  // Используем анимацию падения
                    }
                }
            }
        }
    }

    // Метод для анимации падения блока
    private void BlockFall( Transform block , int fallDistance )
    {
        Debug.Log ("ASDSA");
        if ( fallAnimator != null )
        {
            MonoBehaviour rootMono = block.GetComponentInParent<MonoBehaviour> ();
            if ( rootMono != null )
            {
                rootMono.StartCoroutine ( fallAnimator.AnimateBlockFall ( block , fallDistance ) );
            }
        }
    }

    // Проверка, что координаты внутри сетки
    private bool IsWithinBounds( int x , int y )
    {
        return x >= 0 && x < width && y >= 0 && y < height;
    }
}
