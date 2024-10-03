﻿using UnityEngine;

public interface ITetrisGridManager :IService
{
    bool ValidMove( Transform tetrisBlock,Vector3 direction );
    void AddToGrid( Transform tetrisBlock );
    void CheckForLines( );  // Добавлен метод для проверки линий
}