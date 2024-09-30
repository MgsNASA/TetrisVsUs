using UnityEngine;

public class RotationManager : IRotationManager
{
    public Quaternion GenerateRandomRotation( )
    {
        int randomRotation = Random.Range ( 0 , 4 ) * 90; // Поворот на 90 градусов
        return Quaternion.Euler ( 0 , 0 , randomRotation );
    }
}