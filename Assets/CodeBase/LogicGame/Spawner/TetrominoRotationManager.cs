using UnityEngine;

public class TetrominoRotationManager : IRotationManager
{
    public Quaternion GenerateRandomRotation( )
    {
        return Quaternion.Euler ( 0 , 0 , Random.Range ( 0 , 4 ) * 90 ); // Ротация кратная 90 градусам
    }
}
