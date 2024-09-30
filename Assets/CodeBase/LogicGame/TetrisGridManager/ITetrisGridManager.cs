using UnityEngine;

public interface ITetrisGridManager :IService
{
    bool ValidMove( Transform tetrisBlock );
    void AddToGrid( Transform tetrisBlock );
    void CheckForLines( );  // Добавлен метод для проверки линий
}
