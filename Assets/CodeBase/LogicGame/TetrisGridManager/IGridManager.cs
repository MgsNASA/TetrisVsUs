using UnityEngine;

public interface IGridManager :IService
{
    void AddToGrid( Transform tetrisBlock );
    bool ValidMove( Transform tetrisBlock );
    void CheckForLines( );
}
