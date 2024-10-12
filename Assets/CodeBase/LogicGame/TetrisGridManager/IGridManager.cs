﻿using UnityEngine;

public interface IGridManager :IService
{
    void AddToGrid( GameObject tetrisBlock );
    bool ValidMove( Transform tetrisBlock, Vector3 direction );
    void CheckForLines( );
    int [ ] GetColumnBlockCounts( ); // Добавляем метод для получения количества блоков в столбцах
    void ResetGrid( );
}
