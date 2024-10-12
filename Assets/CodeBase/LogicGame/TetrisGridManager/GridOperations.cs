﻿using System.Collections.Generic;
using UnityEngine;

public class GridOperations : IGridManager, IStateClass
{
    private GameObject [ , ] grid; // Двумерный массив для хранения объектов
    private List<GameObject> storedObjects; // Список для хранения объектов
    private int width; // Ширина сетки
    private int height; // Высота сетки
    private BlockFallAnimator fallAnimator; // Аниматор падения блока
    private Vector2 gridOffset; // Смещение для сетки

    public GridOperations( int width , int height , Vector2 offset )
    {
        this.width = width;
        this.height = height;
        grid = new GameObject [ width , height ]; // Инициализация массива для объектов
        storedObjects = new List<GameObject> (); // Инициализация списка
        this.gridOffset = offset; // Сохраняем смещение
    }

    // Добавление блока в сетку
    public void AddToGrid( GameObject obj )
    {
        Transform tetrisBlock = obj.transform; // Получаем Transform из объекта
        foreach ( Transform child in tetrisBlock )
        {
            int roundedX = Mathf.RoundToInt ( child.position.x - gridOffset.x );
            int roundedY = Mathf.RoundToInt ( child.position.y - gridOffset.y );

            if ( IsWithinBounds ( roundedX , roundedY ) )
            {
                grid [ roundedX , roundedY ] = obj; // Сохраняем объект в сетке
                storedObjects.Add ( obj ); // Добавляем объект в список
            }
        }

        CheckForLines (); // Проверка на наличие полных линий
    }

    public bool ValidMove( Transform tetrisBlock , Vector3 direction )
    {
        foreach ( Transform child in tetrisBlock )
        {
            Vector3 newPosition = child.position + direction; // Рассчитываем новую позицию
            int roundedX = Mathf.RoundToInt ( newPosition.x - gridOffset.x );
            int roundedY = Mathf.RoundToInt ( newPosition.y - gridOffset.y );

            if ( !IsWithinBounds ( roundedX , roundedY ) )
            {
                Debug.Log ( "NotMoving for Grid" );
                return false;
            }

            if ( grid [ roundedX , roundedY ] != null )
            {
                Debug.Log ( "NotMoving for Block" );
                return false;
            }
        }
        return true;
    }

    // Отрисовка сетки
    public void DrawGrid( Vector3 offset )
    {
        Gizmos.color = Color.green;

        // Рисуем вертикальные линии
        for ( int x = 0; x <= width; x++ )
        {
            Gizmos.DrawLine ( new Vector3 ( x , 0 , 0 ) + offset , new Vector3 ( x , height , 0 ) + offset );
        }

        // Рисуем горизонтальные линии
        for ( int y = 0; y <= height; y++ )
        {
            Gizmos.DrawLine ( new Vector3 ( 0 , y , 0 ) + offset , new Vector3 ( width , y , 0 ) + offset );
        }
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
                return false; // Линия не полная
            }
        }
        return true; // Линия полная
    }

    // Удаление полной линии
    private void DeleteLine( int y )
    {
        for ( int x = 0; x < width; x++ )
        {
            if ( grid [ x , y ] != null )
            {
                GameObject.Destroy ( grid [ x , y ] ); // Удаление блока
                grid [ x , y ] = null; // Освобождение клетки в сетке
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
                        GameObject block = grid [ x , y ]; // Получаем GameObject
                        grid [ x , y - fallDistance ] = block; // Перемещение блока вниз
                        grid [ x , y ] = null; // Освобождение клетки в сетке

                        BlockFall ( block.transform , fallDistance ); // Используем анимацию падения
                    }
                }
            }
        }
    }

            // Метод для анимации падения блока
            private void BlockFall( Transform block , int fallDistance )
    {
        if ( fallAnimator != null )
        {
            MonoBehaviour rootMono = block.GetComponentInParent<MonoBehaviour> ();
            if ( rootMono != null )
            {
                rootMono.StartCoroutine ( fallAnimator.AnimateBlockFall ( block , fallDistance ) ); // Запуск анимации
            }
        }
    }

    // Проверка, что координаты внутри сетки
    private bool IsWithinBounds( int x , int y )
    {
        return x >= 0 && x < width && y >= 0 && y < height; // Проверка границ
    }

    public int [ ] GetColumnBlockCounts( )
    {
        int [ ] blockCounts = new int [ width ];

        for ( int x = 0; x < width; x++ )
        {
            blockCounts [ x ] = 0;
            for ( int y = 0; y < height; y++ )
            {
                if ( grid [ x , y ] != null )
                {
                    blockCounts [ x ]++;
                }
            }
        }

        return blockCounts;
    }
    public void ResetGrid( )
    {
        foreach ( var obj in storedObjects )
        {
            GameObject.Destroy ( obj ); // Удаляем объекты
        }
        storedObjects.Clear (); // Очищаем список
        Debug.Log ( "Grid has been reset." ); // Логируем сброс сетки
    }


    public void StartClass( )
    {
        // Инициализация состояния сетки
        ResetGrid ();
        Debug.Log ( "Grid Operations have started." );
    }

    public void Pause( )
    {
        // Логика паузы для сетки (например, отключение анимаций)
        Debug.Log ( "Grid operations are paused." );
    }

    public void Resume( )
    {
        // Логика возобновления (например, включение анимаций)
        Debug.Log ( "Grid operations have resumed." );
    }

    public void Restart( )
    {
        // Сброс сетки и повторная инициализация
        ResetGrid ();
        Debug.Log ( "Grid has been restarted." );
    }

}
