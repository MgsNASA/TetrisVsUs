using UnityEngine;

public interface IRotationManager :IService
{
    Quaternion GenerateRandomRotation( );
}
